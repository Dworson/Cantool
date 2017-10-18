using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfControlLibrary.Mode
{
    class UseCMode : NotificationObject
    {

        //MyProperty
        private Double myVar;
        public Double MyProperty
        {
            get { return myVar; }
            set
            {
                myVar = value;
                this.RaisePropertyChanged("MyProperty");
            }
        }

    }
}
