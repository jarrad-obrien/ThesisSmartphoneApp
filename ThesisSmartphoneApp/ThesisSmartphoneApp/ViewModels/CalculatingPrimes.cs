using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Windows.Input;

namespace ThesisSmartphoneApp.ViewModels
{
    class CalculatingPrimes : INotifyPropertyChanged
    {

        public int _largestPrime = 0;
        public ICommand CalculatePrimesCommand { get; private set; }

        public int LargestPrime
        {
            set
            {
                if(_largestPrime != value)
                {
                    _largestPrime = value;

                    if(PropertyChanged != null)
                    {
                        OnPropertyChanged();
                    }
                }
            }

            get
            {
                return _largestPrime;
            }
        }


        public Command CalculatePrimes
        {
            get
            {
                return new Command(() => { LargestPrime++; });
            }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
