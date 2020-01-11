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
        public static Option<Type> ReadXML<Type>(string file)
        {
            string xmlDir = new FileInfo(file).Directory.FullName;
            string fileName = new FileInfo(file).Name;
            Type model;
            autoCreateDir(xmlDir);
            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            FileStream fs = null;
            try
            {
                fs = new FileStream(Path.Combine(xmlDir, fileName), FileMode.Open);
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

        public static bool WriteXML<Type>(Type model, string file)
        {
            string xmlDir = new FileInfo(file).Directory.FullName;
            string fileName = new FileInfo(file).Name;
            autoCreateDir(xmlDir);
            XmlSerializer serializer = new XmlSerializer(typeof(Type));
            FileStream fs = null;
            StreamWriter sw = null;
            try
            {
                fs = new FileStream(Path.Combine(xmlDir, fileName), FileMode.Create);
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

        public static Option<Type> ReadJSON<Type>(string file)
        {
            string jsonDir = new FileInfo(file).Directory.FullName;
            string fileName = new FileInfo(file).Name;
            Type model;
            autoCreateDir(jsonDir);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Type));
            FileStream fs = null;
            try
            {
                fs = new FileStream(Path.Combine(jsonDir, fileName), FileMode.Open);
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

        public static bool WriteJSON<Type>(Type model, string file)
        {
            string jsonDir = new FileInfo(file).Directory.FullName;
            string fileName = new FileInfo(file).Name;
            autoCreateDir(jsonDir);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Type));
            FileStream fs = null;
            try
            {
                fs = new FileStream(Path.Combine(jsonDir, fileName), FileMode.Create);
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