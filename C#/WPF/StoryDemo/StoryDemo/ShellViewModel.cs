using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using StoryDemo.Modules.Storyboard;

namespace StoryDemo
{
   public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
       public ShellViewModel()
       {
           Items.Add(new StoryViewModel());
           
           //Items.Add()
       }

       
    }
}
