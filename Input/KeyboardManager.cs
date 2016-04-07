using BlockEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine.Input {

    class KeyboardManager : GameComponent {

        private KeyboardState previous;
        private KeyboardState current;

        private ScreenManager screenManager;

        public KeyboardManager(Client client) : base(client) {
            Game.Services.AddService<KeyboardManager>(this);
        }

        public override void Initialize() {
            this.previous = Keyboard.GetState();
            this.current = Keyboard.GetState();

            this.screenManager = Game.Services.GetService<ScreenManager>();

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            this.previous = this.current;
            this.current = Keyboard.GetState();

            if (IsKeyPressed(Keys.Escape))
                Game.Exit();

            if (IsKeyPressed(Keys.F11))
                this.screenManager.ToggleFullScreen();

            base.Update(gameTime);
        }

        public bool IsKeyDown(Keys key) {
            return this.previous.IsKeyDown(key) && this.current.IsKeyDown(key);
        }

        public bool IsKeyPressed(Keys key) {
            return this.previous.IsKeyDown(key) && this.current.IsKeyUp(key);
        }

    }

}
