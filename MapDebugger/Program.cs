using System;
using System.Drawing;
using System.Xml;
using System.IO;

namespace MapDebugger
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Write("Datei: ");

            string path = GetInput();

            Console.WriteLine("Modus:");
            Console.WriteLine("  Debug     [Press D]");
            Console.WriteLine("  Randomize [Press R]");

            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;
            }
            while (key != ConsoleKey.R && key != ConsoleKey.D);

            if (key == ConsoleKey.D)
            {
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine("Die Datei wurde nicht gefunden!");
                    return;
                }

                DebugMap(path);
            }
            else
            {
                Console.Write("Width: ");
                int width = ReadInt();

                Console.Write("Height: ");
                int height = ReadInt();

                using (StreamWriter writer = new StreamWriter(File.Open(path, FileMode.Create)))
                {
                    const string space4 = "    ";
                    const string space8 = space4 + space4;

                    Random rnd = new Random();

                    writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    writer.WriteLine("<map version=\"2.0\">");
                    writer.WriteLine($"{space4}<size>{width};{height}</size>");
                    writer.WriteLine($"{space4}<name>Randomizer</name>");
                    

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (rnd.Next(0, 1000) < 500)
                            {
                                writer.WriteLine($"{space4}<entity>");
                                writer.WriteLine($"{space8}<position>{x};{0};{y}</position>");
                                writer.WriteLine($"{space8}<type>wallblock</type>");
                                writer.WriteLine($"{space4}</entity>");
                            }
                        }
                    }

                    writer.WriteLine("</map>");
                }
            }
            

            Console.ReadKey();
        }

        private static void DebugMap(string path)
        {
            XmlDocument mapDocument = new XmlDocument();
            mapDocument.Load(path);

            string size = SaveSelectSingleNode(mapDocument, "//size").InnerText;

            int mapWidth,
                mapHeight;

            if (!int.TryParse(size.Split(';')[0], out mapWidth))
            {
                Console.Error.WriteLine("Ungültige Werte in '<size>'");

                return;
            }

            if (!int.TryParse(size.Split(';')[0], out mapHeight))
            {
                Console.Error.WriteLine("Ungültige Werte in '<size>'");

                return;
            }

            Bitmap bmp = new Bitmap(mapWidth, mapHeight);

            XmlNodeList nodes = mapDocument.SelectNodes("//entity");

            if (nodes == null) throw new InvalidDataException();

            bool error = false;
            for (int i = 0; i < nodes.Count - 1; i++)
            {

                string[] coordinates = SaveSelectSingleNode(nodes[i], "position").InnerText.Split(';');

                int x;
                if (int.TryParse(coordinates[0], out x))
                {
                    int y;
                    if (int.TryParse(coordinates[1], out y))
                    {
                        int z;
                        if (int.TryParse(coordinates[2], out z))
                        {
                            string type = SaveSelectSingleNode(nodes[i], "type").InnerText;

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
                            error = true;

                            break;
                        }
                    }
                    else
                    {
                        Console.Error.WriteLine("Ungültiger Wert in Y-Koordinate!");
                        bmp.Dispose();
                        bmp = null;
                        error = true;

                        break;
                    }
                }
                else
                {
                    Console.Error.WriteLine("Ungültiger Wert in X-Koordinate!");
                    bmp.Dispose();
                    bmp = null;
                    error = true;

                    break;
                }


            }

            if (!error)
            {
                bmp.Save(Path.Combine(Environment.CurrentDirectory, "output.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
                bmp.Dispose();
            }
            else
            {
                Console.Error.WriteLine("Fehler in Datei!");
            }
        }

        private static string GetInput()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) continue;

                return input;
            }
        }

        private static int ReadInt()
        {
            while (true)
            {
                string input = GetInput();

                int number;
                if (int.TryParse(input, out number))
                    return number;
            }
        }

        private static XmlNode SaveSelectSingleNode(XmlNode root, string xpath)
        {
            if(root == null || string.IsNullOrEmpty(xpath))
                throw new NullReferenceException();

            return root.SelectSingleNode(xpath);
        }
    }
}
