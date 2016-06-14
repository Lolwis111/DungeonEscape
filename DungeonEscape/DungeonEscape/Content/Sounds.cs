using Microsoft.Xna.Framework.Audio;

namespace DungeonEscape
{
    public static class Sounds
    {
        #region Fields

        public static SoundEffectInstance Collect
        {
            get { return _collect; }
            private set { _collect = value; }
        }
        private static SoundEffectInstance _collect = null;

        public static SoundEffectInstance Destroy
        {
            get { return _destroy; }
            private set { _destroy = value; }
        }
        private static SoundEffectInstance _destroy = null;

        public static SoundEffectInstance Door
        {
            get { return _door; }
            private set { _door = value; }
        }
        private static SoundEffectInstance _door = null;

        public static SoundEffectInstance Click
        {
            get { return _click; }
            private set { _click = value; }
        }
        private static SoundEffectInstance _click = null;

        #endregion

        #region Methods

        public static void LoadSounds()
        {
            //Lädt alle Soundeffekte
            //Verzeichnis: %startup%/Content/Audio

            _collect = loadSound("Audio/collect");
            _destroy = loadSound("Audio/destroy");
            _door = loadSound("Audio/door");
            _click = loadSound("Audio/click");
        }

        private static SoundEffectInstance loadSound(string Path)
        {
            //Lädt einen Soundeffekt
            return Basic.Content.Load<SoundEffect>(Path).CreateInstance();
        }

        public static void SetVolume(float volume)
        {
            //Setzt die Lautstärkeeinstellung für alle Effekte
            _collect.Volume = volume;
            _destroy.Volume = volume;
            _door.Volume = volume;
            _click.Volume = volume;
        }

        public static void UnloadSounds()
        {
            //Gibt alle Soundeffekte wieder frei
            if (_collect != null)
            {
                _collect.Dispose();
                _collect = null;
            }

            if (_destroy != null)
            {
                _destroy.Dispose();
                _destroy = null;
            }

            if (_door != null)
            {
                _door.Dispose();
                _door = null;
            }

            if (_click != null)
            {
                _click.Dispose();
                _click = null;
            }
        }

        #endregion
    }
}
