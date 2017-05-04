using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using DungeonEscape.Debug;
using DungeonEscape.Entities;
using DungeonEscape.Entities.Block;
using DungeonEscape.MapEnvironment;
using DungeonEscape.Models;
using DungeonEscape.SaveGames;
using DungeonEscape.Screens;
using DungeonEscape.Security;
using DungeonEscape.Exceptions;
using DungeonEscape.Particles;
using DungeonEscape.Content;

using Microsoft.Xna.Framework.Input;

namespace DungeonEscape.Levels
{
	internal sealed class Level
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
        private int _levelNumber;

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

        private readonly XmlDocument _mapDocument = new XmlDocument();

        private readonly Floor _floor;
        private readonly Ceiling _ceiling;

        private int _mapWidth, _mapHeight;
        public readonly ParticleEngine Particles = new ParticleEngine();

        #endregion

        #region Methods

        public Level(int level)
		{
            string path = Path.Combine(Environment.CurrentDirectory, Basic.Content.RootDirectory, "Levels", $"level{level}.xml");

            if (File.Exists(path))
            {
                _mapDocument.Load(path);
            }
            else
            {
                LogWriter.WriteError(new LevelNotFoundException(
                    $"Das Level {level} konnte nicht geladen werden weil die Datei nicht existiert!"));
                Basic.Game.Exit();
            }

            SetUpMap();

            VertexModel.Init(_mapWidth, _mapHeight);

			_floor = new Floor();

			_ceiling = new Ceiling();

			_levelNumber = level;

            Console.WriteLine("{0}x{1}", _mapWidth, _mapHeight);
		}

		public void Update()
		{
            _toRemove = new List<Entity>();

            for (short i = 0; i < _entities.Count; i++)
			{
                _entities[i].Update();
			}

            Particles.Update();

            if (_toRemove.Count <= 0) return;

		    float cameraDistance = _toRemove[0].CameraDistance;
		    Entity item = _toRemove[0];
                
		    for (int j = 1; j < _toRemove.Count; j++)
		    {
		        if (!(_toRemove[j].CameraDistance < cameraDistance)) continue;

		        cameraDistance = _toRemove[j].CameraDistance;
		        item = _toRemove[j];
		    }

		    _entities.Remove(item);
		}

		public void Render()
		{
            if(!Basic.DebugMode || (Basic.DebugMode && Debug.Debug.DrawFloor))
			    _floor.Render();

            if (!Basic.DebugMode || (Basic.DebugMode && Debug.Debug.DrawCeiling))
                _ceiling.Render();

            _entities.Sort();

            Particles.Render();

            foreach (Entity entity in _entities)
            {
                if (GameScreen.Camera.Frustum.Intersects(entity.Box))
                {
                    entity.Render();
                }
            }
        }

