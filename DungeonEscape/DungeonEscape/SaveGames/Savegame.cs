﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using System.IO;
using DungeonEscape.Content;
using DungeonEscape.Entities;
using DungeonEscape.Entities.Block;
using DungeonEscape.Screens;
using DungeonEscape.GUI.Items;
using DungeonEscape.Security;

namespace DungeonEscape.SaveGames
{
    internal sealed class Savegamescore
    {
        #region Methods 

        public static void Save(Savegamescore saveGame)
        {
            #region XML Datei generieren

            //Speicher für mindestens 12000 Zeichen reservieren
            StringBuilder builder = new StringBuilder(12000);

            builder.AppendFormat("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n");
            builder.AppendFormat("<savegame>\n");
            builder.AppendFormat("\t<levelID>{0}</levelID>\n", saveGame.LevelNumber);
            builder.AppendFormat("\t<position>{0};{1};{2}</position>\n", saveGame.PlayerPosition.X,
                saveGame.PlayerPosition.Y, saveGame.PlayerPosition.Z);

            int p = 0, k = 0, p2 = 0, e = 0;
            builder.AppendFormat("\t<items>\n");
            foreach (Item item in saveGame.Items)
            {
                builder.AppendFormat("\t\t<item>\n");

                switch (item.Type)
                {
                    case ItemType.Key:
                        builder.AppendFormat("\t\t\t<type>key</type>\n");
                        builder.AppendFormat("\t\t\t<id>{0}</id>\n", item.Id);
                        k++;
                        break;
                    case ItemType.Pickaxe:
                        builder.AppendFormat("\t\t\t<type>pickaxe</type>\n");
                        p2++;
                        break;
                    case ItemType.Pliers:
                        builder.AppendFormat("\t\t\t<type>pliers</type>\n");
                        p++;
                        break;
                    case ItemType.None:
                        builder.AppendFormat("\t\t\t<type>empty</type>\n");
                        e++;
                        break;
                    case ItemType.Message:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                builder.AppendFormat("\t\t</item>\n");
            }

            int iId = NativeMethods.checkItems(p, k, p2, e);

            /*Console.WriteLine("ARGS:   {0};{1};{2};{3}", p, k, p2, e);
            Console.WriteLine("ItemID: {0}", iID);*/

            builder.AppendFormat("\t\t<ISECU>{0}</ISECU>\n", iId);
            builder.AppendFormat("\t</items>\n");

            #region Entities

            int eC = 0, bC = 0, sC = 0;

            builder.AppendFormat("\t<level>\n");
            foreach (Entity ent in saveGame.Entities)
            {
                builder.Append(ent.GenerateXml());

                switch (ent.EntityType)
                {
                    case EntityType.DestroyAbleBlock:
                    case EntityType.DoorBlock:
                    case EntityType.GridBlock:
                    case EntityType.HalfBlock:
                    case EntityType.WallBlock:
                    case EntityType.SwitchBlock:
                        bC++;
                        break;
                    case EntityType.LevelDownSprite:
                    case EntityType.LevelUpSprite:
                    case EntityType.MessageSprite:
                        sC++;
                        break;
                    case EntityType.PickAxeSprite:
                    case EntityType.KeySprite:
                    case EntityType.PliersSprite:
                        eC++;
                        break;
                    default:
                    case EntityType.Undefined:
                        eC++;
                        break;
                }

                #region OLD

                /*
                                if (ent is WallBlock)
                                {
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>wallblock</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t</entity>\n");
                                    bC++;
                                }
                                else if (ent is HalfBlock)
                                {
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>halfblock</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t</entity>\n");
                                    bC++;
                                }
                                else if (ent is DestroyBlock)
                                {
                                    //
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
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>key</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t\t<id>{0}</id>", ((Key)ent).Id);
                                    builder.AppendFormat("\t\t</entity>\n");
                                    sC++;
                                }
                                else if (ent is Pliers)
                                {
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>pliers</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t</entity>\n");
                                    sC++;
                                }
                                else if (ent is PickAxe)
                                {
                                    //
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
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>doorblock</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t\t<id>{0}</id>", ((DoorBlock)ent).Id);
                                    builder.AppendFormat("\t\t</entity>\n");
                                    sC++;
                                }
                                else if (ent is SwitchBlock)
                                {
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>switch</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t\t<id>{0}</id>", ((SwitchBlock)ent).Id);
                                    builder.AppendFormat("\t\t</entity>\n");
                                    bC++;
                                }
                                else if (ent is GridBlock)
                                {
                                    //
                                    builder.AppendFormat("\t\t<entity>\n");
                                    builder.AppendFormat("\t\t\t<type>gridblock</type>\n");
                                    builder.AppendFormat("\t\t\t<position>{0};{1};{2}</position>\n", ent.Position.X, ent.Position.Y, ent.Position.Z);
                                    builder.AppendFormat("\t\t\t<_destroyed>{0}</_destroyed>\n", ((GridBlock)ent)._destroyed ? "true" : "false");
                                    builder.AppendFormat("\t\t</entity>\n");
                                    sC++;
                                }
                                else eC++;*/

                #endregion
            }

            builder.AppendFormat("\t</level>\n");

            Console.WriteLine("ARGS:  {0};{1};{2}", eC, bC, sC);
            Console.WriteLine("MapID: {0}", NativeMethods.checkLevel(eC, bC, sC));

            //builder.AppendFormat("\t<SECU>{0}</SECU>", NativeMethods.checkLevel(eC, bC, sC));

            #endregion

            builder.AppendFormat("</savegame>");

            #endregion

            string fileName = GetFileName(saveGame.SaveState);

            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DungeonEscape");
            string path = Path.Combine(dirPath, fileName);

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            if (File.Exists(path))
                File.Delete(path);

            using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(builder.ToString());
            }
        }

