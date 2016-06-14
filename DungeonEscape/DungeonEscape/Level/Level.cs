using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Runtime.InteropServices;

namespace DungeonEscape
{
	public sealed class Level
    {
        #region Fields

        public string Name 
        {
            get { return _name; }
            private set { _name = value; }
        }
        private string _name = string.Empty;

        public int LevelNumber
        {
            get { return _levelNumber; }
            set { _levelNumber = value; }
        }
        private int _levelNumber = 0;

        public List<Entity> Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }
        private List<Entity> _entities = new List<Entity>();

        public List<Entity> ToRemove
        {
            get { return _toRemove; }
            set { _toRemove = value; }
        }
        private List<Entity> _toRemove = new List<Entity>();

        private XmlDocument mapDocument = new XmlDocument();

        private Floor floor;
        private Ceiling ceiling;
        private int mapWidth = 0, mapHeight = 0;

        #endregion

        #region Methods

        public Level(int level)
		{
            string p = string.Format("{0}/{1}/Levels/level{2}.xml", Environment.CurrentDirectory, Basic.Content.RootDirectory, level);

            if (File.Exists(p))
            {
                mapDocument.Load(p);
            }
            else
            {
                LogWriter.WriteError(new LevelNotFoundException(string.Format("Das Level {0} konnte nicht geladen werden weil die Datei nicht existiert!", level)));
                Basic.Game.Exit();
            }

            setUpMap();

			floor = new Floor(mapWidth, mapHeight);
			ceiling = new Ceiling(mapWidth, mapHeight);
			_levelNumber = level;
		}

		public void Update()
		{
            _toRemove = new List<Entity>();
            //_entities.Sort();

            for (short i = 0; i < _entities.Count; i++)
			{
                _entities[i].Update();
			}

            if (_toRemove.Count > 0)
			{
                float cameraDistance = _toRemove[0].CameraDistance;
                Entity item = _toRemove[0];
                
                for (int j = 1; j < _toRemove.Count; j++)
				{
                    if (_toRemove[j].CameraDistance < cameraDistance)
					{
                        cameraDistance = _toRemove[j].CameraDistance;
                        item = _toRemove[j];
					}
				}

                _entities.Remove(item);
			}
		}

		public void Render()
		{
			floor.Render();
			ceiling.Render();

            _entities.Sort();

            for (int i = 0; i < _entities.Count; i++)
			{
                if (GameScreen.Camera.Frustum.Intersects(_entities[i].Box))
                {
                    _entities[i].Render();
                }
			}
		}

