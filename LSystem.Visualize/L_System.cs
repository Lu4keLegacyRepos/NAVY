using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LSystem.Visualize
{
    public class L_System
    {
        private string generatedString;
        public Dictionary<char, Action> grammar;
        private Stack<LPoint> stack = new Stack<LPoint>();

        public int Step = 2;
        float AngleStep = (float)Math.PI / 180 * 90;

        public List<LPoint> points = new List<LPoint>();
        LPoint currentPoint;

        public L_System(Point startPoint, int step, float angleStep)
        {
            // pravidla pro vykreslovani
            grammar = new Dictionary<char, Action>()
            {
                {'F', () => { Forward(); AddPoint(); }},
                {'f', () => { Forward(); }},
                {'[', () => { stack.Push(currentPoint);}},
                {']', () => { currentPoint = stack.Pop(); points.Add(currentPoint);}},
                {'+', () => { Rotate(false); }},
                {'-', () => { Rotate(true); }}
            };


            currentPoint = new LPoint(startPoint, AngleStep, Color.DarkBlue);
            Step = step;
            AngleStep = (float)Math.PI / 180 * angleStep;
        }

        private void AddPoint()
        {
            points.Add(currentPoint);
        }
        private void Forward()
        {
            Point newStackPointLocation = new Point(currentPoint.Point.X + PolarToRectangularCoord(Step, currentPoint.Angle).X,
                currentPoint.Point.Y + PolarToRectangularCoord(Step, currentPoint.Angle).Y);
            currentPoint = new LPoint(newStackPointLocation, currentPoint.Angle, Color.Blue);

        }

        private void Rotate(bool left)
        {
            if (left)
            {
                currentPoint.Angle -= AngleStep;
            }
            else
            {
                currentPoint.Angle += AngleStep;
            }
        }

        // vygenerovaní stringu pro vykresleni o volitelné délce iterací
        public L_System GenerateString(Dictionary<char, string> rules, string axiom, int iterations)
        {
            string result = axiom;

            for (int i = 0; i < iterations; i++)
            {
                string relativeRes = "";
                foreach (char c in result)
                    if (rules.ContainsKey(c))
                    {
                        relativeRes += rules[c];
                    }
                    else
                        relativeRes += c;

                result = relativeRes;
            }
            generatedString = result;
            return this;
        }

        // generování bodů k vykreslení z vygenerovaného stringu
        public List<LPoint> Generate()
        {
            foreach (char c in generatedString)
                grammar[c]();

            return points;
        }

        // prevod souradnic
        private Point PolarToRectangularCoord(float step, float angle)
        {
            return new Point((int)Math.Round(Math.Cos(angle) * step), (int)Math.Round((Math.Sin(angle) * step)));
        }
    }
}
