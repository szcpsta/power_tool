using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pt5Viewer.Presenters
{
    public abstract class Presenter
    {
        public PresenterManager PresenterManager { protected get; set; }

        //public string Name { get; }
        //public Presenter(string name)
        //{
        //    Name = name;
        //}
    }
}
