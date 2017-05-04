using System;
using System.IO;
using System.Xml;

namespace DungeonEscape.Content
{
    public static class LanguageStrings
    {
        public static string BackToGame;
        public static string SaveAndQuit;
        public static string BackToMenu;
        public static string YouLost;

        public static string NewGame;
        public static string LoadGame;
        public static string LoadTutorial;
        public static string LoadSettings;
        public static string Exit;

        public static string SaveStateOne;
        public static string SaveStateTwo;
        public static string SaveStateThree;
        public static string SaveStateFour;

        public static string Back;

        public static void LoadStrings(string language)
        {
            string path = Path.Combine(Environment.CurrentDirectory, Basic.Content.RootDirectory, "Language", $"{language}.xml");

            if (!File.Exists(path))
                throw new FileNotFoundException("The specified language was not found!");

            XmlDocument languageDocument = new XmlDocument();
            languageDocument.Load(path);

            if(!languageDocument.HasChildNodes)
                throw new InvalidDataException();

            BackToGame = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string1").InnerText;
            SaveAndQuit = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string2").InnerText;
            BackToMenu = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string3").InnerText;
            YouLost = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string4").InnerText;

            NewGame = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string5").InnerText;
            LoadGame = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string6").InnerText;
            LoadTutorial = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string7").InnerText;
            LoadSettings = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string8").InnerText;
            Exit = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string9").InnerText;

            SaveStateOne = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string10").InnerText;
            SaveStateTwo = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string11").InnerText;
            SaveStateThree = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string12").InnerText;
            SaveStateFour = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string13").InnerText;

            Back = Utils.Utils.SaveSelectSingleNode(languageDocument, "//string14").InnerText;

            ErrorStrings.InvalidLevel = Utils.Utils.SaveSelectSingleNode(languageDocument, "//error1").InnerText;
            ErrorStrings.BrokenLevel = Utils.Utils.SaveSelectSingleNode(languageDocument, "//error2").InnerText;
            ErrorStrings.SecurityError = Utils.Utils.SaveSelectSingleNode(languageDocument, "//error3").InnerText;

            SettingsLabelStrings.ButtonCancel = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting1").InnerText;
            SettingsLabelStrings.ButtonSave = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting2").InnerText;

            SettingsLabelStrings.LabelVolume = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting3").InnerText;
            SettingsLabelStrings.LabelResolution = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting4").InnerText;
            SettingsLabelStrings.LabelTextures = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting5").InnerText;
            SettingsLabelStrings.LabelLanguage = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting6").InnerText;

            SettingsLabelStrings.WindowTitle = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting7").InnerText;
            SettingsLabelStrings.FullscreenSwitch = Utils.Utils.SaveSelectSingleNode(languageDocument, "//setting8").InnerText;

            string[] options = Utils.Utils.SaveSelectSingleNode(languageDocument, "//settings9").InnerText.Split(';');

            if (options.Length < 1)
                throw new InvalidDataException();

            SettingsLabelStrings.TextureOptions = options;
        }

        public static class ErrorStrings
        {
            public static string InvalidLevel;
            public static string BrokenLevel;
            public static string SecurityError;
        }

        public static class SettingsLabelStrings
        {
            public static string LabelVolume;
            public static string LabelTextures;
            public static string LabelResolution;
            public static string LabelLanguage;

            public static string[] TextureOptions;

            public static string ButtonSave;
            public static string ButtonCancel;

            public static string WindowTitle;

            public static string FullscreenSwitch;
        }
    }
}
