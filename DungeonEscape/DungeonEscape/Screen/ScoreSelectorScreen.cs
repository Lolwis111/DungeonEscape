using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DungeonEscape
{
    public sealed class ScoreSelectorScreen : IScreen
    {
        private List<Button> buttons = new List<Button>();
        private bool loadGame = false;

        public ScoreSelectorScreen(bool load)
        {
            Screen.ShowMouse();

            loadGame = load;

            buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 100, 460, 70, "Speicherstand 1", new Click(LoadScore1)));
            buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 180, 460, 70, "Speicherstand 2", new Click(LoadScore2)));
            buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 260, 460, 70, "Speicherstand 3", new Click(LoadScore3)));
            buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 340, 460, 70, "Speicherstand 4", new Click(LoadScore4)));
            buttons.Add(new Button(Basic.WindowSize.Width / 2 - 230, 420, 460, 70, "Zurück", new Click(Back)));

            if (load)
            {
                buttons[0].Enabled = checkFile(SaveState.One);
                buttons[1].Enabled = checkFile(SaveState.Two);
                buttons[2].Enabled = checkFile(SaveState.Three);
                buttons[3].Enabled = checkFile(SaveState.Four);
            }
            else
            {
                buttons[0].Enabled = true;
                buttons[1].Enabled = true;
                buttons[2].Enabled = true;
                buttons[3].Enabled = true;
            }

            buttons[0].Visible = true;
            buttons[1].Visible = true;
            buttons[2].Visible = true;
            buttons[3].Visible = true;

            buttons[4].Enabled = true;
            buttons[4].Visible = true;
        }

        private bool checkFile(SaveState state)
        {
            if (state == SaveState.One)
                return File.Exists(string.Format("{0}/save1.xml", Environment.CurrentDirectory));
            else if (state == SaveState.Two)
                return File.Exists(string.Format("{0}/save2.xml", Environment.CurrentDirectory));
            else if (state == SaveState.Three)
                return File.Exists(string.Format("{0}/save3.xml", Environment.CurrentDirectory));
            else if(state == SaveState.Four)
                return File.Exists(string.Format("{0}/save4.xml", Environment.CurrentDirectory));

            return false;
        }

        public void Update()
        {
            buttons[0].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[1].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[2].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[3].PositionX = Basic.WindowSize.Width / 2 - 230;
            buttons[4].PositionX = Basic.WindowSize.Width / 2 - 230;

            foreach (Button button in buttons)
                button.Update();

            System.Threading.Thread.Sleep(1);
        }

        public void Render()
        {
            foreach (Button current in buttons)
            {
                current.Render();
            }
        }

        private void LoadScore1()
        {
            if (loadGame)
                Basic.setScreen(new GameScreen(Savegamescore.Load(SaveState.One)));
            else
                Basic.setScreen(new GameScreen(1, SaveState.One));
        }

        private void LoadScore2()
        {
            if (loadGame)
                Basic.setScreen(new GameScreen(Savegamescore.Load(SaveState.Two)));
            else
                Basic.setScreen(new GameScreen(1, SaveState.Two));
        }

        private void LoadScore3()
        {
            if (loadGame)
                Basic.setScreen(new GameScreen(Savegamescore.Load(SaveState.Three)));
            else
                Basic.setScreen(new GameScreen(1, SaveState.Three));
        }

        private void LoadScore4()
        {
            if (loadGame)
                Basic.setScreen(new GameScreen(Savegamescore.Load(SaveState.Four)));
            else
                Basic.setScreen(new GameScreen(1, SaveState.Four));
        }

        private void Back()
        {
            Basic.setScreen(new MainMenuScreen());
        }

        public void Init()
        {
            
        }
    }

    public enum SaveState
    {
        One,
        Two,
        Three,
        Four,
        ByPass
    }
}
