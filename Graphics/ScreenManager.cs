using Microsoft.Xna.Framework;

namespace BlockEngine.Graphics {

    class ScreenManager : GameComponent {

        GraphicsDeviceManager graphics;

        public bool isFullScreenEnabled = false;
        public bool isVerticalSyncEnabled = false;

        public ScreenManager(Client client, GraphicsDeviceManager graphics) : base(client) {
            this.graphics = graphics;

            Game.Services.AddService(typeof(ScreenManager), this);
        }

        public override void Initialize() {
            this.graphics.PreferMultiSampling = true;
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;

            this.graphics.IsFullScreen = isFullScreenEnabled;
            this.graphics.SynchronizeWithVerticalRetrace = isVerticalSyncEnabled;

            this.graphics.ApplyChanges();

            base.Initialize();
        }

        public void ToggleFullScreen() {
            this.isFullScreenEnabled = !this.isFullScreenEnabled;

            this.graphics.IsFullScreen = isFullScreenEnabled;
            this.graphics.ApplyChanges();
        }

        public void ToggleVerticalSync() {
            this.isVerticalSyncEnabled = !this.isVerticalSyncEnabled;

            this.graphics.SynchronizeWithVerticalRetrace = this.isVerticalSyncEnabled;
            this.graphics.ApplyChanges();
        }

        public int ScreenWidth {
            get { return Game.Window.ClientBounds.Width; }
        }

        public int ScreenHeight {
            get { return Game.Window.ClientBounds.Height; }
        }

    }

}
