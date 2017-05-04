using System;
using System.Collections.Generic;
using DungeonEscape.Content;
using DungeonEscape.Debug;
using DungeonEscape.SaveGames;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
    internal sealed class GameMain : Game
	{
	    public GraphicsDeviceManager Graphics { get; set; }

	    public float SoundVolume { get; set; }

	    private readonly RasterizerState _rs;
		private readonly SamplerState _sampler;

		public GameMain(IEnumerable<string> args)
		{
            Graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            Basic.DebugMode = false;

            //Kommandozeilenparameter überprüfen
		    foreach (string arg in args)
		    {
		        ParseArgument(arg);
		    }

            if (Basic.DebugMode)
            {
                Console.WriteLine("Debug functions:");
                Console.WriteLine("   <F1> print position");
                Console.WriteLine("   <F2> toggle ceiling");
                Console.WriteLine("   <F3> toggle floor");
                Console.WriteLine("   <F4> toogle bounding boxes");
                Console.WriteLine("   <F5> toggle framerate");
                Console.WriteLine("   <Space> move up");
                Console.WriteLine("   <LShift> move down");
            }

            //Rendereinstellungen
            _rs = new RasterizerState
            {
                MultiSampleAntiAlias = true,
                CullMode = CullMode.CullCounterClockwiseFace,
                FillMode = FillMode.Solid
            };

            //Texturfiltereigenschaften
            _sampler = new SamplerState()
            {
                Filter = TextureFilter.Anisotropic,
                MaxAnisotropy = 16,
                MipMapLevelOfDetailBias = 1f,
                MaxMipLevel = 1
            };
        }

		protected override void Initialize()
		{
            Savegamesettings settings = Savegamesettings.Load();

            Basic.Init(this, settings.Language);

            // Einstellungen laden
            SoundVolume = settings.Volume;      // Volume -> Aus settings.xml

            Graphics.PreferMultiSampling = true;                        // Kantenglättung
            Graphics.IsFullScreen = settings.Fullscreen;                // Vollbild -> Aus settings.xml
            Graphics.PreferredBackBufferWidth = settings.Resolution.X;  // Auflösung (X) -> Aus settings.xml
            Graphics.PreferredBackBufferHeight = settings.Resolution.Y; // Auflösung (Y) -> Aus settings.xml
            Graphics.SynchronizeWithVerticalRetrace = true;             // VSync
            Graphics.ApplyChanges();                                    // Grafikeinstellungen anwenden

            

            // Fenstergröße setzen
            Basic.WindowSize = new Rectangle(0, 0, settings.Resolution.X, settings.Resolution.Y);
            Basic.UseSmallTextures = settings.UseLowTextures;

            base.Initialize();
		}

		protected override void Update(GameTime gameTime)
		{
			Basic.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			GraphicsDevice.SamplerStates[0] = _sampler;
			GraphicsDevice.RasterizerState = _rs;

			Basic.Render();
			base.Draw(gameTime);
		}

		protected override void UnloadContent()
		{
			Basic.UnloadContent();
		}

	    private void ParseArgument(string arg)
	    {
            if (arg == "-debug")
            {
                Basic.DebugMode = true;
            }
            else if (arg.StartsWith("-l"))
            {
                int level;
                if (int.TryParse(arg.Substring(2), out level))
                {
                    Basic.ByPassMenu = true;
                    Basic.ByPassLevel = level;
                }
                else
                {
                    LogWriter.WriteError(new System.ArgumentException(LanguageStrings.ErrorStrings.InvalidLevel));
                    // LogWriter.WriteError(new System.ArgumentException("Ungültiges Level!"));
                    Exit();
                }
            }
        }
	}
}
