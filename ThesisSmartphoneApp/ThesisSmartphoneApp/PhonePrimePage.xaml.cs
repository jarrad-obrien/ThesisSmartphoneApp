using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ThesisSmartphoneApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PhonePrimePage : ContentPage
	{
		public PhonePrimePage ()
		{
			InitializeComponent ();

			BindingContext = new ThesisSmartphoneApp.Utilities.CalculatingPrimes("Phone");
		}
	}
}