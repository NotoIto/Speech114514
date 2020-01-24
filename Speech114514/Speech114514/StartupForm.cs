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
using System.Reflection;

namespace NotoIto.App.Speech114514
{
    public partial class StartupForm : Form
    {
        string version = "0.2r3";
        string configDirectory = "";
        Stream inputStream = new MemoryStream();
        Config.AudioSettingsModel audioSettings;
        Config.VocabularyModel vocabulary;
        WaveOut waveOut;
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
            waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);
            waveIn.StartRecording();
        }

        private void PlaySound(string fileName)
        {
            AudioFileReader reader = new AudioFileReader(fileName);
            waveOut.Init(reader);
            waveOut.Play();
        }

        private void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            //inputStream.Write(e.Buffer, 0, e.Buffer.Length);
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
            this.Text += version;
            Assembly myAssembly = Assembly.GetEntryAssembly();
            configDirectory = new FileInfo(myAssembly.Location).Directory.FullName;
            audioSettings = Utility.ClassSerializer.ReadXML<Config.AudioSettingsModel>(configDirectory + @"\config.xml")
                .Match(
                    some: (x) => x,
                    none: () => new Config.AudioSettingsModel()
                );
            vocabulary = Utility.ClassSerializer.ReadXML<Config.VocabularyModel>(configDirectory + @"\vocabulary.xml")
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
            Utility.ClassSerializer.WriteXML(vocabulary, configDirectory + @"\vocabulary.xml");
            var engine = new SpeechRecognitionEngine(Application.CurrentCulture);
            SetInputStream(inputComboBox.SelectedIndex);
            //engine.SetInputToAudioStream(inputStream, new System.Speech.AudioFormat.SpeechAudioFormatInfo(48000, System.Speech.AudioFormat.AudioBitsPerSample.Sixteen,System.Speech.AudioFormat.AudioChannel.Stereo));
            //engine.SetInputToWaveStream(inputStream);
            engine.SetInputToDefaultAudioDevice();
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(new Choices(vocabularyDictionary.Keys.ToArray()));
            engine.LoadGrammar(new Grammar(gb));
            engine.SpeechRecognized += SpeechRecognized;
            waveOut = new WaveOut();
            waveOut.DeviceNumber = outputComboBox.SelectedIndex;
            engine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            // 生データを表示
            string recognitionWord = e.Result.Text;
            string fileName;
            if (e.Result.Confidence >= 0.5 && vocabularyDictionary.TryGetValue(recognitionWord, out fileName))
            {
                PlaySound(fileName);
            }
        }

        private void inputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            audioSettings.InputDevice = inputComboBox.Text;
            Utility.ClassSerializer.WriteXML(audioSettings, configDirectory + @"\config.xml");
        }

        private void outputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            audioSettings.OutputDevice = outputComboBox.Text;
            Utility.ClassSerializer.WriteXML(audioSettings, configDirectory + @"\config.xml");
        }
    }
}