        private void SetUpMap()
        {
            string size = Utils.Utils.SaveSelectSingleNode(_mapDocument, "//size").InnerText;

            if (!int.TryParse(size.Split(';')[0], out _mapWidth))
            {
                throw new FormatException($"{size.Split(';')[0]} ist kein gültiger Wert für die Breite!");
            }

            if (!int.TryParse(size.Split(';')[1], out _mapHeight))
            {
                throw new FormatException($"{size.Split(';')[1]} ist kein gültiger Wert für die Höhe!");
            }

            Name = Utils.Utils.SaveSelectSingleNode(_mapDocument, "//name").InnerText;

            XmlNodeList nodes = _mapDocument.SelectNodes("//entity");

            if (nodes == null)
                throw new NullReferenceException();

            int eC = 0, bC = 0, sC = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                string[] coordinates =
                    Utils.Utils.SaveSelectSingleNode(nodes[i], "position").InnerText.Split(';');
                int x;
                if (!int.TryParse(coordinates[0], out x))
                {
                    throw new FormatException($"Invalid value X = {coordinates[2]}!");
                }

                int y;
                if (!int.TryParse(coordinates[1], out y))
                {
                    throw new FormatException($"Invalid value Y = {coordinates[1]}!");
                }

                int z;
                if (!int.TryParse(coordinates[2], out z))
                {
                    throw new FormatException($"Invalid value Z = {coordinates[0]}!");
                }

                string type = Utils.Utils.SaveSelectSingleNode(nodes[i], "type").InnerText;

                switch (type)
                {
                    case "wallblock":
                    {
                        _entities.Add(new WallBlock(x, y, z));
                        bC++;
                        break;
                    }
                    case "spawn":
                    {
                        GameScreen.Camera.Position = new Vector3(x, y, z);
                        break;
                    }
                    case "levelup":
                    {
                        _entities.Add(new LevelUp(x, y, z));
                        sC++;
                        break;
                    }
                    case "leveldown":
                    {
                        _entities.Add(new LevelDown(x, y, z));
                        sC++;
                        break;
                    }
                    case "key":
                    {
                        _entities.Add(new Key(x, y, z)
                        {
                            Id =
                                int.Parse(Utils.Utils.SaveSelectSingleNode(nodes[i], "id").InnerText)
                        });
                        sC++;
                        break;
                    }
                    case "pliers":
                    {
                        _entities.Add(new Pliers(x, y, z));
                        sC++;
                        break;
                    }
                    case "pickaxe":
                    {
                        _entities.Add(new PickAxe(x, y, z));
                        sC++;
                        break;
                    }
                    case "message":
                    {
                        _entities.Add(new Message(x, y, z)
                        {
                            Text = Utils.Utils.SaveSelectSingleNode(nodes[i], "text").InnerText
                        });
                        sC++;
                        break;
                    }
                    case "destroyblock":
                    {
                        _entities.Add(new DestroyBlock(x, y, z));
                        bC++;
                        break;
                    }
                    case "doorblock":
                    {
                        _entities.Add(new DoorBlock(x, y, z)
                        {
                            Id =
                                int.Parse(Utils.Utils.SaveSelectSingleNode(nodes[i], "id").InnerText)
                        });
                        sC++;
                        break;
                    }
                    case "gridblock":
                    {
                        _entities.Add(new GridBlock(x, y, z));
                        sC++;
                        break;
                    }
                    case "switch":
                    {
                        _entities.Add(new SwitchBlock(x, y, z)
                        {
                            Id =
                                int.Parse(Utils.Utils.SaveSelectSingleNode(nodes[i], "id").InnerText)
                        });
                        bC++;
                        break;
                    }
                    case "halfblock":
                    {
                        _entities.Add(new HalfBlock(x, y, z));
                        bC++;
                        break;
                    }
                    case "empty":
                    {
                        eC++;
                        break;
                    }
                    default:
                    {
                        throw new FormatException($"Invalid blocktype '{type}'!");
                    }
                }
            }

            if (Basic.DebugMode)
                return;

            int n = NativeMethods.checkLevel(eC, bC, sC);
            int n1 = int.Parse(Utils.Utils.SaveSelectSingleNode(_mapDocument, "//SECU").InnerText);

            if (n == n1) return;

            //throw new InvalidChecksumException("Die Checksumme stimmt nicht überein!\nLevel wird nicht geladen!");
            throw new InvalidChecksumException(LanguageStrings.ErrorStrings.SecurityError);

            /*System.Windows.Forms.MessageBox.Show(
                "Die Checksumme stimmt nicht überein!\nLevel wird nicht geladen!", "SECURITYERROR");*/
            // Basic.SetScreen(new MainMenuScreen());
        }

        public void Init()
        {
            for (short i = 0; i < _entities.Count; i++)
                _entities[i].Init();
        }

		public Entity GetEntity(float x, float z)
		{
            for (short i = 0; i < _entities.Count - 1; i++)
            {
                if (Utils.Utils.CompareFloats(_entities[i].Position.X, x) 
                    && Utils.Utils.CompareFloats(_entities[i].Position.Z, z))
                    return _entities[i];
            }

			return null;
        }

        #endregion
    }
}
