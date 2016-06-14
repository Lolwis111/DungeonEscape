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
                    selectorID.Value = _tile.ID;
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
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StringBuilder builder = new StringBuilder(30000);
                builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
                builder.AppendLine("<!--Erstellt mit MapCreator2D-->");
                builder.AppendLine("<map version=\"2.0\">");
                builder.AppendFormat("\t<size>{0};{1}</size>{2}", 20, 20, Environment.NewLine);


                if (!nameSet)
                {
                    InputDialog dialog = new InputDialog();
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    {
                        MessageBox.Show("Es muss ein Name ingegeben werden!", "Fehler");
                        return;
                    }

                    name = dialog.EnteredText;
                    nameSet = true;

                    dialog.Dispose();
                    dialog = null;
                }

                builder.AppendFormat("\t<name>{0}</name>{1}", name, Environment.NewLine);

                int eC = 0, bC = 0, sC = 0;

                foreach (Tile tile in Basic.Tiles)
                {
                    builder.AppendLine("\t<entity>");
                    builder.AppendFormat("\t\t<position>{0};0;{1}</position>{2}", tile.Rectangle.X/30, tile.Rectangle.Y/30, Environment.NewLine);
                    switch (tile.Type)
                    {
                        case TileType.Destroyable:
                            builder.AppendFormat("\t\t<type>destroyblock</type>{0}", Environment.NewLine);
                            bC++;
                            break;
                        case TileType.Door:
                            builder.AppendFormat("\t\t<type>doorblock</type>{0}", Environment.NewLine);
                            builder.AppendFormat("\t\t<id>{0}</id>{1}", tile.ID, Environment.NewLine);
                            sC++;
                            break;
                        case TileType.Grid:
                            builder.AppendFormat("\t\t<type>gridblock</type>{0}", Environment.NewLine);
                            sC++;
                            break;
                        case TileType.Key:
                            builder.AppendFormat("\t\t<type>key</type>{0}", Environment.NewLine);
                            builder.AppendFormat("\t\t<id>{0}</id>{1}", tile.ID, Environment.NewLine);
                            sC++;
                            break;
                        case TileType.LevelDown:
                            builder.AppendFormat("\t\t<type>leveldown</type>{0}", Environment.NewLine);
                            sC++;
                            break;
                        case TileType.LevelUp:
                            builder.AppendFormat("\t\t<type>levelup</type>{0}", Environment.NewLine);
                            sC++;
                            break;
                        case TileType.Pickaxe:
                            builder.AppendFormat("\t\t<type>pickaxe</type>{0}", Environment.NewLine);
                            sC++;
                            break;
                        case TileType.Pliers:
                            builder.AppendFormat("\t\t<type>pliers</type>{0}", Environment.NewLine);
                            sC++;
                            break;
                        case TileType.Wall:
                            builder.AppendFormat("\t\t<type>wallblock</type>{0}", Environment.NewLine);
                            bC++;
                            break;
                        case TileType.Half:
                            builder.AppendFormat("\t\t<type>halfblock</type>{0}", Environment.NewLine);
                            bC++;
                            break;
                        case TileType.Spawn:
                            builder.AppendFormat("\t\t<type>spawn</type>{0}", Environment.NewLine);
                            break;
                        case TileType.Message:
                            builder.AppendFormat("\t\t<type>message</type>{0}", Environment.NewLine);
                            builder.AppendFormat("\t\t<text>{0}</text>{1}", tile.Message, Environment.NewLine);
                            sC++;
                            break;
                        case TileType.Switch:
                            builder.AppendFormat("\t\t<type>switch</type>{0}", Environment.NewLine);
                            builder.AppendFormat("\t\t<id>{0}</id>{1}", tile.ID, Environment.NewLine);
                            bC++;
                            break;
                        case TileType.None:
                            builder.AppendLine("\t\t<type>empty</type>");
                            eC++;
                            break;
                    }
                    builder.AppendLine("\t</entity>");
                }

                builder.AppendFormat("<SECU>{0}</SECU>", NativeMethods.checkLevel(eC, bC, sC));

                builder.AppendLine("</map>");

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
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Wall));
                                else if (type == "spawn")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Spawn));
                                else if (type == "levelup")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.LevelUp));
                                else if (type == "leveldown")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.LevelDown));
                                else if (type == "key")
                                {
                                    if (idMode) Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Key) { ID = int.Parse(node.SelectSingleNode("id").InnerText) });
                                    else Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Key) { ID = 0 });
                                }
                                else if (type == "pliers")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Pliers));
                                else if (type == "pickaxe")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Pickaxe));
                                else if (type == "destroyblock")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Destroyable));
                                else if (type == "doorblock")
                                {
                                    if (idMode) Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Door) { ID = int.Parse(node.SelectSingleNode("id").InnerText) });
                                    else Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Door) { ID = 0 });
                                }
                                else if (type == "gridblock")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Grid));
                                else if (type == "message")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Message) { Message = node.SelectSingleNode("text").InnerText });
                                else if (type == "switch")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Switch) { ID = int.Parse(node.SelectSingleNode("id").InnerText) });
                                else if (type == "halfblock")
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.Half));
                                else
                                    Basic.Tiles.Add(new Tile(x * 30, z * 30, TileType.None));
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
            _tile.ID = (int)selectorID.Value;
        }
    }
}