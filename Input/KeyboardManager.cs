using BlockEngine.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine.Input {

    class KeyboardManager : GameComponent {

        private KeyboardState previous;
        private KeyboardState current;

        public KeyboardManager(Client client) : base(client) {
            Game.Services.AddService<KeyboardManager>(this);
        }

        public override void Initialize() {
            this.previous = Keyboard.GetState();
            this.current = Keyboard.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            if (Enabled) {
                this.previous = this.current;
                this.current = Keyboard.GetState();

                if (IsKeyPressed(Keys.Escape))
                    Game.Exit();

                base.Update(gameTime);
            }
        }

        public bool IsKeyDown(Keys key) {
            return this.previous.IsKeyDown(key) && this.current.IsKeyDown(key);
        }

        public bool IsKeyPressed(Keys key) {
            return this.previous.IsKeyDown(key) && this.current.IsKeyUp(key);
        }

    }

}
