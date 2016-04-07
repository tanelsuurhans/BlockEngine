using System;
using Microsoft.Xna.Framework;
using BlockEngine.Renderer;

namespace BlockEngine.Debug {

    class DebugManager : DrawableGameComponent {

        private int frameRate;
        private int frameCounter;

        private TimeSpan elapsed;

        private DebugRenderer debugRenderer;

        public DebugManager(Client client) : base(client) {
            this.debugRenderer = new DebugRenderer(client);
            Game.Services.AddService<DebugManager>(this);            
        }

        public override void Initialize() {
            Game.Components.Add(this.debugRenderer);

            this.frameRate = 0;
            this.frameCounter = 0;

            this.elapsed = TimeSpan.Zero;

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
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
