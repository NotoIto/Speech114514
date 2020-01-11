using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace NotoIto.App.Speech114514.Config
{
    [System.Serializable]
    public class AudioSettingsModel
    {
        public string InputDevice = "";
        public string OutputDevice = "";
    }
}
