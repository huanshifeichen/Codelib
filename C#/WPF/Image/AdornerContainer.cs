using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Controls;
namespace ImageDemo
{
  public  class AdornerContainer : Adorner
    {
      public AdornerContainer(UIElement adornerElement): base(adornerElement)
      {

      }

      private UIElement child;

      public UIElement Child
      {
          get { return child; }
          set {
              if (child != value)
              {
                  RemoveLogicalChild(child);
                  RemoveVisualChild(child);
                  child = value;
                  AddLogicalChild(child);
                  AddVisualChild(child);
              }
          }
      }


      protected override int VisualChildrenCount
      {
          get
          {
              return this.child == null ? 0 : 1;
          }
      }

      protected override System.Windows.Media.Visual GetVisualChild(int index)
      {
          return this.child;
      }



      protected override Size MeasureOverride(Size constraint)
      {
          if (child !=null)
          {
              child.Measure(constraint);
          }
          return AdornedElement.RenderSize;
      }

      protected override Size ArrangeOverride(Size finalSize)
      {
          child.Arrange(new Rect(finalSize));
          return child.RenderSize;
      }

    }
}
