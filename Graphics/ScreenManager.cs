using BlockEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine.Graphics {

    class ScreenManager : GameComponent {

        private GraphicsDeviceManager graphics;

        public bool isFullScreenEnabled = false;
        public bool isVerticalSyncEnabled = false;

        private KeyboardManager keyboardManager;

        public ScreenManager(Client client, GraphicsDeviceManager graphics) : base(client) {
            this.graphics = graphics;

            Game.Services.AddService(typeof(ScreenManager), this);
        }

        public override void Initialize() {
            this.keyboardManager = Game.Services.GetService<KeyboardManager>();

            this.graphics.PreferMultiSampling = true;
            this.graphics.PreferredBackBufferWidth = 1024;
            this.graphics.PreferredBackBufferHeight = 768;

            this.graphics.IsFullScreen = isFullScreenEnabled;
            this.graphics.SynchronizeWithVerticalRetrace = isVerticalSyncEnabled;

            this.graphics.ApplyChanges();

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            if (this.keyboardManager.IsKeyPressed(Keys.F11))
                ToggleFullScreen();

            if (this.keyboardManager.IsKeyPressed(Keys.F12))
                ToggleVerticalSync();

            base.Update(gameTime);
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
