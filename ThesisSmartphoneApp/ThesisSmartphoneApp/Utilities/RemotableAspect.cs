﻿using System;
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
			base.OnEntry(arg);

			var result = Task.Run(async () => await GetResult(arg));

			arg.ReturnValue = 12345;
			
		}

		async Task GetResult(MethodExecutionArgs arg)
		{
			using (var c = new HttpClient())
			{
				var client = new HttpClient();

				dynamic jsonRequest = new ExpandoObject();
				var jsonRequestDictionary = (IDictionary<string, object>)jsonRequest;

				jsonRequestDictionary.Add("methodName", arg.Method.Name);

				//jsonRequest.methodName = arg.Method.Name;

				for (int i = 0; i < arg.Method.GetParameters().Length; i++)
				{
					jsonRequestDictionary.Add(arg.Method.GetParameters()[i].Name, arg.Arguments[i]);
					//string parameterName = arg.Method.GetParameters()[i].Name;
					//jsonRequest.parameterName = arg.Arguments[i];
				}

				var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequestDictionary);
				HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");

				var response = await client.PostAsync(ConnectedSingleton.Instance.Address, content);

				if (response.IsSuccessStatusCode)
				{
					JObject result = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
					//LargestPrime = (int)result["highestPrime"];
				}

				// TODO handle on response failure
			}
		}
	}
}
