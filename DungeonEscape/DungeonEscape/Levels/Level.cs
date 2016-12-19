using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using DungeonEscape.Debug;
using DungeonEscape.Entities;
using DungeonEscape.Entities.Block;
using DungeonEscape.Screens;
using DungeonEscape.MapEnvironment;
using DungeonEscape.SaveGames;
using DungeonEscape.Security;

namespace DungeonEscape.Levels
{
	public sealed class Level
    {
        #region Fields

        public string Name { get; private set; } = string.Empty;

        public int LevelNumber { get; set; }

        public List<Entity> Entities { get; set; } = new List<Entity>();

        public List<Entity> ToRemove { get; set; } = new List<Entity>();

        private readonly XmlDocument _mapDocument = new XmlDocument();

        private readonly Floor _floor;
        private readonly Ceiling _ceiling;
        private int _mapWidth, _mapHeight;

        #endregion

        #region Methods

        public Level(int level)
		{
            string p = $"{Environment.CurrentDirectory}/{Basic.Content.RootDirectory}/Levels/level{level}.xml";

            if (File.Exists(p))
            {
                _mapDocument.Load(p);
            }
            else
            {
                LogWriter.WriteError(new LevelNotFoundException(
                    $"Das Level {level} konnte nicht geladen werden weil die Datei nicht existiert!"));
                Basic.Game.Exit();
            }

            SetUpMap();

			_floor = new Floor(_mapWidth, _mapHeight);
			_ceiling = new Ceiling(_mapWidth, _mapHeight);
			LevelNumber = level;
		}

		public void Update()
		{
            ToRemove = new List<Entity>();
            //_entities.Sort();

            for (short i = 0; i < Entities.Count; i++)
			{
                Entities[i].Update();
			}

		    if (ToRemove.Count <= 0) return;

		    float cameraDistance = ToRemove[0].CameraDistance;
		    Entity item = ToRemove[0];
                
		    for (int j = 1; j < ToRemove.Count; j++)
		    {
		        if (!(ToRemove[j].CameraDistance < cameraDistance)) continue;

		        cameraDistance = ToRemove[j].CameraDistance;
		        item = ToRemove[j];
		    }

		    Entities.Remove(item);
		}

		public void Render()
		{
			_floor.Render();
			_ceiling.Render();

            Entities.Sort();

            foreach (Entity entity in Entities)
            {
                if (GameScreen.Camera.Frustum.Intersects(entity.Box))
                {
                    entity.Render();
                }
            }
		}

        private void SetUpMap()
        {
            string size = Utils.Utils.SelectSingleNode(_mapDocument, "//size").InnerText;

            if (int.TryParse(size.Split(';')[0], out _mapWidth))
            {
                if (int.TryParse(size.Split(';')[1], out _mapHeight))
                {
                    Name = Utils.Utils.SelectSingleNode(_mapDocument, "//name").InnerText;

                    XmlNodeList nodes = _mapDocument.SelectNodes("//entity");

                    if (nodes == null)
                        throw new NullReferenceException();

                    int eC = 0, bC = 0, sC = 0;

                    for (int i = 0; i < nodes.Count; i++)
                    {
                        string[] coordinates = Utils.Utils.SelectSingleNode(nodes[i], "position").InnerText.Split(';');
                        int x;
                        if (int.TryParse(coordinates[0], out x))
                        {
                            int y;
                            if (int.TryParse(coordinates[1], out y))
                            {
                                int z;
                                if (int.TryParse(coordinates[2], out z))
                                {
                                    string type = Utils.Utils.SelectSingleNode(nodes[i], "type").InnerText;

                                    switch (type)
                                    {
                                        case "wallblock":
                                            Entities.Add(new WallBlock(x, y, z));
                                            bC++;
                                            break;
                                        case "spawn":
                                            GameScreen.Camera.Position = new Vector3(x, y, z);
                                            break;
                                        case "levelup":
                                            Entities.Add(new LevelUp(x, y, z));
                                            sC++;
                                            break;
                                        case "leveldown":
                                            Entities.Add(new LevelDown(x, y, z));
                                            sC++;
                                            break;
                                        case "key":
                                            Entities.Add(new Key(x, y, z)
                                            {
                                                Id = int.Parse(Utils.Utils.SelectSingleNode(nodes[i], "id").InnerText)
                                            });
                                            sC++;
                                            break;
                                        case "pliers":
                                            Entities.Add(new Pliers(x, y, z));
                                            sC++;
                                            break;
                                        case "pickaxe":
                                            Entities.Add(new PickAxe(x, y, z));
                                            sC++;
                                            break;
                                        case "message":
                                            Entities.Add(new Message(x, y, z)
                                            {
                                                Text = Utils.Utils.SelectSingleNode(nodes[i], "text").InnerText
                                            });
                                            sC++;
                                            break;
                                        case "destroyblock":
                                            Entities.Add(new DestroyBlock(x, y, z));
                                            bC++;
                                            break;
                                        case "doorblock":
                                            Entities.Add(new DoorBlock(x, y, z)
                                            {
                                                Id = int.Parse(Utils.Utils.SelectSingleNode(nodes[i], "id").InnerText)
                                            });
                                            sC++;
                                            break;
                                        case "gridblock":
                                            Entities.Add(new GridBlock(x, y, z));
                                            sC++;
                                            break;
                                        case "switch":
                                            Entities.Add(new SwitchBlock(x, y, z)
                                            {
                                                Id = int.Parse(Utils.Utils.SelectSingleNode(nodes[i], "id").InnerText)
                                            });
                                            bC++;
                                            break;
                                        case "halfblock":
                                            Entities.Add(new HalfBlock(x, y, z));
                                            bC++;
                                            break;
                                        case "empty":
                                            eC++;
                                            break;
                                        default:
                                            throw new FormatException($"{type} ist kein gültiger Typ für einen Block!");
                                    }
                                }
                                else
                                {
                                    throw new FormatException($"{coordinates[0]} ist kein gültiger Wert für Z!");
                                }
                            }
                            else
                            {
                                throw new FormatException($"{coordinates[1]} ist kein gültiger Wert für Y!");
                            }
                        }
                        else
                        {
                            throw new FormatException($"{coordinates[2]} ist kein gültiger Wert für X!");
                        }
                    }

                    int n = NativeMethods.checkLevel(eC, bC, sC);
                    int n1 = int.Parse(Utils.Utils.SelectSingleNode(_mapDocument, "//SECU").InnerText);

                    if (n == n1) return;

                    System.Windows.Forms.MessageBox.Show(
                        "Die Checksumme stimmt nicht überein!\nLevel wird nicht geladen!", "SECURITYERROR");
                    Basic.SetScreen(new MainMenuScreen());
                }
                else
                {
                    throw new FormatException($"{size.Split(';')[1]} ist kein gültiger Wert für die Höhe!");
                }
            }
            else
            {

                throw new FormatException($"{size.Split(';')[0]} ist kein gültiger Wert für die Breite!");
            }
        }

        public void Init()
        {
            for (short i = 0; i < Entities.Count; i++)
                Entities[i].Init();
        }

		public Entity GetEntity(float x, float z)
		{
            for (short i = 0; i < Entities.Count - 1; i++)
            {
                if (Utils.Utils.CompareFloats(Entities[i].Position.X, x) 
                    && Utils.Utils.CompareFloats(Entities[i].Position.Z, z))
                    return Entities[i];
            }

			return null;
        }

        #endregion
    }
}
