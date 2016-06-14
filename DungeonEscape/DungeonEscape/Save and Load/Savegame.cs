using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;
using System.IO;

namespace DungeonEscape
{
    public sealed class Savegamescore
    {
        public static void Save(Savegamescore saveGame)
        {
            #region XML Datei generieren

            //Speicher für mindestens 12000 Zeichen reservieren
            StringBuilder builder = new StringBuilder(12000);

            builder.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            builder.AppendFormat("<savegame>\n");
            builder.AppendFormat("\t<levelID>{0}</levelID>\n", saveGame.LevelNumber);
            builder.AppendFormat("\t<position>{0};{1};{2}</position>\n", saveGame.PlayerPosition.X, saveGame.PlayerPosition.Y, saveGame.PlayerPosition.Z);

            int p = 0, k = 0, p2 = 0, e = 0;
            builder.AppendFormat("\t<items>\n");
            foreach (Item item in saveGame.Items)
            {
                builder.AppendFormat("\t\t<item>\n");

                if (item.Type == ItemType.Key)
                {
                    builder.AppendFormat("\t\t\t<type>key</type>\n");
                    builder.AppendFormat("\t\t\t<id>{0}</id>\n", item.ID);
                    k++;
                }
                else if (item.Type == ItemType.Pickaxe)
                {
                    builder.AppendFormat("\t\t\t<type>pickaxe</type>\n");
                    p2++;
                }
                else if (item.Type == ItemType.Pliers)
                {
                    builder.AppendFormat("\t\t\t<type>pliers</type>\n");
                    p++;
                }
                else if (item.Type == ItemType.None)
                {
                    builder.AppendFormat("\t\t\t<type>empty</type>\n");
                    e++;
                }

                builder.AppendFormat("\t\t</item>\n");
            }

            int iID = NativeMethods.checkItems(p, k, p2, e);

            /*Console.WriteLine("ARGS:   {0};{1};{2};{3}", p, k, p2, e);
            Console.WriteLine("ItemID: {0}", iID);*/

            builder.AppendFormat("\t\t<ISECU>{0}</ISECU>\n", iID);
            builder.AppendFormat("\t</items>\n");

            #region Entities

            int eC = 0, bC = 0, sC = 0;

            builder.AppendFormat("\t<level>\n");
            foreach (Entity ent in saveGame.Entities)
            {
                if (ent is WallBlock)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>wallblock</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    bC++;
                }
                else if (ent is HalfBlock)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>halfblock</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    bC++;
                }
                else if (ent is DestroyBlock)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>destroyblock</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    bC++;
                }
                else if (ent is LevelUp)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>levelup</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is LevelDown)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>leveldown</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is Key)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>key</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t\t<id>{0}</id>", ((Key)ent).ID);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is Pliers)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>pliers</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is PickAxe)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>pickaxe</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is Message)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>message</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t\t<text>{0}</text>\n", ((Message)ent).Text);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is DoorBlock)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>doorblock</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t\t<id>{0}</id>", ((DoorBlock)ent).ID);
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else if (ent is SwitchBlock)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>switch</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t\t<id>{0}</id>", ((SwitchBlock)ent).ID);
                    builder.AppendFormat("\t\t</entity>\n");
                    bC++;
                }
                else if (ent is GridBlock)
                {
                    builder.AppendFormat("\t\t<entity>\n");
                    builder.AppendFormat("\t\t\t<type>gridblock</type>\n");
                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                    builder.AppendFormat("\t\t\t<destroyed>{0}</destroyed>\n", ((GridBlock)ent).destroyed ? "true" : "false");
                    builder.AppendFormat("\t\t</entity>\n");
                    sC++;
                }
                else eC++;
            }
            builder.AppendFormat("\t</level>\n");

            Console.WriteLine("ARGS:  {0};{1};{2}", eC, bC, sC);
            Console.WriteLine("MapID: {0}", NativeMethods.checkLevel(eC, bC, sC));

            //builder.AppendFormat("\t<SECU>{0}</SECU>", NativeMethods.checkLevel(eC, bC, sC));

            #endregion

            builder.AppendFormat("</savegame>");

            #endregion

            StreamWriter writer = null;
            try
            {

                string fileName = string.Empty;
                switch (saveGame._saveState)
                {
                    case SaveState.One:
                        fileName = "save1.xml";
                        break;
                    case SaveState.Two:
                        fileName = "save2.xml";
                        break;
                    case SaveState.Three:
                        fileName = "save3.xml";
                        break;
                    case SaveState.Four:
                        fileName = "save4.xml";
                        break;
                    case SaveState.ByPass:
                        fileName = "bypass.xml";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (File.Exists(Path.Combine(Environment.CurrentDirectory, fileName)))
                    File.Delete(Path.Combine(Environment.CurrentDirectory, fileName));

                writer = new StreamWriter(File.Open(Path.Combine(Environment.CurrentDirectory, fileName), FileMode.Create));
                writer.Write(builder.ToString());
            }
            catch (Exception ex)
            {
                LogWriter.WriteError(ex);
                System.Windows.Forms.MessageBox.Show("Fehler beim speichern!", "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Dispose();
                    writer = null;
                }
            }
        }

        public static Savegamescore Load(SaveState state)
        {
            XmlDocument document = new XmlDocument();
            Savegamescore save = new Savegamescore();
            save._saveState = state;

            string fileName = string.Empty;
            switch (save._saveState)
            {
                case SaveState.One:
                    fileName = "save1.xml";
                    break;
                case SaveState.Two:
                    fileName = "save2.xml";
                    break;
                case SaveState.Three:
                    fileName = "save3.xml";
                    break;
                case SaveState.Four:
                    fileName = "save4.xml";
                    break;
                case SaveState.ByPass:
                    fileName = "bypass.xml";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #region Parse

            if (File.Exists(Path.Combine(Environment.CurrentDirectory, fileName)))
            {
                try
                {
                    document.Load(Path.Combine(Environment.CurrentDirectory, fileName));

                    if (!int.TryParse(document.SelectSingleNode("//levelID").InnerText, out save._levelNumber))
                        throw new InvalidDataException("Die Datei save.xml scheint beschädigt zu sein!");

                    string[] coordinateStrings = document.SelectSingleNode("savegame/position").InnerText.Split(';');

                    float x = 0, y = 0, z = 0;
                    if (!float.TryParse(coordinateStrings[0], out x))
                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                    if (!float.TryParse(coordinateStrings[1], out y))
                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                    if (!float.TryParse(coordinateStrings[2], out z))
                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                    save._playerPosition = new Vector3(x, y, z);

                    XmlNode itemNode = document.SelectSingleNode("//items");
                    XmlNodeList nodes = itemNode.SelectNodes("//item");

                    int p = 0, k = 0, p2 = 0, e = 0, i = 0;

                    for (i = 0; i < 6; i++)
                    {
                        string type = nodes[i].SelectSingleNode("//type").InnerText;

                        if (type == "key")
                        {
                            save._items[i] = new Item();
                            save._items[i].Type = ItemType.Key;
                            int id = 0;
                            if (!int.TryParse(nodes[i].SelectSingleNode("//id").InnerText, out id))
                                throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                            save._items[i].ID = id;
                            k++;
                        }
                        else if (type == "pickaxe")
                        {
                            save._items[i] = new Item();
                            save._items[i].Type = ItemType.Pickaxe;
                            p2++;
                        }
                        else if (type == "pliers")
                        {
                            save._items[i] = new Item();
                            save._items[i].Type = ItemType.Pliers;
                            p++;
                        }
                        else
                        {
                            save._items[i] = new Item();
                            save._items[i].Type = ItemType.None;
                            e++;
                        }
                    }

                    /*Console.WriteLine("ARGS:   {0};{1};{2};{3}", p, k, p2, e);
                    Console.WriteLine("ItemID: {0}", NativeMethods.checkItems(p, k, p2, e));*/

                    int iid = 0;
                    if (!int.TryParse(itemNode.SelectSingleNode("//ISECU").InnerText, out iid))
                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                    if (iid != NativeMethods.checkItems(p, k, p2, e))
                    {
                        System.Windows.Forms.MessageBox.Show("Speicherstand wurde verändert!\nDas Spiel wird nicht geladen!", "SECURITY ERROR");
                        Basic.setScreen(new MainMenuScreen());

                        document = null;
                        itemNode = null;
                        nodes = null;
                        return null;
                    }

                    nodes = document.SelectNodes("//level/entity");
                    Entity ent = null;

                    int eC = 0, bC = 0, sC = 0;

                    for (i = 0; i < nodes.Count - 1; i++)
                    {
                        coordinateStrings = nodes[i].SelectSingleNode("position").InnerText.Split(';');

                        if (!float.TryParse(coordinateStrings[0], out x))
                            throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                        if (!float.TryParse(coordinateStrings[1], out y))
                            throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                        if (!float.TryParse(coordinateStrings[2], out z))
                            throw new InvalidDataException("Die Datei save.xml scheint beschädigt zu sein!");

                        string type = nodes[i].SelectSingleNode("type").InnerText;
                        switch (type)
                        {
                            case "wallblock":
                                {
                                    ent = new WallBlock(x, y, z);
                                    save.Entities.Add(ent);

                                    bC++;

                                    break;
                                }
                            case "halfblock":
                                {
                                    ent = new HalfBlock(x, y, z);
                                    save.Entities.Add(ent);

                                    bC++;

                                    break;
                                }
                            case "destroyblock":
                                {
                                    ent = new DestroyBlock(x, y, z);
                                    save.Entities.Add(ent);

                                    bC++;

                                    break;
                                }
                            case "levelup":
                                {
                                    ent = new LevelUp(x, y, z);
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "leveldown":
                                {
                                    ent = new LevelDown(x, y, z);
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "key":
                                {
                                    int id = 0;
                                    if (!int.TryParse(nodes[i].SelectSingleNode("id").InnerText, out id))
                                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                                    ent = new Key(x, y, z) { ID = id };
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "pliers":
                                {
                                    ent = new Pliers(x, y, z);
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "pickaxe":
                                {
                                    ent = new PickAxe(x, y, z);
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "message":
                                {
                                    ent = new Message(x, y, z) { Text = nodes[i].SelectSingleNode("text").InnerText };
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "doorblock":
                                {
                                    int id = 0;
                                    if (!int.TryParse(nodes[i].SelectSingleNode("id").InnerText, out id))
                                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                                    ent = new DoorBlock(x, y, z) { ID = id };
                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "gridblock":
                                {
                                    ent = new GridBlock(x, y, z);

                                    if (!bool.TryParse(nodes[i].SelectSingleNode("destroyed").InnerText, out ((GridBlock)ent).destroyed))
                                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                                    save.Entities.Add(ent);

                                    sC++;

                                    break;
                                }
                            case "switch":
                                {
                                    int id = 0;
                                    if (!int.TryParse(nodes[i].SelectSingleNode("id").InnerText, out id))
                                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                                    ent = new SwitchBlock(x, y, z) { ID = id };
                                    save.Entities.Add(ent);

                                    bC++;

                                    break;
                                }
                            case "empty":
                                eC++;
                                break;
                        }
                    }

                    Console.WriteLine("ARGS:  {0};{1};{2}", eC, bC, sC);
                    Console.WriteLine("MapID: {0}", NativeMethods.checkLevel(eC, bC, sC));

                    /*int lid = NativeMethods.checkLevel(eC, bC, sC);
                    int lid2 = 0;
                    if (!int.TryParse(document.SelectSingleNode("//SECU").InnerText, out lid2))
                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                    if (lid != lid2)
                    {
                        System.Windows.Forms.MessageBox.Show("Speicherstand wurde verändert!\nDas Spiel wird nicht geladen!", "SECURITY ERROR");
                        Basic.setScreen(new MainMenuScreen());

                        return null;
                    }*/

                }
                catch (Exception ex)
                {
                    LogWriter.WriteError(ex);
                    System.Windows.Forms.MessageBox.Show("Fehler beim laden!", "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                    Basic.Game.Exit();
                }
            }

            #endregion

            document = null;

            return save;
        }

        public SaveState SaveState
        {
            get { return _saveState; }
            set { _saveState = value; }
        }
        private SaveState _saveState = SaveState.One;

        public Vector3 PlayerPosition
        {
            get { return _playerPosition; }
            set { _playerPosition = value; }
        }
        private Vector3 _playerPosition = Vector3.Zero;

        public List<Entity> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }
        private List<Entity> _entities = new List<Entity>();

        public Item[] Items
        {
            get { return _items; }
            set { _items = value; }
        }
        private Item[] _items = new Item[6];

        public int LevelNumber
        {
            get { return _levelNumber; }
            set { _levelNumber = value; }
        }
        private int _levelNumber = 0;

        public Savegamescore()
        {

        }
    }

    public struct Resolution
    {
        public Resolution(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }
    
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
                if (File.Exists(string.Format("{0}/settings.xml", Environment.CurrentDirectory)))
                    File.Delete(string.Format("{0}/settings.xml", Environment.CurrentDirectory));

                writer = new StreamWriter(File.Open(string.Format("{0}/settings.xml", Environment.CurrentDirectory), FileMode.CreateNew));
                writer.Write(builder.ToString());
            }
            catch (Exception ex)
            {
                LogWriter.WriteError(ex);
                System.Windows.Forms.MessageBox.Show("Fehler beim speichern!", "Fehler", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Dispose();
                    writer = null;
                }
            }
        }

        public static Savegamesettings Load()
        {
            XmlDocument document = new XmlDocument();
            Savegamesettings settings = new Savegamesettings();

            if (File.Exists(string.Format("{0}/settings.xml", Environment.CurrentDirectory)))
            {
                try
                {
                    document.Load(string.Format("{0}/settings.xml", Environment.CurrentDirectory));

                    if (!float.TryParse(document.SelectSingleNode("//volume").InnerText, out settings._volume))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    if (!bool.TryParse(document.SelectSingleNode("//fullscreen").InnerText, out settings._fullscreen))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    int x = 0, y = 0;

                    if (!int.TryParse(document.SelectSingleNode("//resolution").InnerText.Split(';')[0], out x))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    if (!int.TryParse(document.SelectSingleNode("//resolution").InnerText.Split(';')[1], out y))
                        throw new InvalidDataException("Die Datei settings.xml scheint beschädigt zu sein!");

                    settings.UseLowTextures = document.SelectSingleNode("//textures").InnerText == "low";

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
                settings.Resolution = new Resolution() { X = 1280, Y = 720 };
                settings.Fullscreen = false;
            }

            document = null;

            return settings;
        }

        public float Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        private float _volume = 0.0f;

        public bool Fullscreen
        {
            get { return _fullscreen; }
            set { _fullscreen = value; }
        }
        private bool _fullscreen = false;

        public Resolution Resolution
        {
            get { return _resolution; }
            set { _resolution = value; }
        }
        private Resolution _resolution = new Resolution(0, 0);

        public bool UseLowTextures
        {
            get { return _lowTextures; }
            set { _lowTextures = value; }
        }
        private bool _lowTextures = false;

        public Savegamesettings()
        {

        }
    }
}
