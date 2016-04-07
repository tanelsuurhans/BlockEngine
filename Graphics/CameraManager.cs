using BlockEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlockEngine.Graphics {

    class CameraManager : GameComponent {

        private float fieldOfView;
        private float aspectRatio;

        private float nearPlane = 0.1f;
        private float farPlane = 1000f;

        private Vector3 position = Vector3.Zero;
        private Vector3 direction = Vector3.Zero;
        private Vector3 movement = Vector3.Zero;

        private Matrix projection;
        private Matrix view;

        private float mouseXSensitivity = 0.1f;
        private float mouseYSensitivity = 0.1f;

        private float mouseXRotation;
        private float mouseYRotation;

        private MouseManager mouseManager;
        private KeyboardManager keyboardManager;

        public CameraManager(Client client) : base(client) {
            Game.Services.AddService<CameraManager>(this);
        }

        public override void Initialize() {
            this.aspectRatio = Game.GraphicsDevice.Viewport.AspectRatio;
            this.fieldOfView = MathHelper.ToRadians(45);
            this.projection = Matrix.CreatePerspectiveFieldOfView(this.fieldOfView, this.aspectRatio, this.nearPlane, this.farPlane);

            this.mouseManager = Game.Services.GetService<MouseManager>();
            this.keyboardManager = Game.Services.GetService<KeyboardManager>();

            base.Initialize();
        }

        public override void Update(GameTime gameTime) {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.UpdateMouse();
            this.UpdateKeyboard();

            Matrix rotationX = Matrix.CreateRotationX(this.mouseYRotation * elapsed);
            Matrix rotationY = Matrix.CreateRotationY(this.mouseXRotation * elapsed);
            Matrix rotation = rotationX * rotationY;

            this.position = this.position + Vector3.Transform(this.movement, rotation);
            this.direction = this.position + Vector3.Transform(Vector3.Forward, rotation);

            this.view = Matrix.CreateLookAt(this.position, this.direction, Vector3.Transform(Vector3.Up, rotation));

            base.Update(gameTime);
        }

        private void UpdateKeyboard() {
            if (Game.IsActive) {
                this.movement = Vector3.Zero;

                if (this.keyboardManager.IsKeyDown(Keys.Left))
                    movement += Vector3.Left;

                if (this.keyboardManager.IsKeyDown(Keys.Right))
                    movement += Vector3.Right;

                if (this.keyboardManager.IsKeyDown(Keys.Up))
                    movement += Vector3.Forward;

                if (this.keyboardManager.IsKeyDown(Keys.Down))
                    movement += Vector3.Backward;

                if (movement.LengthSquared() != 0)
                    movement.Normalize();
            }
        }

        private void UpdateMouse() {
            if (Game.IsActive) {
                this.mouseXRotation -= this.mouseManager.ChangedX() * this.mouseXSensitivity;
                this.mouseYRotation -= this.mouseManager.ChangedY() * this.mouseYSensitivity;
            }
        }

        public Matrix View {
            get { return this.view; }
        }

        public Matrix Projection {
            get { return this.projection; }
        }

        public Vector3 Position {
            get { return this.position; }
        }

        public Vector3 Direction {
            get { return this.direction; }
        }

    }

}
