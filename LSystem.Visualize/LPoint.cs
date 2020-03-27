using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LSystem.Visualize
{
    public struct LPoint
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
