using OpenTK;

namespace IcedEngine
{
    public class Transform
    {
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 LocalScale { get; set; }
        public Matrix4 TransformationMatrix => CalculateTransformationMatrix(); 

        public Transform() : this(Vector2.Zero, 0f, Vector2.One) { }
        public Transform(float x, float y) : this(new Vector2(x, y), 0f, Vector2.One) { }
        public Transform(float x, float y, float rotation) : this(new Vector2(x, y), rotation, Vector2.One) { }
        public Transform(float x, float y, float rotation, float scaleX, float scaleY) : this(new Vector2(x, y), rotation, new Vector2(scaleX, scaleY)) { }
        public Transform(Vector2 position, float rotation, Vector2 localScale)
        {
            Position = position;
            Rotation = rotation;
            LocalScale = localScale;
        }

        public void Translate(float translationFactor)
        {
            Translate(translationFactor, translationFactor);
        }

        public void Translate(float x, float y)
        {
            Translate(new Vector2(x, y));
        }

        public void Translate(Vector2 position)
        {
            Position += position;
        }

        public void Rotate(float rotation)
        {
            Rotation += rotation;
        }

        public void Scale(float scaleFactor)
        {
            Scale(scaleFactor, scaleFactor);
        }

        public void Scale(float x, float y)
        {
            Scale(new Vector2(x, y));
        }

        public void Scale(Vector2 scaleFactor)
        {
            LocalScale += scaleFactor;
        }

        private Matrix4 CalculateTransformationMatrix()
        {
            var translation =  Matrix4.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
            var rotation = Matrix4.CreateFromQuaternion(Quaternion.FromEulerAngles(Rotation, 0, 0));
            var scale = Matrix4.CreateScale(new Vector3(LocalScale.X, LocalScale.Y, 1));

            var transformationMatrix = translation * (rotation * scale);
            return transformationMatrix;
        }
    }
}
