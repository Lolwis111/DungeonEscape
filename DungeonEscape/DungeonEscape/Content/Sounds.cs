using Microsoft.Xna.Framework.Audio;

namespace DungeonEscape.Content
{
    public static class Sounds
    {
        #region Fields

        public static SoundEffectInstance Collect;

        public static SoundEffectInstance Destroy;

        public static SoundEffectInstance Door;

        public static SoundEffectInstance Click;

        #endregion

        #region Methods

        public static void LoadSounds()
        {
            //Lädt alle Soundeffekte
            //Verzeichnis: %startup%/Content/Audio

            Collect = LoadSound("Audio\\collect");
            Destroy = LoadSound("Audio\\destroy");
            Door = LoadSound("Audio\\door");
            Click = LoadSound("Audio\\click");
        }

        private static SoundEffectInstance LoadSound(string path)
        {
            //Lädt einen Soundeffekt
            return Basic.Content.Load<SoundEffect>(path).CreateInstance();
        }

        public static void SetVolume(float volume)
        {
            //Setzt die Lautstärkeeinstellung für alle Effekte
            Collect.Volume = volume;
            Destroy.Volume = volume;
            Door.Volume = volume;
            Click.Volume = volume;
        }

        public static void UnloadSounds()
        {
            //Gibt alle Soundeffekte wieder frei
            Collect?.Dispose();

            Destroy?.Dispose();

            Door?.Dispose();

            Click?.Dispose();
        }

        #endregion
    }
}
