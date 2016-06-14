using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.IO;

namespace MapDebugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Datei: ");
            string path = Console.ReadLine();

            if (System.IO.File.Exists(path))
            {
                Console.Error.WriteLine("Die Datei wurde nicht gefunden!");
                return;
            }

            XmlDocument mapDocument = new XmlDocument();
            mapDocument.Load(path);

            string size = mapDocument.SelectSingleNode("//size").InnerText;

            int mapWidth = 0, mapHeight = 0;
            if (!int.TryParse(size.Split(';')[0], out mapWidth))
            {
                Console.Error.WriteLine("Ungültige Werte in '<size>'");

                mapDocument = null;
                return;
            }

            if (!int.TryParse(size.Split(';')[0], out mapHeight))
            {
                Console.Error.WriteLine("Ungültige Werte in '<size>'");

                mapDocument = null;
                return;
            }

            Bitmap bmp = new Bitmap(mapWidth, mapHeight);
            string name = mapDocument.SelectSingleNode("//name").InnerText;
            XmlNodeList nodes = mapDocument.SelectNodes("//entity");
            string[] coordinates = new string[3];
            bool error = false;
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                coordinates = nodes[i].SelectSingleNode("position").InnerText.Split(';');
                int x = 1, y = 1, z = 1;
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
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(255, 255, 255));
                                    break;
                                case "spawn":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(255, 0, 0));
                                    break;
                                case "levelup":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(255, 200, 200));
                                    break;
                                case "leveldown":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(200, 255, 200));
                                    break;
                                case "key":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(255, 100, 100));
                                    break;
                                case "pliers":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(100, 255, 100));
                                    break;
                                case "pickaxe":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(100, 100, 255));
                                    break;
                                case "destroyblock":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(0, 255, 0));
                                    break;
                                case "doorblock":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(100, 100, 100));
                                    break;
                                case "gridblock":
                                    Console.WriteLine("{0}: {1}:\t\t\t{2};{3}", i, type.ToUpper(), x, z);
                                    bmp.SetPixel(x, z, Color.FromArgb(0, 0, 255));
                                    break;
                                case "empty":
                                    bmp.SetPixel(x, z, Color.FromArgb(0, 0, 0));
                                    break;
                                default:
                                    Console.Error.WriteLine("Ungültiger Wert in '<type>'");
                                    error = true;
                                    break;
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine("Ungültiger Wert in Z-Koordinate!");
                            bmp.Dispose();
                            bmp = null;
                            nodes = null;
                            mapDocument = null;

                            break;
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Ungültiger Wert in Y-Koordinate!");
                        bmp.Dispose();
                        bmp = null;
                        nodes = null;
                        mapDocument = null;

                        break;
                    }
                }
                else
                {
                    Console.Error.WriteLine("Ungültiger Wert in X-Koordinate!");
                    bmp.Dispose();
                    bmp = null;
                    nodes = null;
                    mapDocument = null;

                    break;
                }


            }

            if (!error)
            {
                bmp.Save(Path.Combine(Environment.CurrentDirectory, "output.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
                bmp.Dispose();
                bmp = null;
            }
            else 
            {
                Console.Error.WriteLine("Fehler in Datei!");
            }

            Console.ReadKey();
        }
    }
}