        private static string GetFileName(SaveState state)
        {
            switch (state)
            {
                case SaveState.One:
                    return "save1.xml";
                case SaveState.Two:
                    return "save2.xml";
                case SaveState.Three:
                    return "save3.xml";
                case SaveState.Four:
                    return "save4.xml";
                case SaveState.ByPass:
                    return "bypass.xml";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static Savegamescore Load(SaveState state)
        {
            XmlDocument document = new XmlDocument();
            Savegamescore save = new Savegamescore {SaveState = state};

            string fileName = GetFileName(save.SaveState);

            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "DungeonEscape");
            string path = Path.Combine(dirPath, fileName);

            if (!Directory.Exists(dirPath) || !File.Exists(path)) return save;

            #region Parse

            document.Load(path);

            if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(document, "//levelID").InnerText, out save._levelNumber))
                //throw new InvalidDataException("Die Datei save.xml scheint beschädigt zu sein!");
                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

            string[] coordinateStrings = Utils.Utils.SaveSelectSingleNode(document, "savegame/position").InnerText.Split(';');

            float x, y, z;
            if (!float.TryParse(coordinateStrings[0], out x))
                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

            if (!float.TryParse(coordinateStrings[1], out y))
                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

            if (!float.TryParse(coordinateStrings[2], out z))
                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

            save.PlayerPosition = new Vector3(x, y, z);

            XmlNode itemNode = Utils.Utils.SaveSelectSingleNode(document, "//items");

            if(itemNode == null)
                throw new NullReferenceException();

            XmlNodeList nodes = itemNode.SelectNodes("//item");

            if(nodes == null)
                throw new NullReferenceException();

            int p = 0, k = 0, p2 = 0, e = 0;

