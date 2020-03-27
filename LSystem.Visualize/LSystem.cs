using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LSystem.Visualize
{
    public class LSystem
    {
        private string generatedString;
        public Dictionary<char, Action> grammar;
        private Stack<LPoint> stack = new Stack<LPoint>();

        public int Step = 2;
        float AngleStep = (float)Math.PI / 180 * 90;

        public List<LPoint> points = new List<LPoint>();
        LPoint currentPoint;


        private int probabilityToChangeLength = 10;
        private int minStepLength = 5;
        private int maxStepLength = 15;

        private int probabilityToChangeAngle = 10;
        private int minAngleStep = 90;
        private int maxAngleStep = 90;

        public LSystem(Point startPoint, int step, int angleStep)
        {
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


        private void ChangeLength()
        {
            Random n = new Random();

            if (n.Next(probabilityToChangeLength) % 10 == 0 || Step == 0)
            {
                Step = n.Next(minStepLength, maxStepLength + 1);
            }
        }

        private void ChangeAngleStep()
        {
            Random n = new Random();

            if (n.Next(probabilityToChangeAngle) % 10 == 0 || AngleStep == 0)
            {
                AngleStep = (float)(n.Next((int)minAngleStep, (int)maxAngleStep + 1) * Math.PI / 180);
            }
        }
        private void AddPoint()
        {
            points.Add(currentPoint);
        }
        private void Forward()
        {
          //  ChangeAngleStep();
         //   ChangeLength();

            Point newStackPointLocation = new Point(currentPoint.Point.X + PolarToRectangularCoord(Step, currentPoint.Angle).X,
                currentPoint.Point.Y + PolarToRectangularCoord(Step, currentPoint.Angle).Y);

            currentPoint = new LPoint(newStackPointLocation, currentPoint.Angle, Color.Blue);
            //if (currentPoint.Angle == points[points.Count - 1].Angle)
            //     currentPoint.Color = Color.DarkBlue;
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
        public LSystem GenerateString(Dictionary<char, string> rules, string axiom, int iterations)
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

        public List<LPoint> Generate()
        {
            foreach (char c in generatedString)
                grammar[c]();

            return points;
        }

        private Point PolarToRectangularCoord(float step, float angle)
        {
            return new Point((int)Math.Round(Math.Cos(angle) * step), (int)Math.Round((Math.Sin(angle) * step)));
        }
    }
}
