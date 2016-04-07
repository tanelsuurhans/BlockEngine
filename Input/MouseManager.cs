using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BlockEngine.Graphics;
using System;

namespace BlockEngine.Input {

    class MouseManager : GameComponent {

        private MouseState previous;
        private MouseState current;

        private ScreenManager screenManager;

        public MouseManager(Client client) : base(client) {
            Game.IsMouseVisible = false;
            Game.Services.AddService<MouseManager>(this);
        }

        public override void Initialize() {
            this.screenManager = Game.Services.GetService<ScreenManager>();

            this.CenterPosition();

            this.previous = Mouse.GetState();
            this.current = Mouse.GetState();

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            System.Console.WriteLine("Game is " + Game.IsActive);

            if (Game.IsActive) {
                this.current = Mouse.GetState();
                this.previous = this.current;

                this.CenterPosition();
                
                base.Update(gameTime);
            }
        }

        public float ChangedX() {
            return this.current.X - (this.screenManager.ScreenWidth / 2);
        }

        public float ChangedY() {
            return this.current.Y - (this.screenManager.ScreenHeight / 2);
        }

        private void CenterPosition() {
            Mouse.SetPosition(
                this.screenManager.ScreenWidth / 2,
                this.screenManager.ScreenHeight / 2
            );
        }

    }
}
