using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
namespace GestureDemo
{
  public   static  class VisualTreeExtension
  {
      #region wptoolkit中的方法
      /// <summary>
      ///  返回元素的所有子元素
      /// </summary>
      /// <param name="parent"></param>
      /// <returns></returns>
      public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
      {
          Debug.Assert(parent!= null,"Thre parent cannot be null");


          int childCount = VisualTreeHelper.GetChildrenCount(parent);
          for (int i = 0; i < childCount; i++)
          {
              yield return VisualTreeHelper.GetChild(parent, i);
          }
          
      }
      /// <summary>
      ///获取一个FrameworkElement的所有逻辑后代，使用广度优先搜索。一个visual元素要是另一个visual元素的逻辑子孩子，则他们有有相同的
      /// 命名范围。为了性能原因，这个方法使用的是队列而不是递归。
      /// </summary>
      /// <param name="parent"></param>
      /// <returns></returns>
      public static IEnumerable<FrameworkElement> GetLogicalChildrenBreadthFirst(this FrameworkElement parent)
      {
          Debug.Assert(parent != null, "Thre parent cannot be null");
          
          Queue<FrameworkElement> queue = new Queue<FrameworkElement>(parent.GetVisualChildren().OfType<FrameworkElement>());

          while (queue.Count>0)
          {

              FrameworkElement element = queue.Dequeue();
              yield return element;
              foreach (FrameworkElement visualChild in element.GetVisualChildren().OfType<FrameworkElement>())
              {
                  queue.Enqueue(visualChild);
              }
          }
      }


      /// <summary>
      /// Gets the visual parent of the element.
      /// </summary>
      /// <param name="node">The element to check.</param>
      /// <returns>The visual parent.</returns>
      internal static FrameworkElement GetVisualParent(this FrameworkElement node)
      {
          return VisualTreeHelper.GetParent(node) as FrameworkElement;
      }

      /// <summary>
      /// 获取到visual树根的所有祖先元素
      /// </summary>
      /// <param name="node"></param>
      /// <returns></returns>
      public static IEnumerable<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
      {
          FrameworkElement parent = node.GetVisualParent();
          while (parent != null)
          {
              yield return parent;
              parent = parent.GetVisualParent();
          }
      }

      /// <summary>
      /// 获取特定类型的第一个父元素
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="element"></param>
      /// <returns></returns>
      public static T GetParentByType<T>(this DependencyObject element) 
          where  T:FrameworkElement
      {

          Debug.Assert(element != null, "The element cannot be null.");

          T result = null;

          DependencyObject parent = VisualTreeHelper.GetParent(element);

          while (parent != null)
          {
              result = parent as T;

              if (result != null)
              {
                  return result;
              }
              parent = VisualTreeHelper.GetParent(parent);


          }

          return null;

      }


      #endregion

    #region 自己添的方法
      /// <summary>
      /// 获取特定类型的子元素集合
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="element"></param>
      /// <returns></returns>
      public static IEnumerable<T> GetChildrenByType<T>(this DependencyObject element) where T : FrameworkElement
      {
          return element.GetVisualChildren().OfType<T>();
      }
    #endregion
  }
}
