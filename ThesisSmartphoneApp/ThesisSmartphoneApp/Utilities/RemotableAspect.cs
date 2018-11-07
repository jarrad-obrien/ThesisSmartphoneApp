using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody;
using MethodBoundaryAspect.Fody.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ThesisSmartphoneApp.Utilities
{
	class RemotableAspect : OnMethodBoundaryAspect
	{
		public override void OnExit(MethodExecutionArgs arg)
		{
			Console.WriteLine("TEST");
			base.OnEntry(arg);
			string methodName = arg.Method.Name;


			var y = arg.Method.GetParameters();
			for(int i = 0; i < y.Length; i++)
			{
				Console.WriteLine("parameter " + i + ": " + arg.Method.GetParameters()[i]);
			}

			arg.ReturnValue = 12345;
			

		}

		//async Task GetResult()
		//{
		//	using (var c = new HttpClient())
		//	{
		//		var client = new HttpClient();
		//		var jsonRequest = new { calculateTo = Number };
		//		var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest);
		//		HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
		//		var response = await client.PostAsync(Address, content);

		//		if (response.IsSuccessStatusCode)
		//		{
		//			JObject result = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
		//			LargestPrime = (int)result["highestPrime"];
		//		}

		//	}
		//}
	}
}
