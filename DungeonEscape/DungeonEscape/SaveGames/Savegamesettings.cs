using System;
using System.IO;
using System.Text;
using System.Xml;

namespace DungeonEscape.SaveGames
{
    internal struct Savegamesettings
    {
        public static void Save(Savegamesettings settings)
        {
            StringBuilder builder = new StringBuilder(250);

            const string space4 = "    ";

            string fullscreen = settings.Fullscreen ? "true" : "false";
            string textures = settings.UseLowTextures ? "low" : "normal";

            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            builder.Append("<settings>\n");
                builder.AppendLine($"{space4}<volume>{settings.Volume}</volume>");
                builder.AppendLine($"{space4}<fullscreen>{fullscreen}</fullscreen>");
                builder.AppendLine($"{space4}<resolution>{settings.Resolution.X};{settings.Resolution.Y}</resolution>");
                builder.AppendLine($"{space4}<textures>{textures}</textures>");
                builder.AppendLine($"{space4}<language>{settings.Language}</language>");
            builder.Append("</settings>");

            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DungeonEscape");
            string path = Path.Combine(dirPath, "settings.xml");

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.CreateNew)))
            {
                writer.Write(builder.ToString());
            }
        }

        public static Savegamesettings Load()
        {
            XmlDocument document = new XmlDocument();
            Savegamesettings settings = new Savegamesettings();

            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DungeonEscape");
            string path = Path.Combine(dirPath, "settings.xml");

            if (!Directory.Exists(dirPath) || !File.Exists(path))
            {
                settings.Volume = 0.0f;
                settings.Resolution = new Resolution { X = 1280, Y = 720 };
                settings.Fullscreen = false;
                settings.Language = "english";
                settings.UseLowTextures = true;

                return settings;
            }

            document.Load(path);

            if (!float.TryParse(Utils.Utils.SaveSelectSingleNode(document, "//volume").InnerText, out settings.Volume))
                throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

            if (
                !bool.TryParse(Utils.Utils.SaveSelectSingleNode(document, "//fullscreen").InnerText,
                    out settings.Fullscreen))
                throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

            int x, y;

            if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(document, "//resolution").InnerText.Split(';')[0], out x))
                throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

            if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(document, "//resolution").InnerText.Split(';')[1], out y))
                throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

            settings.UseLowTextures = Utils.Utils.SaveSelectSingleNode(document, "//textures").InnerText == "low";

            settings.Resolution = new Resolution { X = x, Y = y };

            settings.Language = Utils.Utils.SaveSelectSingleNode(document, "//language").InnerText;

            return settings;
        }

        public float Volume;
        public bool Fullscreen;
        public Resolution Resolution;
        public bool UseLowTextures;
        public string Language;
    }
}
