using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    public static class BasicIO
    {
        public static void SaveTextureToPng(Texture2D texture, string fileName, string subfolder = "Resources")
        {
            var bytes = texture.EncodeToPNG();
            var file = File.Open(Application.dataPath + "/"+ subfolder + "/" + fileName + ".png", FileMode.Create);
            var binary = new BinaryWriter(file);
            binary.Write(bytes);
            binary.Close();
            file.Close();
        }

        public static Texture2D LoadTextureFromPng(string filename, string subfolder = "Resources")
        {
            if (!File.Exists(Application.dataPath + "/" + subfolder + "/" + filename + ".png")) return null;
   
            var fileData = File.ReadAllBytes(Application.dataPath + "/" + subfolder + "/" + filename + ".png");
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            return tex;
        }

        public static void SaveToFile(string data, string filename, string type = ".txt", string subfolder = "Resources")
        {
            StreamWriter writer = new StreamWriter(Application.dataPath + "/" + subfolder + "/" + filename + type);
            writer.Write(data);
            writer.Close();
        }

        public static string ReadFromFile(string filename, string type = ".txt", string subfolder = "Resources")
        {
            if (!File.Exists(Application.dataPath + "/" + subfolder + "/" + filename + type)) return null;
            StreamReader reader = new StreamReader(Application.dataPath + "/" + subfolder + "/" + filename + type);
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }
    }
}
