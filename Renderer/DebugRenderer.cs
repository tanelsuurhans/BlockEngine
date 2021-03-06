﻿using BlockEngine.Debug;
using BlockEngine.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Diagnostics;

namespace BlockEngine.Renderer {

    class DebugRenderer : DrawableGameComponent {

        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;

        private DebugManager debugManager;
        private ScreenManager screenManager;
        private CameraManager cameraManager;

        private Process process;

        public DebugRenderer(Client client) : base(client) {
            this.Visible = false;
        }

        public override void Initialize() {
            this.screenManager = Game.Services.GetService<ScreenManager>();
            this.debugManager = Game.Services.GetService<DebugManager>();
            this.cameraManager = Game.Services.GetService<CameraManager>();

            this.process = Process.GetCurrentProcess();

            base.Initialize();
        }

        protected override void LoadContent() {
            this.spriteFont = Game.Content.Load<SpriteFont>("Fonts\\Quantifier_14px");
            this.spriteBatch = new SpriteBatch(Game.GraphicsDevice);

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            int width = this.screenManager.ScreenWidth;
            int height = this.screenManager.ScreenHeight;
            int frames = this.debugManager.FrameRate;

            this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            DrawText(string.Format("FPS: {0}x{1} @ {2}", width, height, frames), 0);
            DrawText(string.Format("RAM: {0} MB", this.process.WorkingSet64 / 1024 / 1024), 1);
            DrawText(string.Format("Position: {0}", this.cameraManager.Position), 2);
            DrawText(string.Format("Direction: {0}", this.cameraManager.Direction), 3);

            this.spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawText(string text, int order) {
            this.spriteBatch.DrawString(this.spriteFont, text, TextLocation(text, order), Color.Black);
        }

        private Vector2 TextLocation(string text, int order) {
            return new Vector2(10, this.spriteFont.MeasureString(text).Y * order + 10);
        }
        
        public void ToggleDisplay() {
            this.Visible = !this.Visible;
        }

    }

}
