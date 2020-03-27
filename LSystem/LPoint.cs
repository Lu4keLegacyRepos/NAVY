using System.Drawing;

namespace LSystem
{
    struct LPoint
    {
        public float Angle;
        public Point Point;
        public Color Color;

        public LPoint(Point point, float angle, Color color)
        {
            Point = point;
            Angle = angle;
            Color = color;
        }
    }
}
