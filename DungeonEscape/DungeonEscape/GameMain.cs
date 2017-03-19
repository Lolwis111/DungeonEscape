using System.Collections.Generic;
using DungeonEscape.Debug;
using DungeonEscape.SaveGames;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
    internal sealed class GameMain : Game
	{
	    public GraphicsDeviceManager Graphics
	    {
	        get { return _graphics; }
	        set { _graphics = value; }
	    }
        private GraphicsDeviceManager _graphics;

	    public float SoundVolume
	    {
	        get { return _soundVolume; }
	        set { _soundVolume = value; }
	    }
	    private float _soundVolume;

	    private readonly RasterizerState _rs;
		private readonly SamplerState _sampler;

		public GameMain(IList<string> args)
		{
            _graphics = new GraphicsDeviceManager(this);

            Basic.DebugMode = false;

            //Kommandozeilenparameter überprüfen
            if (args.Count > 0)
            {
                if (args[0] == "-debug")
                {
                    Basic.DebugMode = true;
                }
                else if (args[0].StartsWith("-l"))
                {
                    int level;
                    if (int.TryParse(args[0].Substring(2), out level))
                    {
                        Basic.ByPassMenu = true;
                        Basic.ByPassLevel = level;
                    }
                    else
                    {
                        LogWriter.WriteError(new System.ArgumentException("Ungültiges Level!"));
                        Exit();
                    }
                }
            }
                

            //Einstellungen laden
            Savegamesettings settings = Savegamesettings.Load();
            _soundVolume = settings.Volume;      //Volume -> Aus settings.xml

			Content.RootDirectory = "Content";

            _graphics.PreferMultiSampling = true;                        //Kantenglättung
            _graphics.IsFullScreen = settings.Fullscreen;                //Vollbild -> Aus settings.xml
            _graphics.PreferredBackBufferWidth = settings.Resolution.X;  //Auflösung (X) -> Aus settings.xml
            _graphics.PreferredBackBufferHeight = settings.Resolution.Y; //Auflösung (Y) -> Aus settings.xml
            _graphics.SynchronizeWithVerticalRetrace = true;             //VSync
            _graphics.ApplyChanges();                                    //Grafikeinstellungen anwenden

            //Fenstergröße setzen
			Basic.WindowSize = new Rectangle(0, 0, settings.Resolution.X, settings.Resolution.Y);
            Basic.UseSmallTextures = settings.UseLowTextures;

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
			Basic.Init(this);
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
	}
}
