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

            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            builder.Append("<settings>\n");
            builder.AppendFormat("{1}<volume>{0}</volume>\n", settings.Volume, space4);
            builder.AppendFormat("{1}<fullscreen>{0}</fullscreen>", settings.Fullscreen ? "true" : "false", space4);
            builder.AppendFormat("{1}<resolution>{0};{1}</resolution>", settings.Resolution.X, settings.Resolution.Y, space4);
            builder.AppendFormat("{1}<textures>{0}</textures>", settings.UseLowTextures ? "low" : "normal", space4);
            builder.AppendFormat("{1}<language>{0}</language>", settings.Language, space4);
            builder.Append("</settings>");

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "settings.xml")))
                File.Delete(Path.Combine(Environment.CurrentDirectory, "settings.xml"));

            using (StreamWriter writer = new StreamWriter(File.Open(Path.Combine(Environment.CurrentDirectory, "settings.xml"), FileMode.CreateNew)))
            {
                writer.Write(builder.ToString());
            }
        }

        public static Savegamesettings Load()
        {
            XmlDocument document = new XmlDocument();
            Savegamesettings settings = new Savegamesettings();

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "settings.xml")))
            {
                document.Load(Path.Combine(Environment.CurrentDirectory, "settings.xml"));

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

                settings.Resolution = new Resolution {X = x, Y = y};

                settings.Language = Utils.Utils.SaveSelectSingleNode(document, "//language").InnerText;
            }
            else
            {
                settings.Volume = 0.0f;
                settings.Resolution = new Resolution { X = 1280, Y = 720 };
                settings.Fullscreen = false;
                settings.Language = "english";
            }

            return settings;
        }

        public float Volume;
        public bool Fullscreen;
        public Resolution Resolution;
        public bool UseLowTextures;
        public string Language;
    }
}