            for (int i = 0; i < 6; i++)
            {
                string type = Utils.Utils.SaveSelectSingleNode(nodes[i], "//type").InnerText;

                switch (type)
                {
                    case "key":
                        save.Items[i] = new Item {Type = ItemType.Key};

                        int id;
                        if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(nodes[i], "//id").InnerText, out id))
                            //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                            throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                        save.Items[i].Id = id;
                        k++;
                        break;
                    case "pickaxe":
                        save.Items[i] = new Item {Type = ItemType.Pickaxe};
                        p2++;
                        break;
                    case "pliers":
                        save.Items[i] = new Item {Type = ItemType.Pliers};
                        p++;
                        break;
                    default:
                        save.Items[i] = new Item {Type = ItemType.None};
                        e++;
                        break;
                }
            }

            /*Console.WriteLine("ARGS:   {0};{1};{2};{3}", p, k, p2, e);
                Console.WriteLine("ItemID: {0}", NativeMethods.checkItems(p, k, p2, e));*/

            int iid;
            if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(itemNode, "//ISECU").InnerText, out iid))
                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

            if (iid != NativeMethods.checkItems(p, k, p2, e))
            {
                System.Windows.Forms.MessageBox.Show("Speicherstand wurde verändert!\nDas Spiel wird nicht geladen!",
                    "SECURITY ERROR");
                Basic.SetScreen(new MainMenuScreen());


                return null;
            }

            nodes = document.SelectNodes("//level/entity");

            int eC = 0, bC = 0, sC = 0;

            for (int i = 0; i < nodes?.Count - 1; i++)
            {
                coordinateStrings = Utils.Utils.SaveSelectSingleNode(nodes[i], "position").InnerText.Split(';');

                if (!float.TryParse(coordinateStrings[0], out x))
                    //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                    throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                if (!float.TryParse(coordinateStrings[1], out y))
                    //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                    throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                if (!float.TryParse(coordinateStrings[2], out z))
                    //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                    throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                string type = Utils.Utils.SaveSelectSingleNode(nodes[i], "type").InnerText;
                Entity ent;

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
                        int id;
                        if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(nodes[i], "id").InnerText, out id))
                                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                            ent = new Key(x, y, z) {Id = id};
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
                        ent = new Message(x, y, z) {Text = Utils.Utils.SaveSelectSingleNode(nodes[i], "text").InnerText};
                        save.Entities.Add(ent);

                        sC++;

                        break;
                    }
                    case "doorblock":
                    {
                        int id;
                        if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(nodes[i], "id").InnerText, out id))
                                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                            ent = new DoorBlock(x, y, z) {Id = id};
                        save.Entities.Add(ent);

                        sC++;

                        break;
                    }
                    case "gridblock":
                    {
                        ent = new GridBlock(x, y, z);

                        if (
                            !bool.TryParse(Utils.Utils.SaveSelectSingleNode(nodes[i], "_destroyed").InnerText,
                                out ((GridBlock) ent).Destroyed))
                                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                            save.Entities.Add(ent);

                        sC++;

                        break;
                    }
                    case "switch":
                    {
                        int id;
                        if (!int.TryParse(Utils.Utils.SaveSelectSingleNode(nodes[i], "id").InnerText, out id))
                                //throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");
                                throw new InvalidDataException(LanguageStrings.ErrorStrings.BrokenLevel);

                            ent = new SwitchBlock(x, y, z) {Id = id};
                        save.Entities.Add(ent);

                        bC++;

                        break;
                    }
                    case "empty":
                        eC++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Console.WriteLine("ARGS:  {0};{1};{2}", eC, bC, sC);
            Console.WriteLine("MapID: {0}", NativeMethods.checkLevel(eC, bC, sC));

#if !DEBUG

                    int lid = NativeMethods.checkLevel(eC, bC, sC);
                    int lid2 = 0;
                    if (!int.TryParse(document.SelectSingleNode("//SECU").InnerText, out lid2))
                        throw new InvalidDataException("Die Datei scheint beschädigt zu sein!");

                    if (lid != lid2)
                    {
                        System.Windows.Forms.MessageBox.Show("Speicherstand wurde verändert!\nDas Spiel wird nicht geladen!", "SECURITY ERROR");
                        Basic.SetScreen(new MainMenuScreen());

                        return null;
                    }
#endif


            #endregion

            return save;
        }

        #endregion

        #region Fields

        public SaveState SaveState { get; set; } = SaveState.One;

        public Vector3 PlayerPosition { get; set; } = Vector3.Zero;

        public List<Entity> Entities { get; set; }

        public Item[] Items { get; set; } = new Item[6];

        public int LevelNumber
        {
            get { return _levelNumber; }
            set { _levelNumber = value; }
        }
        private int _levelNumber;

        #endregion
    }
}
