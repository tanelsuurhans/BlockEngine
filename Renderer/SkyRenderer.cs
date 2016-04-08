using BlockEngine.Graphics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlockEngine.Renderer {

    class SkyRenderer : DrawableGameComponent {

        private CameraManager cameraManager;

        private Model skyModel;
        private Effect skyEffect;

        private Vector4 DayColor = Color.SkyBlue.ToVector4();
        private Vector4 NightColor = Color.Black.ToVector4();
        private Vector4 HorizonColor = Color.White.ToVector4();
        private Vector4 EveningTint = Color.Red.ToVector4();
        private Vector4 MorningTint = Color.Gold.ToVector4();

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
            GraphicsDevice.RasterizerState = new RasterizerState() { CullMode = CullMode.None };
            GraphicsDevice.DepthStencilState = DepthStencilState.DepthRead;

            Matrix cameraMatrix = Matrix.CreateTranslation(new Vector3(this.cameraManager.Position.X, 0, this.cameraManager.Position.Z));
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

                    effect.Parameters["DayColor"].SetValue(this.DayColor);
                    effect.Parameters["NightColor"].SetValue(this.NightColor);
                    effect.Parameters["HorizonColor"].SetValue(this.HorizonColor);
                    //effect.Parameters["MorningTint"].SetValue(this.MorningTint);
                    //effect.Parameters["EveningTint"].SetValue(this.EveningTint);

                }

                mesh.Draw();
            }

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            base.Draw(gameTime);
        }

    }

}
