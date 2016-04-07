﻿using BlockEngine.Debug;
using BlockEngine.Graphics;
using BlockEngine.Input;
using BlockEngine.Renderer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine {

    public class Client : Game {

        GraphicsDeviceManager graphics;

        private ScreenManager screenManager;
        private CameraManager cameraManager;

        private MouseManager mouseManager;
        private KeyboardManager keyboardManager;

        private SkyRenderer skyRenderer;

        private DebugManager debugManager;

        public Client() {
            Content.RootDirectory = "Content";

            this.graphics = new GraphicsDeviceManager(this);

            this.screenManager = new ScreenManager(this, graphics);
            this.keyboardManager = new KeyboardManager(this);
            this.mouseManager = new MouseManager(this);
            this.cameraManager = new CameraManager(this);
            this.skyRenderer = new SkyRenderer(this);
            this.debugManager = new DebugManager(this);
        }

        protected override void Initialize() {
            Components.Add(screenManager);
            Components.Add(mouseManager);
            Components.Add(keyboardManager);
            Components.Add(cameraManager);
            Components.Add(skyRenderer);
            Components.Add(debugManager);

            base.Initialize();
        }

        protected override void LoadContent() {
            base.LoadContent();
        }

        protected override void UnloadContent() {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            BasicEffect effect = new BasicEffect(GraphicsDevice);

            VertexPositionColor[] vertices = new VertexPositionColor[3];
            vertices[0] = new VertexPositionColor(new Vector3(0, 10, 0), Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(-10, -10, 0), Color.Green);
            vertices[2] = new VertexPositionColor(new Vector3(10, -10, 0), Color.Blue);

            VertexBuffer buffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            buffer.SetData<VertexPositionColor>(vertices);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            GraphicsDevice.SetVertexBuffer(buffer);

            effect.World = Matrix.CreateTranslation(0, 0, -100);
            effect.View = this.cameraManager.View;
            effect.Projection = this.cameraManager.Projection;
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {

                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);

            }

            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            base.Draw(gameTime);
        }

    }

}
