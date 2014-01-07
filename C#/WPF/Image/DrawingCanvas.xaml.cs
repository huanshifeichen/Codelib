using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Diagnostics;

namespace ImageDemo
{
    /// <summary>
    /// DrawingCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class DrawingCanvas : UserControl
    {
        public DrawingCanvas()
        {
            InitializeComponent();
            rect = new Rectangle();
           // rect.Fill = Brushes.Blue;
            //rect.Stroke = Brushes.Black;
            //rect.StrokeDashArray = new DoubleCollection() { 1,2 };
        }
        private Point originPoint;
        private Point endPoint;
        public Rectangle rect;
        private void LeftButtonDownOnClick(object sender, MouseButtonEventArgs e)
        {
            originPoint = e.GetPosition(canvas);
            canvas.Children.Clear();
           
        }

        private void MouseOnMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                endPoint = e.GetPosition(canvas);
                Rect targetRect = new Rect(originPoint, endPoint);
                rect.Width = targetRect.Width;
                rect.Height = targetRect.Height;
                
                //获取装饰元素作为VisualBrush的来源
                VisualBrush vb = new VisualBrush(DecoratedElement);
                vb.Viewbox = targetRect;
                vb.ViewboxUnits = BrushMappingMode.Absolute;

                rect.Fill = vb;

                Canvas.SetTop(rect, targetRect.Top);
                Canvas.SetLeft(rect, targetRect.Left);
                if (canvas.Children.Count==0)
                {
                    canvas.Children.Add(rect);
                }


               Debug.WriteLine(string.Format("endPoint x:{0},y:{1}", endPoint.X, endPoint.Y));
            }
        }

        public UIElement DecoratedElement { get; set; }

    }
}
