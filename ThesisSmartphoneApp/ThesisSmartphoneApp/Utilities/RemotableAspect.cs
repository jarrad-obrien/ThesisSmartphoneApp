using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody;
using MethodBoundaryAspect.Fody.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ThesisSmartphoneApp.Utilities;

namespace ThesisSmartphoneApp.Utilities
{
	class RemotableAspect : OnMethodBoundaryAspect
	{
		public override void OnExit(MethodExecutionArgs arg)
		{
			base.OnExit(arg);

			// If the app is connected to a server, use it perform the method instead of the app
			if(ConnectedSingleton.Instance.Connected)
			{
				var result = Task.Run(async () => await GetResult(arg));
				result.Wait();
			}

		}

		// Makes a POST call to an API and overwrites the return value of the original method with the
		// result of the API call
		async Task GetResult(MethodExecutionArgs arg)
		{
			using (var c = new HttpClient())
			{
				var client = new HttpClient();

				dynamic jsonRequest = new ExpandoObject();
				var jsonRequestDictionary = (IDictionary<string, object>)jsonRequest;

				// Construct the JSON
				// Add the name of the method to the JSON
				jsonRequestDictionary.Add("methodName", arg.Method.Name);

				// Add the parameter names and values of the method to the JSON
				for (int i = 0; i < arg.Method.GetParameters().Length; i++)
				{
					jsonRequestDictionary.Add(arg.Method.GetParameters()[i].Name, arg.Arguments[i]);
				}

				var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequestDictionary);
				HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

				var response = await client.PostAsync(ConnectedSingleton.Instance.Address, content);

				if (response.IsSuccessStatusCode)
				{
					JObject result = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);

					// Overwrite the return value of the method with the result of the API call
					arg.ReturnValue = (int)result["result"];
				}

				// TODO Handle bad server calls - taking too long, bad response code
			}
		}
	}
}
