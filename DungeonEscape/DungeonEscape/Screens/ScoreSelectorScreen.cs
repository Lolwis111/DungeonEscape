using System;
using System.Collections.Generic;
using System.IO;
using DungeonEscape.Content;
using DungeonEscape.GUI.Components;
using DungeonEscape.SaveGames;
using DungeonEscape.Utils;

namespace DungeonEscape.Screens
{
    internal sealed class ScoreSelectorScreen : IScreen
    {
        private readonly List<Button> _buttons = new List<Button>();
        private readonly bool _loadGame;

        public ScoreSelectorScreen(bool load)
        {
            Mouse.ShowMouse();

            _loadGame = load;

            /*_buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Speicherstand 1", LoadScore1));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, "Speicherstand 2", LoadScore2));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Speicherstand 3", LoadScore3));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, "Speicherstand 4", LoadScore4));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, "Zurück", Back));*/

            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, LanguageStrings.SaveStateOne, LoadScore1));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, LanguageStrings.SaveStateTwo, LoadScore2));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, LanguageStrings.SaveStateThree, LoadScore3));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, LanguageStrings.SaveStateFour, LoadScore4));
            _buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, LanguageStrings.Back, Back));

            if (load)
            {
                _buttons[0].Enabled = CheckFile(SaveState.One);
                _buttons[1].Enabled = CheckFile(SaveState.Two);
                _buttons[2].Enabled = CheckFile(SaveState.Three);
                _buttons[3].Enabled = CheckFile(SaveState.Four);
            }
            else
            {
                _buttons[0].Enabled = true;
                _buttons[1].Enabled = true;
                _buttons[2].Enabled = true;
                _buttons[3].Enabled = true;
            }

            _buttons[0].Visible = true;
            _buttons[1].Visible = true;
            _buttons[2].Visible = true;
            _buttons[3].Visible = true;

            _buttons[4].Enabled = true;
            _buttons[4].Visible = true;
        }

        private static bool CheckFile(SaveState state)
        {
            switch (state)
            {
                case SaveState.One:
                    return File.Exists($"{Environment.CurrentDirectory}/save1.xml");

                case SaveState.Two:
                    return File.Exists($"{Environment.CurrentDirectory}/save2.xml");

                case SaveState.Three:
                    return File.Exists($"{Environment.CurrentDirectory}/save3.xml");

                case SaveState.Four:
                    return File.Exists($"{Environment.CurrentDirectory}/save4.xml");

                case SaveState.ByPass:
                    return true;

                default:
                    return false;
            }
        }

        public void Update()
        {
            _buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[2].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[3].PositionX = Basic.WindowSize.Width / 2 - 230;
            _buttons[4].PositionX = Basic.WindowSize.Width / 2 - 230;

            foreach (Button button in _buttons)
                button.Update();

            System.Threading.Thread.Sleep(1);
        }

        public void Render()
        {
            foreach (Button current in _buttons)
            {
                current.Render();
            }
        }

        private void LoadScore1()
        {
            Basic.SetScreen(_loadGame ? new GameScreen(Savegamescore.Load(SaveState.One)) : new GameScreen(1, SaveState.One));
        }

        private void LoadScore2()
        {
            Basic.SetScreen(_loadGame ? new GameScreen(Savegamescore.Load(SaveState.Two)) : new GameScreen(1, SaveState.Two));
        }

        private void LoadScore3()
        {
            Basic.SetScreen(_loadGame ? new GameScreen(Savegamescore.Load(SaveState.Three)) : new GameScreen(1, SaveState.Three));
        }

        private void LoadScore4()
        {
            Basic.SetScreen(_loadGame ? new GameScreen(Savegamescore.Load(SaveState.Four)) : new GameScreen(1, SaveState.Four));
        }

        private static void Back()
        {
            Basic.SetScreen(new MainMenuScreen());
        }

        public void Init()
        {
            
        }
    }
}
