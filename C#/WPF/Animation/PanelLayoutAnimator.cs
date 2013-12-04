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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace WPF_01
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPF_01"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPF_01;assembly=WPF_01"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:PanelLayoutAnimator/>
    ///
    /// </summary>
    public class PanelLayoutAnimator
    {

        public PanelLayoutAnimator(Panel panelToAnimate)
        {

            _panel = panelToAnimate;

            _panel.LayoutUpdated += new EventHandler(PanelLayoutUpdated);

        }



        void PanelLayoutUpdated(object sender, EventArgs e)
        {

            // At this point, the panel has moved the children to the new locations, but hasn't

            // been rendered

            foreach (UIElement child in _panel.Children)
            {

                // Figure out where child actually is right now. This is a combination of where the

                // panel put it and any render transform currently applied

                Point currentPosition = child.TransformToAncestor(_panel).Transform(new Point());



                // See what transform is being applied

                Transform currentTransform = child.RenderTransform;



                // Compute where the panel actually arranged it to

                Point arrangePosition = currentPosition;

                if (currentTransform != null)
                {

                    // Undo any transform we applied

                    arrangePosition = currentTransform.Inverse.Transform(arrangePosition);

                }



                // If we had previously stored an arrange position, see if it has moved

                if (child.ReadLocalValue(SavedArrangePositionProperty) != DependencyProperty.UnsetValue)
                {

                    Point savedArrangePosition = (Point)child.GetValue(SavedArrangePositionProperty);



                    // If the arrange position hasn't changed, then we've already set up animations, etc

                    // and don't need to do anything

                    if (!AreReallyClose(savedArrangePosition, arrangePosition))
                    {

                        // If we apply the current transform to the saved arrange position, we'll see where

                        // it was last rendered

                        Point lastRenderPosition = currentTransform.Transform(savedArrangePosition);



                        // Transform the child from the new location back to the old position

                        TranslateTransform newTransform = new TranslateTransform();

                        child.RenderTransform = newTransform;



                        // Decay the transformation with an animation

                        newTransform.BeginAnimation(TranslateTransform.XProperty, MakeAnimation(lastRenderPosition.X - arrangePosition.X));

                        newTransform.BeginAnimation(TranslateTransform.YProperty, MakeAnimation(lastRenderPosition.Y - arrangePosition.Y));

                    }

                }



                // Save off the previous arrange position

                child.SetValue(SavedArrangePositionProperty, arrangePosition);

            }

        }



        // Check if two points are really close. If you don't do epsilon comparisons, you can get lost in the

        // noise of floating point operations

        private bool AreReallyClose(Point p1, Point p2)
        {

            return (Math.Abs(p1.X - p2.X) < .001 && Math.Abs(p1.Y - p2.Y) < .001);

        }



        // Create an animation to decay from start to 0 over .5 seconds

        private static DoubleAnimation MakeAnimation(double start)
        {

            DoubleAnimation animation = new DoubleAnimation(start, 0d, new Duration(TimeSpan.FromMilliseconds(500)));

            animation.AccelerationRatio = 0.2;

            return animation;

        }



        // dependency property we attach to children to save their last arrange position

        private static readonly DependencyProperty SavedArrangePositionProperty

           = DependencyProperty.RegisterAttached("SavedArrangePosition", typeof(Point), typeof(PanelLayoutAnimator));



        private Panel _panel;

    }
}
