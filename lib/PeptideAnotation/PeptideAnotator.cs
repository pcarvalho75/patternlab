/**
 * Program:     PeptideAnnotator
 * Author:      Diogo Borges Lima and Paulo Costa Carvalho
 * Update:      8/16/2014
 * Update by:   Diogo Borges Lima
 * Description: Peptide Anotator class
 */
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
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace PeptideAnotation
{

    public partial class PeptideAnotator : UserControl
    {
        /// <summary>
        /// Constants
        /// </summary>
        private const int SPACER = 20;
        private const int FONTSIZE = 19;
        private const int X1OFFSET = 10;
        private const int Y1OFFSET = 10;

        /// <summary>
        /// Local variables
        /// </summary>
        private Graphics graphicsPeptAnotator;
        private Bitmap imageBuffer;
        private string Peptide1 { get; set; }
        private string Peptide2 { get; set; }
        private string AnotationAlfa { get; set; }
        private string AnotationBeta { get; set; }
        private int XlPos1 { get; set; }
        private int XlPos2 { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PeptideAnotator()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// Method responsible for drawing intra link peptide
        /// </summary>
        /// <param name="peptide"></param>
        /// <param name="anotation"></param>
        /// <param name="xlPos1"></param>
        /// <param name="xlPos2"></param>
        public void DrawIntraLink(string peptide, string anotation, int xlPos1, int xlPos2)
        {
            #region Setting Values
            Peptide1 = peptide;
            Peptide2 = null; //Force to call DrawIntraLink Method when 'paint' method is called
            AnotationAlfa = anotation;
            XlPos1 = xlPos1;
            XlPos2 = xlPos2;
            #endregion

            //Getting Series b and y
            List<AnnotationItem> myAnnotations = ParseAnnotation(anotation);
            //Setting Font
            Font f = new System.Drawing.Font("Courier New", FONTSIZE);

            #region Writing peptide

            imageBuffer = new Bitmap(panelAnotator.Width, panelAnotator.Height);
            graphicsPeptAnotator = Graphics.FromImage(imageBuffer);
            graphicsPeptAnotator.Clear(Color.White);

            double xLastPosPept = 0.0;
            double yLastPosPept = 0.0;

            using (graphicsPeptAnotator)
            {
                graphicsPeptAnotator.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                //Draw the AAs and ion annotation
                for (int i = 0; i < peptide.Length; i++)
                {
                    graphicsPeptAnotator.DrawString(peptide[i].ToString(), f, Brushes.Green, new Point((i * SPACER) + X1OFFSET, Y1OFFSET));

                    //Write the b series
                    if (myAnnotations.Exists(a => a.Series.Equals("b") && a.IonNo == i))
                    {
                        DrawLeftBreak((i * SPACER) + X1OFFSET + 2, 4 + Y1OFFSET);
                    }

                    //Write the y series
                    if (myAnnotations.Exists(a => a.Series.Equals("y") && a.IonNo == i))
                    {
                        DrawRightBreak(((peptide.Length - i) * SPACER) + X1OFFSET + 2, 4 + Y1OFFSET);
                    }
                }
                xLastPosPept = ((peptide.Length - 1) * SPACER) + X1OFFSET;
                DrawIntraLinker(xlPos1, xlPos2);
                yLastPosPept = Y1OFFSET + FONTSIZE + 30;
            }

            int newWidthRect = (int)(xLastPosPept);
            Rectangle cropRect = new Rectangle(0, 0, newWidthRect + FONTSIZE, (int)yLastPosPept);
            imageBuffer = this.CropImage(imageBuffer, cropRect);
            panelAnotator.BackgroundImage = imageBuffer;

            #endregion
        }

        /// <summary>
        /// Method responsible for drawing inter link peptide
        /// </summary>
        /// <param name="peptide1"></param>
        /// <param name="peptide2"></param>
        /// <param name="anotationAlfa"></param>
        /// <param name="anotationBeta"></param>
        /// <param name="xlPos1"></param>
        /// <param name="xlPos2"></param>
        public void DrawInterLink(string peptide1, string peptide2, string anotationAlfa, string anotationBeta, int xlPos1, int xlPos2)
        {
            #region Setting Values
            Peptide1 = peptide1;
            Peptide2 = peptide2;
            AnotationAlfa = anotationAlfa;
            AnotationBeta = anotationBeta;
            XlPos1 = xlPos1;
            XlPos2 = xlPos2;
            #endregion

            imageBuffer = new Bitmap(panelAnotator.Width, panelAnotator.Height);
            graphicsPeptAnotator = Graphics.FromImage(imageBuffer);
            graphicsPeptAnotator.Clear(Color.White);

            double xLastPosPept1 = 0.0;
            double xLastPosPept2 = 0.0;
            double yLastPosPept1 = 0.0;
            double yLastPosPept2 = 0.0;

            //Getting Series b and y
            List<AnnotationItem> myAnnotationsAlfa = ParseAnnotation(anotationAlfa);
            List<AnnotationItem> myAnnotationsBeta = ParseAnnotation(anotationBeta);
            //Setting font
            Font f = new System.Drawing.Font("Courier New", FONTSIZE);

            #region Calculating offset
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
            #endregion

            #region Writing peptides

            using (graphicsPeptAnotator)
            {
                graphicsPeptAnotator.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                //Draw Peptide 1
                for (int i = 0; i < peptide1.Length; i++)
                {
                    graphicsPeptAnotator.DrawString(peptide1[i].ToString(), f, Brushes.Green, new Point((i * SPACER) + X1OFFSET, Y1OFFSET));

                    //Write the b series
                    int increment = 0;
                    if (xlPos2 - DeltaXLOffset > xlPos1)
                    {
                        increment = Math.Abs(DeltaXLOffset);
                    }

                    if (myAnnotationsAlfa.Exists(a => a.Series.Equals("b") && a.IonNo == i - increment))
                    {
                        DrawLeftBreak((i * SPACER) + X1OFFSET + 2, 4 + Y1OFFSET);
                    }

                    //Write the y series
                    if (myAnnotationsAlfa.Exists(a => a.Series.Equals("y") && a.IonNo == i - increment))
                    {
                        DrawRightBreak(((peptide1.Length - (i - increment)) * SPACER) + X1OFFSET + 2, 4 + Y1OFFSET);
                    }
                }
                xLastPosPept1 = ((peptide1.Length - 1) * SPACER) + X1OFFSET;
                yLastPosPept1 = Y1OFFSET + FONTSIZE + 4;

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
                        DrawLeftBreak((i * SPACER) + X1OFFSET + 2, Y1OFFSET + (3 * FONTSIZE) - 2);
                    }

                    //Write the y series
                    if (myAnnotationsBeta.Exists(a => a.Series.Equals("y") && a.IonNo == i - increment))
                    {
                        DrawRightBreak(((peptide2.Length - (i - increment)) * SPACER) + X1OFFSET + 2, Y1OFFSET + (3 * FONTSIZE) - 2);
                    }

                    graphicsPeptAnotator.DrawString(peptide2[i].ToString(), f, Brushes.Green, new Point((i * SPACER) + X1OFFSET, Y1OFFSET + (3 * FONTSIZE) - 5));
                }
                xLastPosPept2 = ((peptide2.Length - 1) * SPACER) + X1OFFSET;
                yLastPosPept2 = Y1OFFSET + FONTSIZE + 4;

                //Draw the Interlinker
                Pen p = new Pen(Brushes.Black);
                p.Width = 3;
                Point p1 = new Point(((xlPos1 - 1) * SPACER) + X1OFFSET + (FONTSIZE / 2) + 2, Y1OFFSET + FONTSIZE + 5);
                Point p2 = new Point(((xlPos1 - 1) * SPACER) + X1OFFSET + (FONTSIZE / 2) + 2, Y1OFFSET + FONTSIZE + (2 * FONTSIZE));
                graphicsPeptAnotator.DrawLine(p, p1, p2);
            }
            #endregion

            int newHeightRect = (int)(yLastPosPept1 + yLastPosPept2 + (2 * FONTSIZE));
            int newWidthRect = (int)(xLastPosPept1 > xLastPosPept2 ? xLastPosPept1 : xLastPosPept2);
            Rectangle cropRect = new Rectangle(0, 0, newWidthRect + FONTSIZE, newHeightRect);
            imageBuffer = this.CropImage(imageBuffer, cropRect);
            panelAnotator.BackgroundImage = imageBuffer;

        }

        private Bitmap CropImage(Bitmap bmpImage, Rectangle cropArea)
        {
            Bitmap src = bmpImage;
            Bitmap target = new Bitmap(cropArea.Width, cropArea.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropArea,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            if (imageBuffer != null)
            {
                var rc = new Rectangle(this.ClientSize.Width - panelAnotator.BackgroundImage.Width,
                    this.ClientSize.Height - panelAnotator.BackgroundImage.Height,
                    panelAnotator.BackgroundImage.Width, panelAnotator.BackgroundImage.Height);

                panelAnotator.BackgroundImageLayout = ImageLayout.Center;
            }
        }

        /// <summary>
        /// Method responsible for drawing intra link connector 
        /// </summary>
        /// <param name="xlPos1"></param>
        /// <param name="xlPos2"></param>
        private void DrawIntraLinker(int xlPos1, int xlPos2)
        {
            IntraLinkerVerticalLine(xlPos1);
            IntraLinkerVerticalLine(xlPos2);

            Pen p = new Pen(Brushes.Black);
            p.Width = 2;

            Point p1 = new Point(((xlPos1 - 1) * SPACER) + X1OFFSET + (FONTSIZE / 2) + 2, Y1OFFSET + FONTSIZE + 25);
            Point p2 = new Point(((xlPos2 - 1) * SPACER) + X1OFFSET + (FONTSIZE / 2) + 2, Y1OFFSET + FONTSIZE + 25);

            graphicsPeptAnotator.DrawLine(p, p1, p2);
        }

        /// <summary>
        /// Method responsible for drawing inter link connector
        /// </summary>
        /// <param name="xlPos"></param>
        private void IntraLinkerVerticalLine(int xlPos)
        {
            //Draw the Intra Cross Linker Vertical Line
            Pen p = new Pen(Brushes.Black);
            p.Width = 2;

            Point p1 = new Point(((xlPos - 1) * SPACER) + X1OFFSET + (FONTSIZE / 2) + 2, Y1OFFSET + FONTSIZE + 5);
            Point p2 = new Point(((xlPos - 1) * SPACER) + X1OFFSET + (FONTSIZE / 2) + 2, Y1OFFSET + FONTSIZE + 25);

            graphicsPeptAnotator.DrawLine(p, p1, p2);
        }

        /// <summary>
        /// Method responsible for drawing the presence of 'b' fragment ion
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        private void DrawLeftBreak(int xOffset, int yOffset)
        {
            Pen pen = new Pen(Brushes.Red);
            pen.Width = 2;

            Point p1 = new Point(xOffset, yOffset);
            Point p2 = new Point(xOffset, yOffset + FONTSIZE + 2);
            graphicsPeptAnotator.DrawLine(pen, p1, p2);

            Point p3 = new Point(xOffset - 8, yOffset + 8 + FONTSIZE);
            graphicsPeptAnotator.DrawLine(pen, p2, p3);

        }

        /// <summary>
        /// Method responsible for drawing the presence of 'y' fragment ion
        /// </summary>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        private void DrawRightBreak(int xOffset, int yOffset)
        {
            Pen pen = new Pen(Brushes.Red);
            pen.Width = 2;

            Point p1 = new Point(xOffset, yOffset);
            Point p2 = new Point(xOffset, yOffset + FONTSIZE + 2);
            graphicsPeptAnotator.DrawLine(pen, p1, p2);

            Point p3 = new Point(xOffset + 6, yOffset - 6);
            graphicsPeptAnotator.DrawLine(pen, p1, p3);

        }

        /// <summary>
        /// Method responsible for reading predictedIons
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
            }
            catch
            {
                Console.WriteLine("ERROR: Anotation does not seem to be in a correct format.");
            }

            return theAnnotations;
        }

        /// <summary>
        /// Class responsible for translating predicted Ions
        /// </summary>
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

        /// <summary>
        /// Method responsible for calling Paint Method
        /// </summary>
        public void RefreshPeptideAnotator()
        {
            panelAnotator.Refresh();//Call Paint method
        }

        /// <summary>
        /// Method responsible for painting the panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelAnotator_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            if (!String.IsNullOrEmpty(Peptide1))
            {
                if (!String.IsNullOrEmpty(Peptide2))
                    this.DrawInterLink(Peptide1, Peptide2, AnotationAlfa, AnotationBeta, XlPos1, XlPos2);
                else
                    this.DrawIntraLink(Peptide1, AnotationAlfa, XlPos1, XlPos2);
            }
        }

        /// <summary>
        /// Method responsible for copying image to Clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(imageBuffer);
            return;

            MemoryStream memoryStream = new MemoryStream();
            Graphics mainGraphics = Graphics.FromImage(imageBuffer);
            imageBuffer.Save(memoryStream, ImageFormat.Png);

            IntPtr ipHdc = mainGraphics.GetHdc();
            Metafile mf = new Metafile(memoryStream, ipHdc);
            mainGraphics = Graphics.FromImage(mf);
            mainGraphics.FillEllipse(Brushes.Gray, 0, 0, 100, 100);
            mainGraphics.Dispose();
            mf.Save(memoryStream, ImageFormat.Png);
            IDataObject dataObject = new DataObject();
            dataObject.SetData("PNG", false, memoryStream);
            System.Windows.Forms.Clipboard.SetDataObject(dataObject, true);
        }

        /// <summary>
        /// Method responsible for save image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "PNG graphics (*.png)|*.png";

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                imageBuffer.Save(saveFileDialog1.FileName, ImageFormat.Png);
                MessageBox.Show("Peptide annotation saved successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
