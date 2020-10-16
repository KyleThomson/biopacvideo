using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace SeizureHeatmap
{
    class GraphProperties
    {
        public Bitmap mainPlot;
        public Graphics graphics;
        public int Xmax, Ymax;
        public Pen axisPen;
        public PictureBox picture;
        public Form1 graphForm;
        public int xAxisLength;
        public int yAxisLength;
        public List<float> xTickPoints;
        public List<float> yTickPoints;
    }
}
