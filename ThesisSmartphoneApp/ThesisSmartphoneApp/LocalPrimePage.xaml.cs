﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThesisSmartphoneApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocalPrimePage : ContentPage
	{
		public LocalPrimePage ()
		{
			InitializeComponent ();

			BindingContext = new ThesisSmartphoneApp.Utilities.CalculatingPrimes("Local");
		}
	}
}