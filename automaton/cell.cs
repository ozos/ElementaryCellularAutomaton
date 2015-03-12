using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace automaton
{
    public class cell : INotifyPropertyChanged
    {
        public bool IsAlive
        {
            get
            {
                return _isAlive;
            }

            set
            {
                _isAlive = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(
                      this,
                      new PropertyChangedEventArgs("IsAlive")
                    );
                }
            }
        }

        private bool _isAlive = false;

        #region INotifyPropertyChanged Members

        public event
           PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}