        private void setUpMap()
        {
            try
            {
                string size = mapDocument.SelectSingleNode("//size").InnerText;

                if (int.TryParse(size.Split(';')[0], out mapWidth))
                {
                    if (int.TryParse(size.Split(';')[1], out mapHeight))
                    {
                        _name = mapDocument.SelectSingleNode("//name").InnerText;

                        XmlNodeList nodes = mapDocument.SelectNodes("//entity");

                        int eC = 0, bC = 0, sC = 0;

                        string[] coordinates = new string[3];
                        for (int i = 0; i < nodes.Count; i++)
                        {
                            coordinates = nodes[i].SelectSingleNode("position").InnerText.Split(';');
                            int x = 0, y = 0, z = 0;
                            if (int.TryParse(coordinates[0], out x))
                            {
                                if (int.TryParse(coordinates[1], out y))
                                {
                                    if (int.TryParse(coordinates[2], out z))
                                    {
                                        string type = nodes[i].SelectSingleNode("type").InnerText;

                                        switch (type)
                                        {
                                            case "wallblock":
                                                _entities.Add(new WallBlock(x, y, z));
                                                bC++;
                                                break;
                                            case "spawn":
                                                GameScreen.Camera.Position = new Vector3(x, y, z);
                                                break;
                                            case "levelup":
                                                _entities.Add(new LevelUp(x, y, z));
                                                sC++;
                                                break;
                                            case "leveldown":
                                                _entities.Add(new LevelDown(x, y, z));
                                                sC++;
                                                break;
                                            case "key":
                                                _entities.Add(new Key(x, y, z) { ID = int.Parse(nodes[i].SelectSingleNode("id").InnerText) });
                                                //_entities.Add(new Key(x, y, z) { ID = 0 });
                                                sC++;
                                                break;
                                            case "pliers":
                                                _entities.Add(new Pliers(x, y, z));
                                                sC++;
                                                break;
                                            case "pickaxe":
                                                _entities.Add(new PickAxe(x, y, z));
                                                sC++;
                                                break;
                                            case "message":
                                                _entities.Add(new Message(x, y, z) { Text = nodes[i].SelectSingleNode("text").InnerText });
                                                sC++;
                                                break;
                                            case "destroyblock":
                                                _entities.Add(new DestroyBlock(x, y, z));
                                                bC++;
                                                break;
                                            case "doorblock":
                                                _entities.Add(new DoorBlock(x, y, z) { ID = int.Parse(nodes[i].SelectSingleNode("id").InnerText) });
                                                //_entities.Add(new DoorBlock(x, y, z) { ID = 0 });
                                                sC++;
                                                break;
                                            case "gridblock":
                                                _entities.Add(new GridBlock(x, y, z));
                                                sC++;
                                                break;
                                            case "switch":
                                                _entities.Add(new SwitchBlock(x, y, z) { ID = int.Parse(nodes[i].SelectSingleNode("id").InnerText) });
                                                //_entities.Add(new SwitchBlock(x, y, z) { ID = 0 });
                                                bC++;
                                                break;
                                            case "halfblock":
                                                _entities.Add(new HalfBlock(x, y, z));
                                                bC++;
                                                break;
                                            case "empty":
                                                eC++;
                                                break;
                                            default:
                                                throw new FormatException(string.Format("{0} ist kein gültiger Typ für einen Block!", type));
                                        }
                                    }
                                    else
                                    {
                                        throw new FormatException(string.Format("{0} ist kein gültiger Wert für Z!", coordinates[0]));
                                    }
                                }
                                else
                                {
                                    throw new FormatException(string.Format("{0} ist kein gültiger Wert für Y!", coordinates[1]));
                                }
                            }
                            else
                            {
                                throw new FormatException(string.Format("{0} ist kein gültiger Wert für X!", coordinates[2]));
                            }
                        }

                        int n = NativeMethods.checkLevel(eC, bC, sC);
                        int n1 = int.Parse(mapDocument.SelectSingleNode("//SECU").InnerText);
                        if (n != n1)
                        {
                            System.Windows.Forms.MessageBox.Show("Die Checksumme stimmt nicht überein!\nLevel wird nicht geladen!", "SECURITYERROR");
                            Basic.setScreen(new MainMenuScreen());
                        }
                    }
                    else
                    {
                        throw new FormatException(string.Format("{0} ist kein gültiger Wert für die Höhe!", size.Split(';')[1]));
                    }
                }
                else
                {
                    
                    throw new FormatException(string.Format("{0} ist kein gültiger Wert für die Breite!", size.Split(';')[0]));
                }
            }
            catch(Exception ex)
            {
                LogWriter.WriteError(ex);
                Basic.Game.Exit();
            }
        }

        public void Init()
        {
            for (short i = 0; i < _entities.Count; i++)
                _entities[i].Init();
        }

		public Entity getEntity(float x, float z)
		{
            for (short i = 0; i < _entities.Count - 1; i++)
            {
                if (_entities[i].Position.X == x && _entities[i].Position.Z == z)
                    return _entities[i];
            }

			return null;
        }

        #endregion
    }

    [Serializable]
    public sealed class LevelNotFoundException : Exception
    {
        public LevelNotFoundException()
        {
        }

        public LevelNotFoundException(string message) : base(message)
        {
        }

        public LevelNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
