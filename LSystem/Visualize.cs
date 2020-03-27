using Compression_Algorithm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LSystem
{
    public partial class Visualize : Form
    {
        List<StackPoint> nodes = new List<StackPoint>();
        L_system systemAlgorithm;

       // List<LPoint> nodes = new List<LPoint>();
        LSystem system;

        Dictionary<char, string> rules = new Dictionary<char, string>()
            {
            //{'X', "X+YF++YF-FX--FXFX-YF+"}, 

            //{'Y', "-FX+YFYF++YF+FX--FX-Y"},

            //{'F', "FF-[-F+F+F]+[+F-F-F]"},
            // {'F', L_system_string.GenerateRandomPattern(2, 1)}
            // {'F', "FF[-FF][++FF]-[-F+F+F]+[+F-F-F]"},
            //{'A', "F+A"}
            //{'F', "F-F+F+FF-F-F+F"}
            {'F', "F+F-F-FF+F+F-F"}
        };
        public Visualize()
        {
            InitializeComponent();
        }
        void Draw()
        {
            Graphics gc = pictureBox1.CreateGraphics();

            systemAlgorithm = new L_system(new Point(pictureBox1.Size.Width / 2, pictureBox1.Size.Height), 20, 30, 10, 15, 100, 100);

            gc.Clear(Color.White);

            string generated = L_system_string.Generate(rules, L_system_string.GenerateRandomStart(4), 5);
            nodes = systemAlgorithm.Generate(generated);

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                Pen p = new Pen(nodes[i].Color);
                gc.DrawLine(p, nodes[i].Point, nodes[i + 1].Point);
            }

        }
        //public void Draw()
        //{
        //    Graphics gc = pictureBox1.CreateGraphics();

        //    system = new LSystem(new Point(pictureBox1.Size.Width / 2, pictureBox1.Size.Height), 90)
        //        .GenerateString(rules, "F+F+F+F", 5);


        //    gc.Clear(Color.White);

        //    nodes = system.Generate();
        //    gc.DrawRectangle(new Pen(Brushes.Black), new Rectangle(new Point(0, 0), new Size(20,20)));
        //    for (int i = 0; i < nodes.Count - 1; i++)
        //    {
        //        Pen p = new Pen(nodes[i].Color);
        //        gc.DrawLine(p, nodes[i].Point, nodes[i + 1].Point);
        //    }
        //}
        private void btnDraw_Click(object sender, EventArgs e)
        {
            Draw();
        }
    }
}
