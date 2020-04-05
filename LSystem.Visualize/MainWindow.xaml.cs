using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LSystem.Visualize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<LPoint> nodes = new List<LPoint>();
        LSystem system;

        Dictionary<char, string> rules = new Dictionary<char, string>()
        {
            {'F', "F+F--F+F"}
        };
        public MainWindow()
        {
            InitializeComponent();
            canvas.SizeChanged += Canvas_SizeChanged;
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            canvas.Height = e.NewSize.Height;
            canvas.Width = e.NewSize.Width;
        }


        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            /*
               1. Axiom: F+F+F+F
               Rule: F -> F+F-F-FF+F+F-F
               Angle: 90°
            */
            canvas.Children.Clear();
            rules = new Dictionary<char, string>()
            {
                {'F', "F+F-F-FF+F+F-F"}
            };
            system = new LSystem(new System.Drawing.Point((int)(canvas.Width / 2), (int)(canvas.Height / 5)), 1, 90)
                .GenerateString(rules, "F+F+F+F", 4);
            nodes = system.Generate();
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                canvas.Children.Add(new Line() { X1 = nodes[i].Point.X, X2 = nodes[i + 1].Point.X, Y1 = nodes[i].Point.Y, Y2 = nodes[i + 1].Point.Y, Stroke = Brushes.Red });
            }
        }

        private void btnDraw2_Click(object sender, RoutedEventArgs e)
        {
            /*
            2.  Axiom: F++F++F
            Rule: F->F + F--F + F
            Angle: 60°
            */
            canvas.Children.Clear();
            var axiom = "F++F++F";
            var angle = 60;
            rules = new Dictionary<char, string>()
            {
                {'F', "F+F--F+F"}
            };
            system = new LSystem(new System.Drawing.Point((int)(canvas.Width / 2), (int)(canvas.Height / 6)), 15, angle)
                .GenerateString(rules, axiom, 3);
            nodes = system.Generate();
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                canvas.Children.Add(new Line() { X1 = nodes[i].Point.X, X2 = nodes[i + 1].Point.X, Y1 = nodes[i].Point.Y, Y2 = nodes[i + 1].Point.Y, Stroke = Brushes.Red });
            }
        }

        private void btnDraw3_Click(object sender, RoutedEventArgs e)
        {
            /*
                3. Axiom: F
                   Rule: F -> F[+F]F[-F]F
                   Angle: pi/7
            */
            canvas.Children.Clear();
            var axiom = "F";
            float angle = 25.71428571F; //PI/7
            rules = new Dictionary<char, string>()
            {
                {'F', "F[+F]F[-F]F"}
            };
            system = new LSystem(new System.Drawing.Point((int)(canvas.Width / 2), 0), 20, angle)
                .GenerateString(rules, axiom, 5);
            nodes = system.Generate();
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                canvas.Children.Add(new Line() { X1 = nodes[i].Point.X, X2 = nodes[i + 1].Point.X, Y1 = nodes[i].Point.Y, Y2 = nodes[i + 1].Point.Y, Stroke = Brushes.Red });
            }
        }

        private void btnDraw4_Click(object sender, RoutedEventArgs e)
        {
            /*
            4. Axiom: F
               Rule: F -> FF+[+F-F-F]-[-F+F+F]
               Angle: pi/8
            */
            canvas.Children.Clear();
            var axiom = "F";
            float angle = 22.5F; //PI/8
            rules = new Dictionary<char, string>()
            {
                {'F', "FF+[+F-F-F]-[-F+F+F]"}
            };
            system = new LSystem(new System.Drawing.Point((int)(canvas.Width / 5*3), 0), 12, angle)
                .GenerateString(rules, axiom, 4);
            nodes = system.Generate();
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                canvas.Children.Add(new Line() { X1 = nodes[i].Point.X, X2 = nodes[i + 1].Point.X, Y1 = nodes[i].Point.Y, Y2 = nodes[i + 1].Point.Y, Stroke = Brushes.Red });
            }
        }
    }
}
