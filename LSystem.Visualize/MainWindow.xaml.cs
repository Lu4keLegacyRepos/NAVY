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

        public void Draw()
        {


            system = new LSystem(new System.Drawing.Point((int)(canvas.Width / 2), (int)(canvas.Height / 2)),2, 60)
                .GenerateString(rules, "F+F+F+F", 5);

            canvas.Children.Add(new Line() { X1 = 0, X2 = 10, Y1 = 0, Y2 = 10, Stroke = Brushes.Red });
            //    gc.Clear(Color.White);

                nodes = system.Generate();
            //    gc.DrawRectangle(new Pen(Brushes.Black), new Rectangle(new System.Drawing.Point(0, 0), new System.Drawing.Size(20, 20)));
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                //Pen p = new Pen(nodes[i].Color);
                //gc.DrawLine(p, nodes[i].Point, nodes[i + 1].Point);

                canvas.Children.Add(new Line() { X1 = nodes[i].Point.X, X2 = nodes[i+1].Point.X, Y1 = nodes[i].Point.Y, Y2 = nodes[i+1].Point.Y, Stroke = Brushes.Red });
            }
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            Draw();
        }
    }
}
