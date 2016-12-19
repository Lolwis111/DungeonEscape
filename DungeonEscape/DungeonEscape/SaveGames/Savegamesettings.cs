﻿using System;
using System.IO;
using System.Text;
using System.Xml;
using DungeonEscape.Debug;

namespace DungeonEscape.SaveGames
{
    public sealed class Savegamesettings
    {
        public static void Save(Savegamesettings settings)
        {
            StringBuilder builder = new StringBuilder(250);

            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            builder.Append("<settings>\n");
            builder.AppendFormat("\t<volume>{0}</volume>\n", settings.Volume);
            builder.AppendFormat("\t<fullscreen>{0}</fullscreen>", settings.Fullscreen ? "true" : "false");
            builder.AppendFormat("\t<resolution>{0};{1}</resolution>", settings.Resolution.X, settings.Resolution.Y);
            builder.AppendFormat("\t<textures>{0}</textures>", settings.UseLowTextures ? "low" : "normal");
            builder.Append("</settings>");

            StreamWriter writer = null;
            try
            {
                if (File.Exists($"{Environment.CurrentDirectory}/settings.xml"))
                    File.Delete($"{Environment.CurrentDirectory}/settings.xml");

                writer = new StreamWriter(File.Open($"{Environment.CurrentDirectory}/settings.xml", FileMode.CreateNew));
                writer.Write(builder.ToString());
            }
            catch (Exception ex)
            {
                LogWriter.WriteError(ex);
                System.Windows.Forms.MessageBox.Show("Fehler beim speichern!", "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                writer?.Dispose();
            }
        }

        public static Savegamesettings Load()
        {
            XmlDocument document = new XmlDocument();
            Savegamesettings settings = new Savegamesettings();

            if (File.Exists($"{Environment.CurrentDirectory}/settings.xml"))
            {
                try
                {
                    document.Load($"{Environment.CurrentDirectory}/settings.xml");

                    if (!float.TryParse(Utils.Utils.SelectSingleNode(document, "//volume").InnerText, out settings._volume))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    if (!bool.TryParse(Utils.Utils.SelectSingleNode(document, "//fullscreen").InnerText, out settings._fullscreen))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    int x, y;

                    if (!int.TryParse(Utils.Utils.SelectSingleNode(document, "//resolution").InnerText.Split(';')[0], out x))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    if (!int.TryParse(Utils.Utils.SelectSingleNode(document, "//resolution").InnerText.Split(';')[1], out y))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    settings.UseLowTextures = Utils.Utils.SelectSingleNode(document, "//textures").InnerText == "low";

                    settings.Resolution = new Resolution() { X = x, Y = y };
                }
                catch (Exception ex)
                {
                    LogWriter.WriteError(ex);

                    System.Windows.Forms.MessageBox.Show("Fehler beim laden!", "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    Basic.Game.Exit();
                }
            }
            else
            {
                settings.Volume = 0.0f;
                settings.Resolution = new Resolution { X = 1280, Y = 720 };
                settings.Fullscreen = false;
            }

            return settings;
        }

        public float Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        private float _volume;

        public bool Fullscreen
        {
            get { return _fullscreen; }
            set { _fullscreen = value; }
        }
        private bool _fullscreen;

        public Resolution Resolution { get; set; } = new Resolution(0, 0);

        public bool UseLowTextures { get; set; }
    }
}
