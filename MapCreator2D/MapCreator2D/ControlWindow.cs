using System;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;


namespace MapCreator2D
{
    public partial class ControlWindow : Form
    {

        public Tile SelectedTile 
        {
            get { return _tile; }
            set 
            { 
                _tile = value;
                ParseTile();
            }
        }
        private Tile _tile = new Tile(0, 0, TileType.None);

        public TileType Type { get; set; } = TileType.None;

        public bool GetInfo { get; private set; }

        private string _name = string.Empty;
        private bool _nameSet;

        public ControlWindow()
        {
            InitializeComponent();
        }

        private void CheckChange(object sender, EventArgs e)
        {
            gbMessage.Visible = false;
            gbID.Visible = false;
            GetInfo = false;

            if (rdNone.Checked)
                //_tile.Type = TileType.None;
                Type = TileType.None;
            else if (rdWall.Checked)
                //_tile.Type = TileType.Wall;
                Type = TileType.Wall;
            else if (rdPliers.Checked)
                //_tile.Type = TileType.Pliers;
                Type = TileType.Pliers;
            else if (rdPickaxe.Checked)
                //_tile.Type = TileType.Pickaxe;
                Type = TileType.Pickaxe;
            else if (rdLevelUp.Checked)
                //_tile.Type = TileType.LevelUp;
                Type = TileType.LevelUp;
            else if (rdLevelDown.Checked)
                //_tile.Type = TileType.LevelDown;
                Type = TileType.LevelDown;
            else if (rdKey.Checked)
            {
                //_tile.Type = TileType.Key;
                Type = TileType.Key;
                gbID.Visible = true;
            }
            else if (rdGrid.Checked)
                //_tile.Type = TileType.Grid;
                Type = TileType.Grid;
            else if (rdDoor.Checked)
            {
                //_tile.Type = TileType.Door;
                Type = TileType.Door;
                gbID.Visible = true;
            }
            else if (rdDestroyable.Checked)
                //_tile.Type = TileType.Destroyable;
                Type = TileType.Destroyable;
            else if (rdSpawn.Checked)
                //_tile.Type = TileType.Spawn;
                Type = TileType.Spawn;
            else if (rdMessage.Checked)
            {
                //_tile.Type = TileType.Message;
                Type = TileType.Message;
                gbMessage.Visible = true;
            }
            else if (rdSwitch.Checked)
            {
                Type = TileType.Switch;
                gbID.Visible = true;
            }
            else if (rdHalfBlock.Checked)
            {
                Type = TileType.Half;
            }
            else if (rdInfo.Checked)
            {
                GetInfo = true;
            }
        }

