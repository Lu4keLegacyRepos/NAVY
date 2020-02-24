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

        private readonly UIElement[] defaultCanvas;

        private List<IDataSet> trainData;

        private int ElapsedEpoch = 0;
        public MainWindow()
        {
            InitializeComponent();
            p = new Perceptron.Perceptron(2);
            EpochLabel.Content = $"Training epocha: {ElapsedEpoch}";
            lineLabel.Content = $"Line: y={p.LineEquations.a}x+({p.LineEquations.b})";
            SetTrainData();

            W1.Content = p.Weights[0];
            W2.Content = p.Weights[1];
            b.Content = p.Bias;
            //  Error.Content = p.Error;

            defaultCanvas = new UIElement[canvas.Children.Count];
            canvas.Children.CopyTo(defaultCanvas, 0);

        }
        private void SetTrainData()
        {
            trainData = factory.Create(DataSetType.TrainingSet, p.LineEquations);
            p.SetTrainData(trainData);
        }

        private void VisualizePerceptron()
        {
            ResetCanvas();
            var testData = factory.Create(DataSetType.TestSet);
            foreach (TestSet point in testData)
            {
                DrawPoint((point.Input[0], point.Input[1], OutputConvertor((int)p.Guess(point.Input))));
            }

            var tmpStartX = p.LineEquations.GetX(-250);
            var tmpEndX = p.LineEquations.GetX(250);
            DrawLine((tmpStartX, p.LineEquations.GetY(tmpStartX)), (tmpEndX, p.LineEquations.GetY(tmpEndX)));
        }

        private void VisualizeTraining()
        {
            ResetCanvas();
            var stepData = trainData;
            if (stepData != null)
            {
                foreach (TrainingSet point in trainData)
                {
                    DrawPoint((point.Input[0], point.Input[1], OutputConvertor((int)p.Guess(point.Input))));
                }
            }
            var tmpStartX = p.LineEquations.GetX(-250);
            var tmpEndX = p.LineEquations.GetX(250);
            DrawLine((tmpStartX, p.LineEquations.GetY(tmpStartX)), (tmpEndX, p.LineEquations.GetY(tmpEndX)));
            p.TrainStep();
        }

        private void ResetCanvas()
        {
            canvas.Children.Clear();
            foreach (UIElement child in defaultCanvas)
            {
                canvas.Children.Add(child);

            }
        }
        private Brush OutputConvertor(int o) => o switch
        {
            -1 => Brushes.Red,
            1 => Brushes.Green,
            _ => Brushes.Purple
        };

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
            line.StrokeThickness = 1;

            canvas.Children.Add(line);
        }

        private void border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VisualizeTraining();
            EpochLabel.Content = $"Training epocha: {++ElapsedEpoch}";
            W1.Content = p.Weights[0];
            W2.Content = p.Weights[1];
            b.Content = p.Bias;
            //  Error.Content = p.Error;


        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            VisualizePerceptron();

        }

        private void TrainButton_Click(object sender, RoutedEventArgs e)
        {
            SetTrainData();
            VisualizeTraining();
            EpochLabel.Content = $"Training epocha: {++ElapsedEpoch}";

        }

        private void Train100Button_Click(object sender, RoutedEventArgs e)
        {
            p.Train(99, trainData);
            VisualizeTraining();
            ElapsedEpoch += 100;
            EpochLabel.Content = $"Training epocha: {ElapsedEpoch}";
            W1.Content = p.Weights[0];
            W2.Content = p.Weights[1];
            b.Content = p.Bias;
        }
    }
}
