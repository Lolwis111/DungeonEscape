﻿using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace LanguageCreator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonLoadLanguage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                Multiselect = false,
                AddExtension = true,
                DefaultExt = "xml",
                Filter = @"XML Files (*.xml) | *.xml"
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK) return;

            XmlDocument languageDocument = new XmlDocument();
            languageDocument.Load(openFileDialog.FileName);

            if (!languageDocument.HasChildNodes)
                throw new InvalidDataException();

            textBackToGame.Text = SaveSelectSingleNode(languageDocument, "//string1").InnerText;
            textSaveAndQuit.Text = SaveSelectSingleNode(languageDocument, "//string2").InnerText;
            textBackToMenu.Text = SaveSelectSingleNode(languageDocument, "//string3").InnerText;
            textYouLost.Text = SaveSelectSingleNode(languageDocument, "//string4").InnerText;

            textNewGame.Text = SaveSelectSingleNode(languageDocument, "//string5").InnerText;
            textLoadGame.Text = SaveSelectSingleNode(languageDocument, "//string6").InnerText;
            textLoadTutorial.Text = SaveSelectSingleNode(languageDocument, "//string7").InnerText;
            textLoadSettings.Text = SaveSelectSingleNode(languageDocument, "//string8").InnerText;
            textExit.Text = SaveSelectSingleNode(languageDocument, "//string9").InnerText;

            textSaveStateOne.Text = SaveSelectSingleNode(languageDocument, "//string10").InnerText;
            textSaveStateTwo.Text = SaveSelectSingleNode(languageDocument, "//string11").InnerText;
            textSaveStateThree.Text = SaveSelectSingleNode(languageDocument, "//string12").InnerText;
            textSaveStateFour.Text = SaveSelectSingleNode(languageDocument, "//string13").InnerText;

            textBack.Text = SaveSelectSingleNode(languageDocument, "//string14").InnerText;

            textInvalidLevel.Text = SaveSelectSingleNode(languageDocument, "//error1").InnerText;
            textBrokenLevel.Text = SaveSelectSingleNode(languageDocument, "//error2").InnerText;
            /*textSecurityError.Text = SaveSelectSingleNode(languageDocument, "//error3").InnerText;

            textCancelButton.Text = SaveSelectSingleNode(languageDocument, "//setting1").InnerText;
            textSaveButton.Text = SaveSelectSingleNode(languageDocument, "//setting2").InnerText;

            textVolume.Text = SaveSelectSingleNode(languageDocument, "//setting3").InnerText;
            textResolution.Text = SaveSelectSingleNode(languageDocument, "//setting4").InnerText;
            textTextures.Text = SaveSelectSingleNode(languageDocument, "//setting5").InnerText;
            textLanguage.Text = SaveSelectSingleNode(languageDocument, "//setting6").InnerText;

            textWindowTitle.Text = SaveSelectSingleNode(languageDocument, "//setting7").InnerText;
            textFullscreen.Text = SaveSelectSingleNode(languageDocument, "//setting8").InnerText;

            textTextureOptions.Lines = SaveSelectSingleNode(languageDocument, "//settings9").InnerText.Split(';');*/
        }

        private void buttonSaveLanguage_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                CheckFileExists = false,
                AddExtension = true,
                DefaultExt = "xml",
                Filter = @"XML Files (*.xml) | *.xml"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

            StringBuilder outputFile = new StringBuilder(512);

            const string space4 = "    ";

            outputFile.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            outputFile.AppendLine("<language>");
            outputFile.AppendLine($"{space4}<string1>{textBackToGame.Text}</string1>");
            outputFile.AppendLine($"{space4}<string2>{textSaveAndQuit.Text}</string2>");
            outputFile.AppendLine($"{space4}<string3>{textBackToMenu.Text}</string3>");
            outputFile.AppendLine($"{space4}<string4>{textYouLost.Text}</string4>");

            outputFile.AppendLine($"{space4}<string5>{textNewGame.Text}</string5>");
            outputFile.AppendLine($"{space4}<string6>{textLoadGame.Text}</string6>");
            outputFile.AppendLine($"{space4}<string7>{textLoadTutorial.Text}</string7>");
            outputFile.AppendLine($"{space4}<string8>{textLoadSettings.Text}</string8>");
            outputFile.AppendLine($"{space4}<string9>{textExit.Text}</string9>");

            outputFile.AppendLine($"{space4}<string10>{textSaveStateOne.Text}</string10>");
            outputFile.AppendLine($"{space4}<string11>{textSaveStateTwo.Text}</string11>");
            outputFile.AppendLine($"{space4}<string12>{textSaveStateThree.Text}</string12>");
            outputFile.AppendLine($"{space4}<string13>{textSaveStateFour.Text}</string13>");

            outputFile.AppendLine($"{space4}<string14>{textBack.Text}</string14>");

            outputFile.AppendLine($"{space4}<error1>{textInvalidLevel.Text}</error1>");
            outputFile.AppendLine($"{space4}<error2>{textBrokenLevel.Text}</error2>");
            outputFile.AppendLine($"{space4}<error3>{textSecurityError.Text}</error3>");

            outputFile.AppendLine($"{space4}<setting1>{textCancelButton.Text}</setting1>");
            outputFile.AppendLine($"{space4}<setting2>{textSaveButton.Text}</setting2>");

            outputFile.AppendLine($"{space4}<setting3>{textVolume.Text}</setting3>");
            outputFile.AppendLine($"{space4}<setting4>{textResolution.Text}</setting4>");
            outputFile.AppendLine($"{space4}<setting5>{textTextures.Text}</setting5>");
            outputFile.AppendLine($"{space4}<setting6>{textLanguage.Text}</setting6>");

            outputFile.AppendLine($"{space4}<setting7>{textWindowTitle.Text}</setting7>");
            outputFile.AppendLine($"{space4}<setting8>{textFullscreen.Text}</setting8>");

            string options = string.Empty;
            foreach (string l in textTextureOptions.Lines)
                options += l + ";";

            if (options.EndsWith(";"))
                options = options.Substring(options.Length - 1);

            outputFile.AppendLine($"{space4}<settings9>{options}</settings9>");

            outputFile.AppendLine("</language>");

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(File.Open(saveFileDialog.FileName, FileMode.Create, FileAccess.Write));

                writer.Write(outputFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                writer?.Dispose();
            }
        }

        private static XmlNode SaveSelectSingleNode(XmlNode root, string xpath)
        {
            if (root != null)
                return root.SelectSingleNode(xpath);

            throw new NullReferenceException();
        }
    }
}
