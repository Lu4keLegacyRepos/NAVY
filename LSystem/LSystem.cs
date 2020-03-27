using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LSystem
{
    class LSystem
    {
        private string generatedString;
        private Dictionary<char, Action> grammar;
        private Stack<LPoint> stack = new Stack<LPoint>();

        public int Step = 1;
        float AngleStep = (float)Math.PI / 180 * 90;

        private List<LPoint> points = new List<LPoint>();
        LPoint currentPoint;


        private int probabilityToChangeLength = 10;
        private int minStepLength=5;
        private int maxStepLength = 15;

        private int probabilityToChangeAngle=10;
        private int minAngleStep=0;
        private int maxAngleStep=0;

        public LSystem(Point startPoint, int angleStep)
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
            AngleStep = angleStep;
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
            //ChangeAngleStep();
           // ChangeLength();

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






namespace Compression_Algorithm
{
    struct StackPoint
    {
        public float Angle;
        public Point Point;
        public Color Color;

        public StackPoint(Point point, float angle, Color color)
        {
            Point = point;
            Angle = angle;
            Color = color;
        }
    }

    class L_system
    {
        int minStepLength;
        int maxStepLength;
        public int Step = 1;

        float AngleStep = (float)Math.PI / 180 * 90;
        float maxAngleStep;
        float minAngleStep;

        Dictionary<char, Action> dic;

        Stack<StackPoint> stack = new Stack<StackPoint>();

        List<StackPoint> points = new List<StackPoint>();
        StackPoint currentPoint;

        int probabilityToChangeLength;
        int probabilityToChangeAngle;

        public L_system(Point startPoint, float minAngleInDegrees, float maxAngleInDegrees, int minStepLength, int maxStepLength, int probabilityToChangeAngle, int probabilityToChangeLength)
        {
            this.minStepLength = minStepLength;
            this.maxStepLength = maxStepLength;

            this.maxAngleStep = maxAngleInDegrees;
            this.minAngleStep = minAngleInDegrees;

            this.probabilityToChangeAngle = probabilityToChangeAngle;
            this.probabilityToChangeLength = probabilityToChangeLength;

            Step = 0;
            AngleStep = 0;

            ChangeAngleStep();
            ChangeLength();

            currentPoint = new StackPoint(startPoint, AngleStep * 15, Color.Brown);

            dic = new Dictionary<char, Action>()
            {
                {'F', () => { Move(); AddPoint(); }},
                {'f', () => { Move(); }},
                {'[', () => { stack.Push(currentPoint);}},
                {']', () => { currentPoint = stack.Pop(); points.Add(currentPoint);}},
                {'+', () => { Rotate(false); }},
                {'-', () => { Rotate(true); }}
            };

            points = new List<StackPoint>();
            points.Add(currentPoint);
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

        private void Move()
        {
            ChangeAngleStep();
            ChangeLength();

            Point newStackPointLocation = new Point(currentPoint.Point.X + ConvertPolToRect(Step, currentPoint.Angle).X, currentPoint.Point.Y + ConvertPolToRect(Step, currentPoint.Angle).Y);

            currentPoint = new StackPoint(newStackPointLocation, currentPoint.Angle, Color.Green);
            if (currentPoint.Angle == points[points.Count - 1].Angle)
                currentPoint.Color = Color.Brown;
        }

        private Point ConvertPolToRect(float step, float angle)
        {
            return new Point((int)Math.Round(Math.Cos(angle) * step), (int)Math.Round((Math.Sin(angle) * step)));
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

        private void AddPoint()
        {
            points.Add(currentPoint);
        }

        public List<StackPoint> Generate(string rules)
        {
            foreach (char c in rules)
                dic[c]();

            return points;
        }
    }

    class L_system_string
    {
        public static string Generate(Dictionary<char, string> rules, string axiom, int iterations)
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

            return result;
        }

        public static string GenerateRandomPattern(int length, int leaves)
        {
            string leafLeft = "[-F+F+F]";
            string leafRight = "[+F-F-F]";

            char forward = 'F';

            string res = "";
            Random n = new Random();

            for (int i = 0; i < length; i++)
            {
                int random = n.Next(3);

                res += forward;

            }

            for (int i = 0; i < leaves; i++)
            {
                res += "-" + leafLeft + "+" + leafRight;
            }

            return res;
        }

        public static string GenerateRandomStart(int length)
        {
            string res = "";
            Random n = new Random();

            for (int i = 0; i < length; i++)
            {

                int random = n.Next(3);

                if (random == 0)
                    res += "F";
                else if (random == 1)
                    res += "-";
                else
                    res += "+";
            }
            return res;
        }
    }
}