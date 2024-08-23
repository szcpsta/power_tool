using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Pt5Viewer.Models;
using Pt5Viewer.Views;

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

        public virtual void Clear()
        {

        }

        public virtual void Restart()
        {

        }
        public virtual void ModelClosing()
        {

        }

        public virtual void ModelCreated(Pt5Model pt5Model)
        {

        }

        public virtual void ModelStarted()
        {

        }
    }
}
