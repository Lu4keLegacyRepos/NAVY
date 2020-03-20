using Pole.QLearning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Pole.Visualize
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CartPole cp = null;
        BackgroundWorker worker = null;
        Enviroment enviroment = null;
        Agent agent = null;

        public MainWindow()
        {
            InitializeComponent();
            cp = new CartPole(canvas);
            enviroment = new Enviroment(8);
            agent = new Agent(enviroment, (-3, 3), (-15, 15));

            
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(50);

                Dispatcher.Invoke(() =>
                {
                    cp.Move(null);
                    cp.Move(agent.MakeMove(cp.CartX, cp.PoleAngle,cp.LastMove));

                });
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cp.Move(false);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            cp.Move(true);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                cp.Move(false);
            }
            if (e.Key == Key.Right)
            {
                cp.Move(true);
            }
            if (e.Key == Key.Space)
            {
                cp.Reset();
            }

        }
    }
}
