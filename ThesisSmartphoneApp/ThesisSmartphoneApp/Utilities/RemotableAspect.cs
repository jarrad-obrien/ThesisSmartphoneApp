using System;
using System.Collections.Generic;
using System.Text;
using MethodBoundaryAspect.Fody;
using MethodBoundaryAspect.Fody.Attributes;

namespace ThesisSmartphoneApp.Utilities
{
	class RemotableAspect : MethodBoundaryAspect.Fody.Attributes.OnMethodBoundaryAspect
	{
		public override void OnEntry(MethodExecutionArgs arg)
		{
			Console.WriteLine("TEST");
			base.OnEntry(arg);
		}
	}
}
