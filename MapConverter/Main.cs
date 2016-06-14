using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MapConverter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void btnBrowseInput_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.Filter = "Bitmaps (*.bmp)|*.bmp";
                openFile.CheckFileExists = true;
                openFile.CheckPathExists = true;
                openFile.ValidateNames = true;

                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtInput.Text = openFile.FileName;
            }
        }

        private void btnBrowseOutput_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "XML Files (*.xml)|*.xml";

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    txtOutput.Text = saveFile.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtInput.Text))
            {
                MessageBox.Show("ERROR: Inputfile", "Error");
                return;
            }

            Bitmap input = (Bitmap)Bitmap.FromFile(txtInput.Text);

            StringBuilder builder = new StringBuilder(10000);

            builder.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            builder.AppendLine("<!--Konvertiert mit MapConverter-->");
            builder.AppendLine("<map>");
            builder.AppendFormat("<size>{0};{1}</size>{2}", input.Height, input.Width, Environment.NewLine);
            builder.AppendFormat("<name>{0}</name>{1}", txtName.Text, Environment.NewLine);

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    Color color = input.GetPixel(x, y);

                    if (color == Color.FromArgb(255, 255, 255))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>wallblock</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(255, 0, 0))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>spawn</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(255, 200, 200))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>levelup</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(200, 255, 200))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>leveldown</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(255, 100, 100))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>key</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(100, 255, 100))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>pliers</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(100, 100, 255))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>pickaxe</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(0, 255, 0))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>destroyblock</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(100, 100, 100))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>doorblock</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else if (color == Color.FromArgb(0, 0, 255))
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>gridblock</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                    else
                    {
                        builder.AppendLine("\t<entity>");
                        builder.AppendFormat("\t\t<position>{0};0;{1}</position>\n", x, y);
                        builder.AppendFormat("\t\t<type>empty</type>\n");
                        builder.AppendLine("\t</entity>");
                    }
                }
            }

            builder.AppendLine("</map>");

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(txtOutput.Text);
                writer.Write(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace + "\n\n" + ex.Message);
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
    }
}
