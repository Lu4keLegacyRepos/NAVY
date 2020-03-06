using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace QLearning.Visualize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int dim = 30;
        private double[,] image;

        private Dictionary<(int x, int y), int> indexes;
        public MainWindow()
        {
            image = new double[dim, dim];
            indexes = new Dictionary<(int x, int y), int>();
            InitializeComponent();

            AddGrid();




        }

        private void ResetGrid()
        {
            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    image[x, y] = -1;
                }
            }
        }

        private void ResetCanvas()
        {
            ResetGrid();
            foreach (Rectangle ch in canvas.Children)
            {
                ch.Fill = Brushes.White;
            }
        }
        private void AddGrid()
        {
            int cnt = 0;
            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    indexes.Add((x, y), cnt++);
                    var rect = new Rectangle()
                    {
                        Width = 30,
                        Height = 30,
                        MaxHeight = 30,
                        MaxWidth = 30,
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        Uid = $"{x};{y}",
                    };

                    rect.MouseLeftButtonUp += Rect_MouseLeftButtonUp;

                    Canvas.SetTop(rect, y * 30);
                    Canvas.SetLeft(rect, x * 30);
                    canvas.Children.Add(rect);

                }
            }
        }

        private void Rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var s = (Rectangle)sender;
            var coord = GetCoord(s.Uid);
            var tmp = image[coord.x, coord.y];
            if (tmp == -1)
            {
                image[coord.x, coord.y] = 1;
                s.Fill = Brushes.Green;
            }
            else
            {
                image[coord.x, coord.y] = -1;
                s.Fill = Brushes.White;
            }


        }
        private (int x, int y) GetCoord(string coord)
        {
            var text = coord.Split(';').Select(x => int.Parse(x)).ToArray();
            return (text[0], text[1]);
        }

  
    }
}

