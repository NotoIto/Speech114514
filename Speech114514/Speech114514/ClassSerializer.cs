using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using Optional;

namespace NotoIto.Utility
{
    public static class ClassSerializer
    {
        public static Option<Type> ReadXML<Type>(string fileName)
        {
            string xmlDir = new FileInfo(fileName).Directory.FullName;
            Type model;
            autoCreateDir(xmlDir);
            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            FileStream fs = null;
            try
            {
                fs = new FileStream(xmlDir + fileName, FileMode.Open);
                model = (Type)serializer.Deserialize(fs);
            }
            catch
            {
                return default(Type).None();
            }
            finally
            {
                fs?.Close();
            }
            return model.SomeNotNull();
        }

        public static bool WriteXML<Type>(Type model, string fileName)
        {
            string xmlDir = new FileInfo(fileName).Directory.FullName;
            autoCreateDir(xmlDir);
            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(xmlDir + fileName, FileMode.Create);
                sw = new StreamWriter(fs, Encoding.UTF8);
                serializer.Serialize(sw, model);
            }
            catch
            {
                return false;
            }
            finally
            {
                sw?.Close();
                fs?.Close();
            }
            return true;
        }

        public static Option<Type> ReadJSON<Type>(string fileName)
        {
            string jsonDir = new FileInfo(fileName).Directory.FullName;
            Type model;
            autoCreateDir(jsonDir);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Type));
            FileStream fs = null;
            try
            {
                fs = new FileStream(jsonDir + fileName, FileMode.Open);
                model = (Type)serializer.ReadObject(fs);
            }
            catch
            {
                return default(Type).None();
            }
            finally
            {
                fs?.Close();
            }
            return model.SomeNotNull();
        }

        public static bool WriteJSON<Type>(Type model, string fileName)
        {
            string jsonDir = new FileInfo(fileName).Directory.FullName;
            autoCreateDir(jsonDir);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Type));
            FileStream fs = null;
            try
            {
                fs = new FileStream(jsonDir + fileName, FileMode.Create);
                serializer.WriteObject(fs, model);
            }
            catch
            {
                return false;
            }
            finally
            {
                fs?.Close();
            }
            return true;
        }

        public static void autoCreateDir(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public static bool stringToBool(string str)
        {
            return str == "True";
        }
    }
}