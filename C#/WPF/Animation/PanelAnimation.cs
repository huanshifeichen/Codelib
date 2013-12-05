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

  public  class PanelAnimation
    {
      public PanelAnimation(Panel panelToAnimate)
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

            //  Point currentPosition = child.TransformToAncestor(_panel).Transform(new Point());
             Vector vectorNow =  VisualTreeHelper.GetOffset(child);
             Point currentPosition = new Point(vectorNow.X, vectorNow.Y);

              // See what transform is being applied

              Transform currentTransform = child.RenderTransform;



              // Compute where the panel actually arranged it to

              Point arrangePosition = currentPosition;




              // If we had previously stored an arrange position, see if it has moved

              if (child.GetValue(PreviousRectProperty) != DependencyProperty.UnsetValue)
              {
                  var savedRect = PanelAnimation.GetPreviousRect(child);


                  Point savedArrangePosition = savedRect.TopLeft;



                  // If the arrange position hasn't changed, then we've already set up animations, etc

                  // and don't need to do anything

                  if (!AreReallyClose(savedArrangePosition, arrangePosition))
                  {

                      // If we apply the current transform to the saved arrange position, we'll see where

                      // it was last rendered

                     // Point lastRenderPosition = currentTransform.Transform(savedArrangePosition);

                      Point lastRenderPosition = savedArrangePosition;

                      // Transform the child from the new location back to the old position

                      TranslateTransform newTransform = new TranslateTransform();

                      child.RenderTransform = newTransform;

                      Storyboard transition = new Storyboard();
                      var xAnimation = MakeAnimation(lastRenderPosition.X - arrangePosition.X);
                      var yAnimation = MakeAnimation(lastRenderPosition.Y - arrangePosition.Y);
                      
                      Storyboard.SetTarget(xAnimation, child);
                      Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                     // Storyboard.SetTarget(child, xAnimation);

                      Storyboard.SetTarget(yAnimation, child);
                      Storyboard.SetTargetProperty(yAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
                    //  Storyboard.SetTarget( yAnimation);
                      transition.Children.Add(xAnimation);
                      transition.Children.Add(yAnimation);
                      transition.Duration = TimeSpan.FromMilliseconds(1000);
                      transition.Completed += (s, ev) => {
                       // transition.be
                          transition.Remove();
                      };
                      transition.Begin((FrameworkElement)child,true);
                     
                      //transition.Children.Add()

                      // Decay the transformation with an animation

                    //  newTransform.BeginAnimation(TranslateTransform.XProperty, MakeAnimation(lastRenderPosition.X - arrangePosition.X));

//                      newTransform.BeginAnimation(TranslateTransform.YProperty, MakeAnimation(lastRenderPosition.Y - arrangePosition.Y));

                  }

              }



              // Save off the previous arrange position

            //  child.SetValue(SavedArrangePositionProperty, arrangePosition);
              PanelAnimation.SetPreviousRect(child, new Rect(arrangePosition, child.DesiredSize));

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
          animation.EasingFunction = new BackEase();
          //animation.EasingFunction.
          return animation;

      }

      private Panel _panel;



      public static Rect GetPreviousRect(DependencyObject obj)
      {
          return (Rect)obj.GetValue(PreviousRectProperty);
      }

      public static void SetPreviousRect(DependencyObject obj, Rect value)
      {
          obj.SetValue(PreviousRectProperty, value);
      }

      // Using a DependencyProperty as the backing store for PreviousRect.  This enables animation, styling, binding, etc...
      public static readonly DependencyProperty PreviousRectProperty =
          DependencyProperty.RegisterAttached("PreviousRect", typeof(Rect), typeof(PanelAnimation), new PropertyMetadata(new Rect(0,0,0,0)));


    }
}
