using System;
using System.Collections.Generic;
using System.Text;

namespace ThesisSmartphoneApp.Utilities
{
	class ConnectedSingleton
	{
		private static ConnectedSingleton instance;

		private bool _connected = false;

		private string _address = "http://192.168.0.19:1337/prime";

		private ConnectedSingleton() { }

		public static ConnectedSingleton Instance
		{
			get
			{
				if (instance == null)
					instance = new ConnectedSingleton();

				return instance;
			}
		}

		public bool Connected
		{
			get
			{
				return _connected;
			}

			set
			{
				_connected = value;
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
	}
}
