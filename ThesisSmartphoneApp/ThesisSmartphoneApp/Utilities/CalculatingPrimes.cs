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
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThesisSmartphoneApp.Utilities
{
    class CalculatingPrimes : INotifyPropertyChanged
    {

        public int _largestPrime = 0;

        public int _number;

        public long _timer;

        public ICommand CalculateLargestPrimeCommand { get; private set; }

        public bool _canCalculate = true;

        Stopwatch _stopWatch = new Stopwatch();

		public string _address = "http://192.168.0.19:1337/prime";


		public int LargestPrime
        {
            get
            {
                return _largestPrime;
            }
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
        }

        public int Number
        {
            get
            {
                return _number;
            }

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
        }

        public long Timer
        {
            get
            {
                return _timer;
            }

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
        }

		public bool CanCalculate
		{
			get
			{
				return _canCalculate;
			}

			set
			{
				_canCalculate = value;
			}
		}

		public string Address
		{
			get
			{
				return _address;
			}

			set
			{
				_address = value;
			}
		}
		
		public CalculatingPrimes(string location)
        {
            CalculateLargestPrimeCommand = new Command(async () => await CalculateLargestPrimeAsync(location), () => CanCalculate);
        }

        // Handles enabling and disabling the calculate primes button
        void CanCalculatePrime(bool value)
        {
            _canCalculate = value;
            ((Command)CalculateLargestPrimeCommand).ChangeCanExecute();
        }

		// Runs the calculate prime function asynchronously
		async Task CalculateLargestPrimeAsync(string location)
		{
			_stopWatch.Start();
			CanCalculatePrime(false);

			if (location == "Phone")
			{
				await Task.Run(() => LargestPrime = CalculateLargestPrime(Number));
			}

			else if (location == "Local")
			{
				using (var c = new HttpClient())
				{
					var client = new HttpClient();
					var jsonRequest = new { calculateTo = Number };
					var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
					HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
					var response = await client.PostAsync(Address, content);

					if(response.IsSuccessStatusCode)
					{
						JObject result = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
						LargestPrime = (int)result["highestPrime"];
					}

				}
			}

			CanCalculatePrime(true);

			_stopWatch.Stop();
			Timer = _stopWatch.ElapsedMilliseconds;
			_stopWatch.Reset();
		}

		// Finds the highest prime up to and including the specified number
		[TestSkipAspect]
		public int CalculateLargestPrime(int calculateTo)
        {

			int largestPrime = 1;
			bool isPrime;

			for (int i = 1; i <= calculateTo; i++)
			{
				isPrime = true;

				for (int j = 2; j <= i / 2; j++)
				{
					if (i % j == 0)
					{
						isPrime = false;
						break;
					}
				}

				if (isPrime)
				{
					largestPrime = i;
				}
			}

			return largestPrime;

        }

        // Handles updating the view when values are updated
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
