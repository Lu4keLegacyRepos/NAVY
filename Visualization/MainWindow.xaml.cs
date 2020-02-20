using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void VisualizePerceptron()
        {

        }

        private void DrawPoint((double x, double y, Brush color) point)
        {
            Ellipse circle = new Ellipse()
            {
                Width = 3,
                Height = 3,
                Stroke = point.color,
                StrokeThickness = 6
            };

            canvas.Children.Add(circle);

            circle.SetValue(Canvas.LeftProperty, (double)point.x);
            circle.SetValue(Canvas.TopProperty, (double)point.y);
        }

        private void DrawLine((double x, double y) start, (double x, double y) end)
        {
            var line = new Line();
            line.Stroke = Brushes.Black;
            line.X1 = start.x;
            line.X2 = end.x;
            line.Y1 = start.y;
            line.Y2 = end.y;
            line.StrokeThickness = 2;

            canvas.Children.Add(line);
        }
    }
}
