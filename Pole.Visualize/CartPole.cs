using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Pole.Visualize
{
    public class CartPole
    {
        private Rectangle cart;
        private Rectangle pole;
        private double cartStep;
        private int poleLength = 1;
        private Canvas canvas;

        public double PoleAngle { get; private set; } // in degree
        public double PoleAnglePrev { get; private set; } // in degree
        public bool IsBetterAngle => Math.Abs(0 - PoleAngle) < Math.Abs(0 - PoleAnglePrev); //mensi uhel nez predchozi krok ?
        public double CartX { get; private set; }
        public bool LastMove { get;private set; }



        public static double DegreeToRadian(int degree)
        {
            return degree * Math.PI / 180;
        }
        public CartPole(Rectangle cart, Rectangle pole, double cartStep = 2, int initAngleDegree = 0)
        {
            this.cart = cart;
            this.pole = pole;
            this.cartStep = cartStep;
            PoleAngle = (initAngleDegree);
            CartX = 0;
        }

        public CartPole(Canvas canvas, double cartStep = 1, int initAngleDegree = 0)
        {
            this.canvas = canvas;
            cart = new Rectangle() { Height = 30, Stroke = Brushes.Black, Width = 60, Fill = Brushes.Orange };
            pole = new Rectangle() { Height = 120, Stroke = Brushes.Black, Width = 3, Fill = Brushes.Black, RenderTransformOrigin = new System.Windows.Point(0, 0) };

            Canvas.SetLeft(cart, 370);
            Canvas.SetTop(cart, 350);

            Canvas.SetLeft(pole, 399);
            Canvas.SetTop(pole, 350);

            canvas.Children.Add(cart);
            canvas.Children.Add(pole);
            this.cartStep = cartStep;
            PoleAngle = (initAngleDegree);
        }

        public void Reset()
        {
            canvas.Children.Clear();
            cart = new Rectangle() { Height = 30, Stroke = Brushes.Black, Width = 60, Fill = Brushes.Orange };
            pole = new Rectangle() { Height = 120, Stroke = Brushes.Black, Width = 3, Fill = Brushes.Black, RenderTransformOrigin = new System.Windows.Point(0, 0) };

            Canvas.SetLeft(cart, 370);
            Canvas.SetTop(cart, 350);

            Canvas.SetLeft(pole, 399);
            Canvas.SetTop(pole, 350);

            canvas.Children.Add(cart);
            canvas.Children.Add(pole);
            PoleAngle = 0;
            CartX = 0;
        }
        public void Move(bool? right)
        {
            PoleAnglePrev = PoleAngle;
            PoleAngle += right switch
            {
                true => -1 * Math.Atan(Math.Tan(cartStep / poleLength)),
                false => 1 * Math.Atan(Math.Tan(cartStep / poleLength)),
                null => PoleAngle == 0 ? 0 : PoleAngle < 0 ?
                    -Math.Atan(Math.Tan(10 / poleLength)) * Math.Abs(PoleAngle * .15) :
                    Math.Atan(Math.Tan(10 / poleLength)) * Math.Abs(PoleAngle * .15)
            };
            if (right != null)
            {
                var step = (bool)right ? cartStep : -cartStep;
                LastMove = (bool)right;
                MoveTo(cart, step);
                MoveTo(pole, step);
                CartX += step;

            }
            Rotate(pole, PoleAngle);
        }

        private void MoveTo(Rectangle target, double deltaX)
        {
            var left = Canvas.GetLeft(target);
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim = new DoubleAnimation(left, left + deltaX, TimeSpan.FromSeconds(.5));
            trans.BeginAnimation(Canvas.LeftProperty, anim);

            Canvas.SetLeft(target, left + deltaX);
        }
        private void Rotate(Rectangle target, double angle)
        {

            if (angle < -90 || angle > 90)
            {
                PoleAngle = 0;
            }
            RotateTransform trans = new RotateTransform(angle);
            ScaleTransform scale = new ScaleTransform(-1, -1);

            var transGroup = new TransformGroup();
            transGroup.Children.Add(trans);
            transGroup.Children.Add(scale);
            target.RenderTransform = transGroup;

            //target.RenderTransform = trans;
            //DoubleAnimation anim = new DoubleAnimation(trans.Angle, angle, TimeSpan.FromSeconds(.5));
            //trans.BeginAnimation(TranslateTransform.XProperty, anim);


        }
    }
}
