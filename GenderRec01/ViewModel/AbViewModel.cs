using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenderRec01.ViewModel
{
    public abstract class AbViewModel : INotifyPropertyChanged, IDisposable
    {
        protected AbViewModel() { }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(String propName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if(handler != null)
            {
                var e = new PropertyChangedEventArgs(propName);
                handler(this, e);
            }
        }

        public void Dispose()
        {
            this.Dispose();
        }

        protected virtual void onDispose()
        {

        }
    }
}
