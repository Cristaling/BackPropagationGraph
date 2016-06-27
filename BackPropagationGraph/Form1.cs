using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackPropagationGraph
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double scale = 1;

        NeuralNetwork brain;

        public void drawFunction(Func<double, double> function, Color color)
        {
            using (Graphics graph = graphPanel.CreateGraphics())
            {
                double prex = 0;
                double prey = 0;
                Point origin = new Point(graphPanel.Width / 2, graphPanel.Height / 2);
                //Console.WriteLine("Origin point {0} with {1}", origin.X, origin.Y);
                for (int i = -1; i <= graphPanel.Width; i++)
                {
                    double x = i - origin.X;
                    double y = function(x / scale) * scale;
                    y = origin.Y - y;
                    x += origin.X;
                    //Console.WriteLine("Got point {0} with {1}", x, y);
                    graph.DrawLine(new Pen(color), (int)prex, (int)prey, (int)x, (int)y);
                    prex = x;
                    prey = y;
                }
            }
        }

        public void clearFrame()
        {
            using (Graphics graph = graphPanel.CreateGraphics())
            {
                graph.Clear(Color.White);
                graph.DrawLine(new Pen(Color.Black), 0, graphPanel.Height / 2, graphPanel.Width, graphPanel.Height / 2);
                graph.DrawLine(new Pen(Color.Black), graphPanel.Width / 2, 0, graphPanel.Width / 2, graphPanel.Height);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            brain = new NeuralNetwork(1,1,10,10);
            mainTimer.Start();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            scale = trackBar1.Value;
            clearFrame();
            drawFunction(thisSin, Color.Red);
            drawFunction(toTrainSin, Color.Blue);
        }

        public double twoX(double a)
        {
            return 2 * a;
        }

        public double thisSin(double a)
        {
            return Math.Sin(a);
        }

        public double toTrainSin(double a)
        {
            List<double> input = new List<double>();
            input.Add(a / graphPanel.Width);
            brain.setInputs(input);
            List<double> output = brain.getOutputs();
            //Console.WriteLine("Output is {0}", output[0]);
            return output[0] * graphPanel.Height;
        }

        public void doTraining()
        {
            Point origin = new Point(graphPanel.Width / 2, graphPanel.Height / 2);
            //Console.WriteLine("Origin point {0} with {1}", origin.X, origin.Y);
            for (int i = -1; i <= graphPanel.Width; i++)
            {
                double x = i - origin.X;
                brain.trainNetwork(new List<double> { thisSin(x / scale) * scale / graphPanel.Height * 2 }, 0.05);
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            doTraining();
            drawFunction(toTrainSin, Color.Blue);
        }
    }
}
