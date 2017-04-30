using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

namespace PatternTools.ArrowHeads
{
    public static class ArrowDeNovo
    {
        public static void ArrowDeNovoDraw(
            string superiorTag,
            string lowerTag,
            SolidColorBrush myColor,
            Canvas c,
            double startX,
            double endX,
            double locationY,
            ArrowEnds arrowEnds
            )
        {
            ArrowLine al = new ArrowLine();
            al.ArrowAngle = 80;
            al.ArrowEnds = arrowEnds;
            al.Stroke = myColor;
            al.StrokeThickness = 1;
            al.X1 = startX;
            al.X2 = endX;
            al.Y1 = locationY;
            al.Y2 = locationY;
            c.Children.Add(al);
            double width = endX - startX;

            Label superiorLabel = new Label();
            superiorLabel.FontSize = 9;
            superiorLabel.Content = superiorTag;
            superiorLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            
            c.Children.Add(superiorLabel);
            Canvas.SetLeft(superiorLabel, startX + (width / 2) - superiorLabel.DesiredSize.Width / 2);
            Canvas.SetTop(superiorLabel, locationY - superiorLabel.DesiredSize.Height + 5);

            Label inferiorLabel = new Label();
            inferiorLabel.Content = lowerTag;
            inferiorLabel.FontSize = 9;
            inferiorLabel.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            c.Children.Add(inferiorLabel);
            Canvas.SetLeft(inferiorLabel, startX + (width / 2) - inferiorLabel.DesiredSize.Width / 2);
            Canvas.SetTop(inferiorLabel, locationY - 5);

        }
    }
}
