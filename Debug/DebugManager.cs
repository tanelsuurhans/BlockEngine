using System;
using Microsoft.Xna.Framework;
using BlockEngine.Renderer;
using BlockEngine.Input;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine.Debug {

    class DebugManager : DrawableGameComponent {

        private int frameRate;
        private int frameCounter;

        private TimeSpan elapsed;

        private KeyboardManager keyboardManager;
        private DebugRenderer debugRenderer;

        public DebugManager(Client client) : base(client) {
            this.debugRenderer = new DebugRenderer(client);
            Game.Services.AddService<DebugManager>(this);            
        }

        public override void Initialize() {
            this.keyboardManager = Game.Services.GetService<KeyboardManager>();

            this.frameRate = 0;
            this.frameCounter = 0;

            this.elapsed = TimeSpan.Zero;

            // initialize subcomponents
            this.debugRenderer.Initialize();

            // add subcomponents
            Game.Components.Add(this.debugRenderer);

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {

            if (this.keyboardManager.IsKeyPressed(Keys.F1))
                this.debugRenderer.ToggleDisplay();

            this.elapsed += gameTime.ElapsedGameTime;

            if (this.elapsed >= TimeSpan.FromSeconds(1)) {
                this.elapsed -= TimeSpan.FromSeconds(1);

                this.frameRate = this.frameCounter;
                this.frameCounter = 0;
            }            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            this.frameCounter++;
            base.Draw(gameTime);
        }

        public int FrameRate {
            get { return this.frameRate; }
        }

    }

}
