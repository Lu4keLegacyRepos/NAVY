using HopfieldNetwork;
using MathNet.Numerics.LinearAlgebra;
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

namespace TestWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int dim = 30;
        private double[,] image;
        private Network network;

        private Dictionary<(int x, int y), int> indexes;
        public MainWindow()
        {
            image = new double[dim, dim];
            indexes = new Dictionary<(int x, int y), int>();
            InitializeComponent();

            AddGrid();
            ResetImage();
            network = new Network(dim);
            
            
            
        }
        
        private void ResetImage()
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
            ResetImage();
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

                    Canvas.SetTop(rect, y*30);
                    Canvas.SetLeft(rect, x*30);
                    canvas.Children.Add(rect);
                    
                }
            }
        }

        private void Rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var s = (Rectangle)sender;
            var coord = GetCoord(s.Uid);
            var tmp = image[coord.x, coord.y]; 
            if(tmp == -1)
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

        private Matrix<double> ImageToMatrix => CreateMatrix.DenseOfArray(image);
        
        private void SetImage(Matrix<double> matrix)
        {
           image = matrix.ToArray();

            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    var tmp = image[x, y];
                    if (tmp == -1)
                    {
                        ((Rectangle)canvas.Children[indexes[(x, y)]]).Fill = Brushes.White;
                    }
                    else
                    {
                        ((Rectangle)canvas.Children[indexes[(x, y)]]).Fill = Brushes.Red;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // learn

            network.Train(ImageToMatrix);
            ResetCanvas();
            counter.Text = $"{network.PatternCount}/{network.MaxPatternCount}";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // recognize
            var tmp = network.Recognize(ImageToMatrix);
            //Console.Write(ImageToMatrix.ToMatrixString() + "\n\n");
            //Console.Write(tmp.ToMatrixString() + "\n\n");
            ResetCanvas();
            SetImage(tmp);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //reset
            network = new Network(dim);
            ResetCanvas();
            counter.Text = $"{network.PatternCount}/{network.MaxPatternCount}";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //clear canvas
            ResetCanvas();
        }
    }
}
