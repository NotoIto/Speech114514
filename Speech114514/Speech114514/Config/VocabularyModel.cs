using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotoIto.App.Speech114514.Config
{
    [System.Serializable]
    public class VocabularyModel
    {
        public List<VocabularyElement> VocabularyElements = new List<VocabularyElement>();
    }
    [System.Serializable]
    public class VocabularyElement
    {
        public string Keyword = "キーワード";
        public string SoundFile= "音ファイル";
    }
}
