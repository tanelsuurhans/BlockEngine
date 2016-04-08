using Microsoft.Xna.Framework;

namespace BlockEngine.Input {

    class InputManager : GameComponent {

        private KeyboardManager keyboardManager;
        private MouseManager mouseManager;

        public InputManager(Client client) : base(client) {
            this.keyboardManager = new KeyboardManager(client);
            this.mouseManager = new MouseManager(client);

            Game.Services.AddService<InputManager>(this);
        }

        public override void Initialize() {
            this.mouseManager.Initialize();
            this.keyboardManager.Initialize();

            Game.Components.Add(this.mouseManager);
            Game.Components.Add(this.keyboardManager);

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            this.mouseManager.Enabled = Game.IsActive;
            this.keyboardManager.Enabled = Game.IsActive;

            base.Update(gameTime);
        }

    }

}
