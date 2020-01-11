using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NotoIto;
using System.Speech.Recognition;
using System.IO;
using NAudio.Wave;
using Optional;

namespace NotoIto.App.Speech114514
{
    public partial class StartupForm : Form
    {
        string version = "0.1r1";
        string configDirectory = "";
        Stream inputStream = new MemoryStream();
        Config.AudioSettingsModel audioSettings;
        Config.VocabularyModel vocabulary;
        List<System.Threading.Thread> playSoundThreads = new List<System.Threading.Thread>();
        Dictionary<string, string> vocabularyDictionary = new Dictionary<string, string>();
        public StartupForm()
        {
            InitializeComponent();
        }

        private List<string> GetOutputDevices()
        {
            List<string> deviceList = new List<string>();
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var capabilities = WaveOut.GetCapabilities(i);
                deviceList.Add(capabilities.ProductName);
            }
            return deviceList;
        }

        private List<string> GetInputDevices()
        {
            List<string> deviceList = new List<string>();
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                var capabilities = WaveIn.GetCapabilities(i);
                deviceList.Add(capabilities.ProductName);
            }
            return deviceList;
        }

        private void SetInputStream(int index)
        {
            var waveIn = new WaveIn();
            waveIn.DeviceNumber = index;
            waveIn.StartRecording();
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);
        }

        private void PlaySound(int outputDeviceIndex, string fileName)
        {
            ThreadCleanUp();
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(
                () =>
                {
                    AudioFileReader reader = new AudioFileReader(fileName);
                    WaveOut waveOut = new WaveOut();
                    waveOut.DeviceNumber = outputDeviceIndex;
                    waveOut.Init(reader);
                    waveOut.Play();
                }));
            t.Start();
            playSoundThreads.Add(t);
        }

        private void ThreadCleanUp()
        {
            playSoundThreads.RemoveAll((x) => !x.IsAlive);
        }

        private void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            inputStream.Write(e.Buffer, 0, e.Buffer.Length);
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
            this.Text += version;
            configDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Speech114514";
            audioSettings = Utility.ClassSerializer.ReadXML<Config.AudioSettingsModel>(configDirectory + @"config.xml")
                .Match(
                    some: (x) => x,
                    none: () => new Config.AudioSettingsModel()
                );
            vocabulary = Utility.ClassSerializer.ReadXML<Config.VocabularyModel>(configDirectory + @"vocabulary.xml")
                .Match(
                    some: (x) => x,
                    none: () =>
                    {
                        var model = new Config.VocabularyModel();
                        model.VocabularyElements.Add(new Config.VocabularyElement());
                        return model;
                    }
                );
            inputComboBox.Items.AddRange(GetInputDevices().ToArray());
            outputComboBox.Items.AddRange(GetOutputDevices().ToArray());
            if (inputComboBox.Items.Contains(audioSettings.InputDevice))
                inputComboBox.SelectedItem = audioSettings.InputDevice;
            if (outputComboBox.Items.Contains(audioSettings.OutputDevice))
                outputComboBox.SelectedItem = audioSettings.OutputDevice;
            foreach (var vocab in vocabulary.VocabularyElements)
            {
                vocabularyDictionary.Add(vocab.Keyword, vocab.SoundFile);
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if (inputComboBox.SelectedIndex == -1 || outputComboBox.SelectedIndex == -1)
                return;
            Utility.ClassSerializer.WriteXML(vocabulary, configDirectory + @"vocabulary.xml");
            var engine = new SpeechRecognitionEngine(Application.CurrentCulture);
            SetInputStream(inputComboBox.SelectedIndex);
            engine.SetInputToWaveStream(inputStream);
            engine.SpeechRecognized += SpeechRecognized;
            engine.RecognizeAsync();
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // 生データを表示
            string recognitionWord = e.Result.Text;
            string fileName;
            if (e.Result.Confidence >= 0.5 && vocabularyDictionary.TryGetValue(recognitionWord, out fileName))
            {
                PlaySound(outputComboBox.SelectedIndex, fileName);
            }
        }

        private void inputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.ClassSerializer.WriteXML(audioSettings, configDirectory + @"config.xml");
        }

        private void outputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Utility.ClassSerializer.WriteXML(audioSettings, configDirectory + @"config.xml");
        }
    }
}
