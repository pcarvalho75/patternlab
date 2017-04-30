using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PeptideAnotation
{
    public partial class PeptideAnotator : UserControl
    {
        int spacer = 20;
        int fontSize = 19;


        int x1Offset = 10;
        int y1Offset = 10;

        Bitmap imageCache = new Bitmap(1, 1);

        public PeptideAnotator()
        {
            InitializeComponent();
        }

        public void DrawIntraLink(string peptide, string anotation, int xlPos1, int xlPos2)
        {
            //Start things up
            List<AnnotationItem> myAnnotations; Graphics g; Font f;
            StartUpTheCanvas(anotation, out myAnnotations, out g, out f);

            //Draw the AAs and ion annotation
            for (int i = 0; i < peptide.Length; i++)
            {
                g.DrawString(peptide[i].ToString(), f, Brushes.Green, new Point((i * spacer) + x1Offset, y1Offset));

                //Write the b series
                if (myAnnotations.Exists(a => a.Series.Equals("b") && a.IonNo == i))
                {
                    DrawLeftBreak((i * spacer) + x1Offset + 2, 4 + y1Offset, g); 
                }

                //Write the y series
                if (myAnnotations.Exists(a => a.Series.Equals("y") && a.IonNo == i))
                {
                    DrawRightBreak(( (peptide.Length - i) * spacer) + x1Offset + 2, 4 + y1Offset, g);
                }

            }

            DrawIntraLinker(xlPos1, xlPos2, g);
        }

        public  void DrawInterLink(string peptide1, string peptide2, string anotationAlfa, string anotationBeta, int xlPos1, int xlPos2)
        {
            List<AnnotationItem> myAnnotationsAlfa; Graphics g; Font f;
            StartUpTheCanvas(anotationAlfa, out myAnnotationsAlfa, out g, out f);
            List<AnnotationItem> myAnnotationsBeta = ParseAnnotation(anotationBeta);

            //Find out who will get an offset
            int DeltaXLOffset = xlPos1 - xlPos2;
            if (xlPos1 > xlPos2)
            {
                //Add spaces to peptide 2
                for (int i = 0; i < DeltaXLOffset; i++)
                {
                    peptide2 = " " + peptide2;
                }
                xlPos2 += DeltaXLOffset;
            }
            else if (xlPos2 > xlPos1)
            {
                //Add spaces to peptide 1
                for (int i = 0; i < Math.Abs(DeltaXLOffset); i++)
                {
                    peptide1 = " " + peptide1;
                }
                xlPos1 += Math.Abs(DeltaXLOffset);
            }

            //Draw Peptide 1

            for (int i = 0; i < peptide1.Length; i++)
            {
                g.DrawString(peptide1[i].ToString(), f, Brushes.Green, new Point((i * spacer) + x1Offset, y1Offset));

                //Write the b series
                int increment = 0;
                if (xlPos2 - DeltaXLOffset > xlPos1)
                {
                    increment = Math.Abs(DeltaXLOffset);
                }

                if (myAnnotationsAlfa.Exists(a => a.Series.Equals("b") && a.IonNo == i - increment))
                {
                    DrawLeftBreak((i * spacer) + x1Offset + 2, 4 + y1Offset, g);
                }

                //Write the y series
                if (myAnnotationsAlfa.Exists(a => a.Series.Equals("y") && a.IonNo == i - increment))
                {
                    DrawRightBreak(((peptide1.Length - (i - increment)) * spacer) + x1Offset + 2, 4 + y1Offset, g);
                }
            }

            //Draw Peptide 2
            for (int i = 0; i < peptide2.Length; i++)
            {
                int increment = 0;
                if (xlPos1 - DeltaXLOffset < xlPos2)
                {
                    increment = DeltaXLOffset;
                }

                if (myAnnotationsBeta.Exists(a => a.Series.Equals("b") && a.IonNo == i - increment))
                {
                    DrawLeftBreak((i * spacer) + x1Offset + 2,  y1Offset + (3 * fontSize) -2, g);
                }

                //Write the y series
                if (myAnnotationsBeta.Exists(a => a.Series.Equals("y") && a.IonNo == i - increment))
                {
                    DrawRightBreak(((peptide2.Length - (i - increment)) * spacer) + x1Offset + 2, y1Offset + (3 * fontSize) - 2, g);
                }

                g.DrawString(peptide2[i].ToString(), f, Brushes.Green, new Point((i * spacer) + x1Offset, y1Offset + (3 * fontSize) - 5));
            }

            //Draw the Interlinker
            Pen p = new Pen(Brushes.Black);
            p.Width = 3;
            Point p1 = new Point(((xlPos1 - 1) * spacer) + x1Offset + (fontSize / 2) + 2, y1Offset + fontSize + 5);
            Point p2 = new Point(((xlPos1 - 1) * spacer) + x1Offset + (fontSize / 2) + 2, y1Offset + fontSize + (2 * fontSize));
            g.DrawLine(p, p1, p2);    }

        private void DrawIntraLinker(int xlPos1, int xlPos2, Graphics g)
        {
            IntraLinkerVerticalLine(xlPos1, g);
            IntraLinkerVerticalLine(xlPos2, g);

            Pen p = new Pen(Brushes.Black);
            p.Width = 2;

            Point p1 = new Point(((xlPos1 - 1) * spacer) + x1Offset + (fontSize / 2) + 2, y1Offset + fontSize + 25);
            Point p2 = new Point(((xlPos2 - 1) * spacer) + x1Offset + (fontSize / 2) + 2, y1Offset + fontSize + 25);

            g.DrawLine(p, p1, p2);
        }


        private void IntraLinkerVerticalLine(int xlPos, Graphics g)
        {
            //Draw the Intra Cross Linker Vertical Line
            Pen p = new Pen(Brushes.Black);
            p.Width = 2;

            Point p1 = new Point(((xlPos -1) * spacer) + x1Offset + (fontSize / 2) + 2, y1Offset + fontSize + 5);
            Point p2 = new Point(((xlPos -1) * spacer) + x1Offset + (fontSize / 2) + 2, y1Offset + fontSize + 25);

            g.DrawLine(p, p1, p2);
        }

        private void DrawLeftBreak(int xOffset, int yOffset, Graphics g)
        {
            Pen pen = new Pen(Brushes.Red);
            pen.Width = 2;

            Point p1 = new Point(xOffset, yOffset);
            Point p2 = new Point(xOffset, yOffset + fontSize+ 2);

            g.DrawLine(pen, p1, p2);

            Point p3 = new Point(xOffset - 8, yOffset + 8 + fontSize) ;
            g.DrawLine(pen, p2, p3);

        }

        private void DrawRightBreak(int xOffset, int yOffset, Graphics g)
        {
            Pen pen = new Pen(Brushes.Red);
            pen.Width = 2;

            Point p1 = new Point(xOffset, yOffset);
            Point p2 = new Point(xOffset, yOffset + fontSize +2);

            g.DrawLine(pen, p1, p2);

            //Point p3 = new Point(xOffset +6 , yOffset + fontSize + 8);
            Point p3 = new Point(xOffset + 6, yOffset -6);
            g.DrawLine(pen, p1, p3);

        }


        public List<AnnotationItem> ParseAnnotation(string text)
        {
            List<AnnotationItem> theAnnotations = new List<AnnotationItem>();

            string[] tmp = Regex.Split(text, "\n");

            try
            {
                foreach (string t in tmp)
                {
                    if (t.Length == 0) { continue; }
                    string[] cols = Regex.Split(t, "-");
                    AnnotationItem a = new AnnotationItem(cols[0], int.Parse(cols[1]));
                    theAnnotations.Add(a);
                }
            } catch
            {
                MessageBox.Show("Anotation does not seem to be in a correct format.");
            }

            return theAnnotations;
        }

        private void StartUpTheCanvas(string anotation, out List<AnnotationItem> myAnnotations, out Graphics g, out Font f)
        {
            //Parse the anotation
            myAnnotations = ParseAnnotation(anotation);

            g = pictureBoxAnotator.CreateGraphics();
            g.Clear(Color.White);

            f = new System.Drawing.Font("Courier New", fontSize);
        }

        public class AnnotationItem
        {
            public string Series { get; set; }
            public int IonNo { get; set; }

            public AnnotationItem(string series, int ionNo)
            {
                Series = series;
                IonNo = ionNo;
            }
        }

        private void PeptideAnotator_Load(object sender, EventArgs e)
        {

        }

    }

    
}
