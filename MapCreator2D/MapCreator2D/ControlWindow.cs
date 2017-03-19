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
                parseTile();
            }
        }
        private Tile _tile = new Tile(0, 0, TileType.None);

        public TileType Type
        {
            get { return _t; }
            set { _t = value; }
        }
        private TileType _t = TileType.None;

        public bool GetInfo { get { return _info; } }
        private bool _info = false;

        private string name = string.Empty;
        private bool nameSet = false;

        public ControlWindow()
        {
            InitializeComponent();
        }

        private void CheckChange(object sender, EventArgs e)
        {
            gbMessage.Visible = false;
            gbID.Visible = false;
            _info = false;

            if (rdNone.Checked)
                //_tile.Type = TileType.None;
                _t = TileType.None;
            else if (rdWall.Checked)
                //_tile.Type = TileType.Wall;
                _t = TileType.Wall;
            else if (rdPliers.Checked)
                //_tile.Type = TileType.Pliers;
                _t = TileType.Pliers;
            else if (rdPickaxe.Checked)
                //_tile.Type = TileType.Pickaxe;
                _t = TileType.Pickaxe;
            else if (rdLevelUp.Checked)
                //_tile.Type = TileType.LevelUp;
                _t = TileType.LevelUp;
            else if (rdLevelDown.Checked)
                //_tile.Type = TileType.LevelDown;
                _t = TileType.LevelDown;
            else if (rdKey.Checked)
            {
                //_tile.Type = TileType.Key;
                _t = TileType.Key;
                gbID.Visible = true;
            }
            else if (rdGrid.Checked)
                //_tile.Type = TileType.Grid;
                _t = TileType.Grid;
            else if (rdDoor.Checked)
            {
                //_tile.Type = TileType.Door;
                _t = TileType.Door;
                gbID.Visible = true;
            }
            else if (rdDestroyable.Checked)
                //_tile.Type = TileType.Destroyable;
                _t = TileType.Destroyable;
            else if (rdSpawn.Checked)
                //_tile.Type = TileType.Spawn;
                _t = TileType.Spawn;
            else if (rdMessage.Checked)
            {
                //_tile.Type = TileType.Message;
                _t = TileType.Message;
                gbMessage.Visible = true;
            }
            else if (rdSwitch.Checked)
            {
                _t = TileType.Switch;
                gbID.Visible = true;
            }
            else if (rdHalfBlock.Checked)
            {
                _t = TileType.Half;
            }
            else if (rdInfo.Checked)
            {
                _info = true;
            }
        }

        private void parseTile()
        {
            lblPosition.Text = string.Format("X: {0}; Y: {1}", _tile.Rectangle.X / 30, _tile.Rectangle.Y / 30);
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
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "XML Dateien|*.xml";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                StringBuilder builder = new StringBuilder(30000);
                builder.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                builder.Append("<!--Erstellt mit MapCreator2D-->");
                builder.Append("<map version=\"2.0\">");
                builder.AppendFormat("<size>{0};{1}</size>", 20, 20);


                if (!nameSet)
                {
                    InputDialog dialog = new InputDialog();
                    if (dialog.ShowDialog() == DialogResult.Cancel)
                    {
                        MessageBox.Show("Es muss ein Name ingegeben werden!", "Fehler");
                        return;
                    }

                    name = dialog.EnteredText;
                    nameSet = true;

                    dialog.Dispose();
                    dialog = null;
                }

                builder.AppendFormat("<name>{0}</name>", name);

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
                    if (writer != null)
                    {
                        writer.Dispose();
                        writer = null;
                    }
                }

                lblName.Text = "Name: "+ name;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "XML Files|*.xml";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                XmlDocument document = new XmlDocument();
                document.Load(openFile.FileName);

                bool idMode = false;
                XmlNode root = document.SelectSingleNode("//map");
                if (root.Attributes.Count > 0 && root.Attributes["version"].Value == "1.0")
                {
                    idMode = true;
                }

                name = document.SelectSingleNode("//name").InnerText;
                lblName.Text = "Name: " + name;
                nameSet = true;

                Basic.Tiles = new System.Collections.Generic.List<Tile>();

                XmlNodeList nodes = document.SelectNodes("//entity");

                string[] coordinates = new string[3];

                foreach (XmlNode node in nodes)
                {
                    coordinates = node.SelectSingleNode("position").InnerText.Split(';');

                    int x = 0, y = 0, z = 0;
                    if (int.TryParse(coordinates[0], out x))
                    {
                        if (int.TryParse(coordinates[1], out y))
                        {
                            if (int.TryParse(coordinates[2], out z))
                            {
                                string type = node.SelectSingleNode("type").InnerText;

                                if (type == "wallblock")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Wall));
                                }
                                else if (type == "spawn")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Spawn));
                                }
                                else if (type == "levelup")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.LevelUp));
                                }
                                else if (type == "leveldown")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.LevelDown));
                                }
                                else if (type == "key")
                                {
                                    if (idMode)
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Key)
                                        {
                                            Id = int.Parse(node.SelectSingleNode("id").InnerText)
                                        });
                                    }
                                    else
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Key)
                                        {
                                            Id = 0
                                        });
                                    }
                                }
                                else if (type == "pliers")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Pliers));
                                }
                                else if (type == "pickaxe")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Pickaxe));
                                }
                                else if (type == "destroyblock")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Destroyable));
                                }
                                else if (type == "doorblock")
                                {
                                    if (idMode)
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Door)
                                        {
                                            Id = int.Parse(node.SelectSingleNode("id").InnerText)
                                        });
                                    }
                                    else
                                    {
                                        Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Door)
                                        {
                                            Id = 0
                                        });
                                    }
                                }
                                else if (type == "gridblock")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Grid));
                                }
                                else if (type == "message")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Message)
                                    {
                                        Message = node.SelectSingleNode("text").InnerText
                                    });
                                }
                                else if (type == "switch")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Switch)
                                    {
                                        Id = int.Parse(node.SelectSingleNode("id").InnerText)
                                    });
                                }
                                else if (type == "halfblock")
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Half));
                                }
                                else
                                {
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.None));
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
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            nameSet = false;
            name = string.Empty;
            Basic.clearTiles();
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