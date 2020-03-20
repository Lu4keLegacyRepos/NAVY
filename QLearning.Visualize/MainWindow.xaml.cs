using QLearning.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
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
        private int dim = 7;
        private double[,] image;
        private bool run = true;
        private Dictionary<(int X, int Y), Rectangle> indexes;
        private BackgroundWorker worker;
        private BackgroundWorker worker_auto;
        private QLearning.Core.Enviroment env;
        private Agent agent;
        int cnt = 0;
        int cnt_auto = 0;

        /// <summary>
        /// Inicializace prostredi
        /// </summary>
        public MainWindow()
        {
            worker = new BackgroundWorker();
            worker.DoWork += Compute;
            worker.RunWorkerCompleted += ComputeComplete;

            worker_auto = new BackgroundWorker();
            worker_auto.DoWork += Worker_auto_DoWork;
            worker_auto.RunWorkerCompleted += Worker_auto_RunWorkerCompleted;

            var obstacles = new List<(int X, int Y)>();
            {
                var rnd = new Random();
                for (int i = 0; i < dim; i++)
                {
                    obstacles.Add((rnd.Next(dim), rnd.Next(dim)));
                }
            }
            env = new Core.Enviroment(dim, obstacles, (dim - 1, dim - 1));
            agent = new Agent(env,exploreRate: 0.3);

            image = new double[dim, dim];
            indexes = new Dictionary<(int x, int y), Rectangle>();
            InitializeComponent();

            AddGrid(env);
           

        }
        /// <summary>
        /// Update prostredi po akci agenta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_auto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            counter.Content = cnt_auto;
            env.UpdateRectangles();
            Console.Clear();
            Console.Write(agent.PrintMemory());
        }

        private void Worker_auto_DoWork(object sender, DoWorkEventArgs e)
        {
            cnt_auto=agent.AutoMove();
        }

        private void ComputeComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            env.UpdateRectangles();
            Console.Clear();
            Console.Write(agent.PrintMemory());
        }

        private void Compute(object sender, DoWorkEventArgs e)
        {
            agent.MakeMove();
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
            indexes = new Dictionary<(int x, int y), Rectangle>();
        }

        private void ResetCanvas()
        {
            ResetGrid();
            foreach (Rectangle ch in canvas.Children)
            {
                ch.Fill = Brushes.White;
            }
        }
        private void AddGrid(Core.Enviroment env)
        {
            ResetGrid();
            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {

                    var rect = new Rectangle()
                    {
                        Width = 30,
                        Height = 30,
                        MaxHeight = 30,
                        MaxWidth = 30,

                        Stroke = Brushes.Black,
                        Uid = $"{x};{y}",
                    };
                    env.R.First(k => k.Key.Coordinations == (x, y)).Key.UiItem = rect;
                    indexes.Add((x, y), rect);
                    rect.MouseLeftButtonUp += Rect_MouseLeftButtonUp;

                    Canvas.SetTop(rect, y * 30);
                    Canvas.SetLeft(rect, x * 30);
                    canvas.Children.Add(rect);

                }
            }
        }

        private void Rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            worker.RunWorkerAsync();
            cnt++;
            counter.Content = cnt;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
            cnt++;
            counter.Content = cnt;
        }
        private (int x, int y) GetCoord(string coord)
        {
            var text = coord.Split(';').Select(x => int.Parse(x)).ToArray();
            return (text[0], text[1]);
        }

        // reset Agent
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            agent.RandomAgentState();
            cnt = 0;
            counter.Content = cnt;
            env.UpdateRectangles();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            worker_auto.RunWorkerAsync();
        }
    }
}

