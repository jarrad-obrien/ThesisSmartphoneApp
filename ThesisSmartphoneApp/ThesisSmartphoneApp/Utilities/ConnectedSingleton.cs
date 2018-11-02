using System;
using System.Collections.Generic;
using System.Text;

namespace ThesisSmartphoneApp.Utilities
{
	class ConnectedSingleton
	{
		private static ConnectedSingleton instance;

		private bool _connected = false;

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
	}
}
