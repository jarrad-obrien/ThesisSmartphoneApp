using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace ThesisSmartphoneApp.Utilities
{
    class CalculatingPrimes : INotifyPropertyChanged
    {

        public int _largestPrime = 0;

        public int _number;

        public long _timer;

        public ICommand CalculateLargestPrimeCommand { get; private set; }

        //bool canCalculate = true;

        Stopwatch _stopWatch = new Stopwatch();

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

        public long Timer
        {
            set
            {
                if (_timer != value)
                {
                    _timer = value;

                    if (PropertyChanged != null)
                    {
                        OnPropertyChanged();
                    }
                }
            }

            get
            {
                return _timer;
            }
        }

        public bool CanCalculate { get; set; }

        public CalculatingPrimes()
        {
            CalculateLargestPrimeCommand = new Command(async () => await CalculateLargestPrimeAsync());
        }

        async Task CalculateLargestPrimeAsync()
        {
            await Task.Run(() => LargestPrime = CalculateLargestPrime(Number));
        }

        public int CalculateLargestPrime(int calculateTo)
        {
            //string methodName = MethodInfo.GetCurrentMethod().Name;
            //ParameterInfo[] myParams = MethodInfo.GetCurrentMethod().GetParameters();

            //foreach (ParameterInfo p in myParams)
            //{
            //    Console.WriteLine(p.Name);
            //}

            // API CALL

            //canCalculate = false;
            //((Command)CalculateLargestPrimeCommand).ChangeCanExecute();
            _stopWatch.Start();

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

            _stopWatch.Stop();
            Timer = _stopWatch.ElapsedMilliseconds;
            _stopWatch.Reset();

            //((Command)CalculateLargestPrimeCommand).ChangeCanExecute();

            //canCalculate = true;

            return largestPrime;
        }

        //void CanCalculate(bool canCalculate)
        //{
        //    this.canCalculate = canCalculate;
        //    ((Command)CalculateLargestPrimeCommand).ChangeCanExecute();
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
