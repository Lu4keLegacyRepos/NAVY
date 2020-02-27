using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork
{
    public class Layer
    {
        public Matrix Inputs { get; set; }
        public Matrix Weights { get; set; }
        public Matrix Outputs { get; set; }
        public Matrix Bias { get; set; }
        public Func<double, double> ActivationFc { get; set; }
        public Func<double, double> ActivationFc_derivitiv { get; set; }

        public Matrix Errors { get; set; }

        public Layer Prev { get; set; }
        public Layer Next { get; set; }


        public Layer(int numOfNeurons,int numOfInputs)
        {
            Weights = new Matrix(numOfNeurons, numOfInputs);
            Weights.Randomize();

            Bias = new Matrix(numOfNeurons, 1);
            Bias.Randomize();
        }

        /// <summary>
        /// Feed Forward
        /// </summary>
        public void ComputeOut()
        {
            Outputs = Matrix.Multiply(Weights,Inputs);
            Outputs.Add(Bias);
            Outputs.Map(x => ActivationFc(x));
  
            if (Next != null)
            {
                Next.Inputs = Outputs;
                Next.ComputeOut();
            }
        }

        public void UpdateWeights(Matrix targets, double learningRate)
        {
            if (Next == null)
            {
                Errors = Matrix.Subtract(targets, Outputs);

                Matrix gradient = Matrix.Copy(Outputs);
                gradient.Map(x => ActivationFc_derivitiv(x));
                gradient.Multiply(Errors);
                gradient.Multiply(learningRate);

                //  Matrix Weight_T = Matrix.Transpose(Inputs);
                Matrix delta = Matrix.Multiply(gradient, Matrix.Transpose(Inputs));

                Weights.Add(delta);
                Bias.Add(gradient);
            }
            else
            {
                Errors = Matrix.Multiply(Matrix.Transpose(Next.Weights), Next.Errors);

                Matrix gradient = Matrix.Copy(Outputs);
                gradient.Map(x => ActivationFc_derivitiv(x));
                gradient.Multiply(Errors);
                gradient.Multiply(learningRate);

                Matrix delta = Matrix.Multiply(gradient, Matrix.Transpose(Inputs));

                Weights.Add(delta);
                Bias.Add(gradient);
            }
            if (Prev != null)
            {
                Prev.UpdateWeights(targets, learningRate);
            }
        }

        public void ConnectTo(Layer nextLayer)
        {
            Next = nextLayer;
            Next.Prev = this;
        }
    }
}
