using Microsoft.Xna.Framework.Graphics;

namespace Content
{
    public struct Adapter
    {
        public int ScreenWidth;
        public int ScreenHeight;
        public float AspectRatio;
        public string Description;
        public int Id;
        public string Name;

        public static Adapter GetAdapter()
        {
            GraphicsAdapter adapter = GraphicsAdapter.DefaultAdapter;

            return new Adapter
            {
                AspectRatio = adapter.CurrentDisplayMode.AspectRatio, 
                Description = adapter.Description,
                Id = adapter.DeviceId,
                Name = adapter.DeviceName,
                ScreenHeight = adapter.CurrentDisplayMode.Height,
                ScreenWidth = adapter.CurrentDisplayMode.Width 
            };
        }
    }
}
