using BlockEngine.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine.Renderer {

    class SkyRenderer : DrawableGameComponent {

        private CameraManager cameraManager;

        private Model skyModel;
        private Effect skyEffect;

        public SkyRenderer(Client client) : base(client) {
        }

        public override void Initialize() {
            this.cameraManager = Game.Services.GetService<CameraManager>();

            base.Initialize();
        }

        protected override void LoadContent() {
            this.skyEffect = Game.Content.Load<Effect>("Effects\\SkyDome");

            this.skyModel = Game.Content.Load<Model>("Models\\SkyDome");            
            this.skyModel.Meshes[0].MeshParts[0].Effect = this.skyEffect;

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.None, FillMode = FillMode.WireFrame };
            GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

            Matrix cameraMatrix = Matrix.CreateTranslation(new Vector3(this.cameraManager.Position.X, this.cameraManager.Position.Y, this.cameraManager.Position.Z));
            Matrix worldMatrix = Matrix.CreateTranslation(Vector3.Zero) * Matrix.CreateScale(200) * cameraMatrix;

            Matrix[] transforms = new Matrix[this.skyModel.Bones.Count];

            this.skyModel.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in this.skyModel.Meshes) {

                foreach (Effect effect in mesh.Effects) {

                    Matrix world = transforms[mesh.ParentBone.Index] * worldMatrix;

                    effect.CurrentTechnique = effect.Techniques["Default"];
                    
                    effect.Parameters["World"].SetValue(world);
                    effect.Parameters["View"].SetValue(this.cameraManager.View);
                    effect.Parameters["Projection"].SetValue(this.cameraManager.Projection);
                    effect.Parameters["Color"].SetValue(new Color(37, 60, 207).ToVector4());

                }

                mesh.Draw();
            }

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            base.Draw(gameTime);
        }

    }

}