        private void ParseTile()
        {
            lblPosition.Text = $"X: {_tile.Rectangle.X / 30}; Y: {_tile.Rectangle.Y / 30}";
            gbID.Visible = false;
            gbMessage.Visible = false;

            switch (_tile.Type)
            {
                case TileType.Message:
                    txtMessage.Text = _tile.Message;
                    gbMessage.Visible = true;
                    break;
                case TileType.Key:
                case TileType.Door:
                case TileType.Switch:
                    selectorID.Value = _tile.Id;
                    gbID.Visible = true;
                    break;
                case TileType.None:
                case TileType.Destroyable:
                case TileType.Grid:
                case TileType.LevelDown:
                case TileType.LevelUp:
                case TileType.Pickaxe:
                case TileType.Pliers:
                case TileType.Wall:
                case TileType.Spawn:
                case TileType.Half:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                Filter = "XML Dateien|*.xml"
            };
            
            if (saveFile.ShowDialog() != DialogResult.OK) return;

            StringBuilder builder = new StringBuilder(30000);
            builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            builder.Append("<!--Erstellt mit MapCreator2D-->");
            builder.Append("<map version=\"2.0\">");
            builder.AppendFormat("<size>{0};{1}</size>", 20, 20);


            if (!_nameSet)
            {
                InputDialog dialog = new InputDialog();
                if (dialog.ShowDialog() == DialogResult.Cancel)
                {
                    MessageBox.Show("Es muss ein Name ingegeben werden!", "Fehler");
                    return;
                }

                _name = dialog.EnteredText;
                _nameSet = true;

                dialog.Dispose();
            }

            builder.AppendFormat("<_name>{0}</_name>", _name);

            int eC = 0, bC = 0, sC = 0;

            foreach (Tile tile in Basic.Tiles)
            {
                builder.Append("<entity>");
                builder.AppendFormat("<position>{0};0;{1}</position>", tile.Rectangle.X/30, tile.Rectangle.Y/30);
                switch (tile.Type)
                {
                    case TileType.Destroyable:
                        builder.Append("<type>destroyblock</type>");
                        bC++;
                        break;
                    case TileType.Door:
                        builder.Append("<type>doorblock</type>");
                        builder.AppendFormat("<id>{0}</id>", tile.Id);
                        sC++;
                        break;
                    case TileType.Grid:
                        builder.Append("<type>gridblock</type>");
                        sC++;
                        break;
                    case TileType.Key:
                        builder.Append("<type>key</type>");
                        builder.AppendFormat("<id>{0}</id>", tile.Id);
                        sC++;
                        break;
                    case TileType.LevelDown:
                        builder.Append("<type>leveldown</type>");
                        sC++;
                        break;
                    case TileType.LevelUp:
                        builder.Append("<type>levelup</type>");
                        sC++;
                        break;
                    case TileType.Pickaxe:
                        builder.Append("<type>pickaxe</type>");
                        sC++;
                        break;
                    case TileType.Pliers:
                        builder.Append("<type>pliers</type>");
                        sC++;
                        break;
                    case TileType.Wall:
                        builder.Append("<type>wallblock</type>");
                        bC++;
                        break;
                    case TileType.Half:
                        builder.Append("<type>halfblock</type>");
                        bC++;
                        break;
                    case TileType.Spawn:
                        builder.Append("<type>spawn</type>");
                        break;
                    case TileType.Message:
                        builder.Append("<type>message</type>");
                        builder.AppendFormat("<text>{0}</text>", tile.Message);
                        sC++;
                        break;
                    case TileType.Switch:
                        builder.Append("<type>switch</type>");
                        builder.AppendFormat("<id>{0}</id>", tile.Id);
                        bC++;
                        break;
                    case TileType.None:
                        builder.Append("<type>empty</type>");
                        eC++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                builder.Append("</entity>");
            }

            builder.AppendFormat("<SECU>{0}</SECU>", NativeMethods.checkLevel(eC, bC, sC));

            builder.Append("</map>");

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(File.Open(saveFile.FileName, FileMode.Create));

                writer.Write(builder.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Fehler beim Schreiben der Datei!");
            }
            finally
            {
                writer?.Dispose();
            }

            lblName.Text = "Name: "+ _name;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "XML Files|*.xml"
            };

            if (openFile.ShowDialog() != DialogResult.OK) return;

            XmlDocument document = new XmlDocument();
            document.Load(openFile.FileName);

            bool idMode = false;
            XmlNode root = document.SelectSingleNode("//map");

            if (root?.Attributes == null) throw new InvalidDataException();

            if (root.Attributes.Count > 0 && root.Attributes["version"].Value == "1.0")
            {
                idMode = true;
            }

            _name = SaveSelectSingleNode(document, "//name").InnerText;
            lblName.Text = "Name: " + _name;
            _nameSet = true;

            Basic.Tiles = new System.Collections.Generic.List<Tile>();

            XmlNodeList nodes = document.SelectNodes("//entity");

            if (nodes == null) throw new InvalidDataException();

            foreach (XmlNode node in nodes)
            {
                string[] coordinates = SaveSelectSingleNode(node, "position").InnerText.Split(';');

                int x;
                if (int.TryParse(coordinates[0], out x))
                {
                    int y;
                    if (int.TryParse(coordinates[1], out y))
                    {
                        int z;
                        if (int.TryParse(coordinates[2], out z))
                        {
                            string type = SaveSelectSingleNode(node, "type").InnerText;

                            switch (type)
                            {
                                case "wallblock":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Wall));
                                    break;
                                case "spawn":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Spawn));
                                    break;
                                case "levelup":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.LevelUp));
                                    break;
                                case "leveldown":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.LevelDown));
                                    break;
                                case "key":
                                    if (idMode)
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Key)
                                        {
                                            Id = int.Parse(SaveSelectSingleNode(node, "id").InnerText)
                                        });
                                    }
                                    else
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Key)
                                        {
                                            Id = 0
                                        });
                                    }
                                    break;
                                case "pliers":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Pliers));
                                    break;
                                case "pickaxe":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Pickaxe));
                                    break;
                                case "destroyblock":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Destroyable));
                                    break;
                                case "doorblock":
                                    if (idMode)
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Door)
                                        {
                                            Id = int.Parse(SaveSelectSingleNode(node, "id").InnerText)
                                        });
                                    }
                                    else
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Door)
                                        {
                                            Id = 0
                                        });
                                    }
                                    break;
                                case "gridblock":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Grid));
                                    break;
                                case "message":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Message)
                                    {
                                        Message = SaveSelectSingleNode(node, "text").InnerText
                                    });
                                    break;
                                case "switch":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Switch)
                                    {
                                        Id = int.Parse(SaveSelectSingleNode(node, "id").InnerText)
                                    });
                                    break;
                                case "halfblock":
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Half));
                                    break;
                                default:
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.None));
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Fehler bei Z-Koordinate!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fehler bei Y-Koordinate!");
                    }
                }
                else
                {
                    MessageBox.Show("Fehler bei X-Koordinate!");
                }
            }
        }

        private static XmlNode SaveSelectSingleNode(XmlNode root, string xpath)
        {
            if(string.IsNullOrEmpty(xpath) || root == null)
                throw new NullReferenceException();

            return root.SelectSingleNode(xpath);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _nameSet = false;
            _name = string.Empty;
            Basic.ClearTiles();
        }

        private void btnSetMessage_Click(object sender, EventArgs e)
        {
            _tile.Message = txtMessage.Text;
        }

        private void btnSetID_Click(object sender, EventArgs e)
        {
            _tile.Id = (int)selectorID.Value;
        }
    }
}