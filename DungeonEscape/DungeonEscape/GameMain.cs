using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonEscape
{
    public sealed class GameMain : Game
	{
        public GraphicsDeviceManager Graphics
        {
            get { return _graphics; }
            set { _graphics = value; }
        }
		private GraphicsDeviceManager _graphics;

        public float SoundVolume
        {
            get { return _volume; }
            set { _volume = value; }
        }
        private float _volume = 0.0f;

		private RasterizerState _rs;
		private SamplerState _sampler;

		public GameMain(string[] args)
		{
			_graphics = new GraphicsDeviceManager(this);

            Basic.DebugMode = false;

            //Kommandozeilenparameter überprüfen
            if (args.Length > 0)
            {
                if (args[0] == "-debug")
                {
                    Basic.DebugMode = true;
                }
                else if (args[0].StartsWith("-l"))
                {
                    int level = 0;
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
            _volume = settings.Volume;      //Volume -> Aus settings.xml

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
			_rs = new RasterizerState();
			_rs.MultiSampleAntiAlias = true;
            _rs.CullMode = CullMode.CullCounterClockwiseFace;
            _rs.FillMode = FillMode.Solid;

            //Texturfiltereigenschaften
			_sampler = new SamplerState();
			_sampler.Filter = TextureFilter.Anisotropic;
			_sampler.MaxAnisotropy = 16;
			_sampler.MipMapLevelOfDetailBias = 1f;
			_sampler.MaxMipLevel = 1;
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
