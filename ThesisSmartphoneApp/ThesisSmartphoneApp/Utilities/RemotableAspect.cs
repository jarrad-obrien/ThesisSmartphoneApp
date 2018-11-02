using System;
using System.Collections.Generic;
using System.Text;
using MethodBoundaryAspect.Fody;
using MethodBoundaryAspect.Fody.Attributes;

namespace ThesisSmartphoneApp.Utilities
{
	class RemotableAspect : OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionArgs arg)
		{
			Console.WriteLine("TEST");
			base.OnEntry(arg);
			string methodName = arg.Method.Name;

			//var x = arg.Arguments;
			//for (int i = 0; i < x.Length; i++)
			//{
			//	Console.WriteLine("arguments " + i + ": " + x[i]);
			//}



			var y = arg.Method.GetParameters();
			for(int i = 0; i < y.Length; i++)
			{
				Console.WriteLine("parameter " + i + ": " + arg.Method.GetParameters()[i]);
			}

			//int numberOfArguments = arg.Arguments.GetLength;


			Console.WriteLine(arg.Arguments.ToString());
			Console.WriteLine(arg.Instance);
			Console.WriteLine(arg.Method.GetMethodBody());
			Console.WriteLine(arg.Method.GetParameters());

		}
	}

	class TestSkipAspect: OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionArgs arg)
		{
			base.OnEntry(arg);
			OnExit(arg);
		}

		public override void OnExit(MethodExecutionArgs arg)
		{
			base.OnExit(arg);
			arg.FlowBehavior = FlowBehavior.RethrowException;
			arg.ReturnValue = 22;
			throw new Exception("test");
		}
	}
}
