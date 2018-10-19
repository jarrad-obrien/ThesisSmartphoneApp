using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ThesisSmartphoneApp.ViewModels
{
    class CalculatingPrimes : INotifyPropertyChanged
    {

        public int _largestPrime = 0;

        public int _number;

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

        public int Number
        {
            set
            {
                if (_number != value)
                {
                    _number = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged();
                    }
                }
            }

            get
            {
                return _number;
            }
        }


        public Command CalculateLargestPrimeCommand
        {
            get
            {
                return new Command(() => { LargestPrime = CalculateLargestPrime(Number); });
            }
        }

        public int CalculateLargestPrime(int calculateTo)
        {
            int largestPrime = 1;
            bool isPrime;

            for(int i = 1; i <= calculateTo; i++)
            {
                isPrime = true;

                for(int j = 2; j <= i/2; j++)
                {
                    if(i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if(isPrime)
                {
                    largestPrime = i;
                }
            }

            return largestPrime;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
