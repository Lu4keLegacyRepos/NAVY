using Perceptron.DataSet;
using Perceptron.Enums;
using Perceptron.Interfaces;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Visualization
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSetFactory factory = new DataSetFactory();
        Perceptron.Perceptron p;
        private double LineCalc(double x) => 4 * x - 5;
        private List<IDataSet> trainData;

        private int ElapsedEpoch = 0;
        public MainWindow()
        {
            InitializeComponent();
            p = new Perceptron.Perceptron(2);
            EpochLabel.Content = $"Training epocha: {ElapsedEpoch}";
            SetTrainData();
        }
        private void SetTrainData()
        {
            trainData = factory.Create(DataSetType.TrainingSet);
            p.SetTrainData(15, trainData);
        }

        private void VisualizePerceptron()
        {
            canvas.Children.Clear();
            var testData = factory.Create(DataSetType.TestSet);
            foreach (TestSet point in testData)
            {
                DrawPoint((point.Input[0], point.Input[1], OutputConvertor(p.Guess(point.Input))));
            }

            DrawLine((-250, LineCalc(-250)), (250, LineCalc(250)));
        }

        private void VisualizeTraining()
        {
            canvas.Children.Clear();
            var stepData = trainData;
            if (stepData != null)
            {
                foreach (TrainingSet point in trainData)
                {
                    DrawPoint((point.Input[0], point.Input[1], OutputConvertor(p.Guess(point.Input))));
                }
            }
            DrawLine((-250, LineCalc(-250)), (250, LineCalc(250)));
            p.TrainStep();
        }


        private Brush OutputConvertor(double o) => o == 0 ? Brushes.Blue : o < 0 ? Brushes.Red : Brushes.Green;
        private void DrawPoint((double x, double y, Brush color) point)
        {
            Ellipse circle = new Ellipse()
            {
                Width = 7,
                Height = 7,
                Stroke = point.color,
                StrokeThickness = 2
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

        private void border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EpochLabel.Content = $"Training epocha: {++ElapsedEpoch}";
            VisualizeTraining();

        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            VisualizePerceptron();
        }

        private void TrainButton_Click(object sender, RoutedEventArgs e)
        {
            SetTrainData();
            EpochLabel.Content = $"Training epocha: {++ElapsedEpoch}";
            VisualizeTraining();
        }
    }
}
