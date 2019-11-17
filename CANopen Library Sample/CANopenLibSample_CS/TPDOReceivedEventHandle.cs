
using System;
using System.Collections.Generic;
using System.Text;

namespace CANopenLib
{
	public class TPDOReceivedEventHandle : EventArgs
	{
		public TPDOReceivedEventHandle(PDOMessage _TPDOMessage)
		{
			this.TPDOMessage = _TPDOMessage;
		}

		public PDOMessage TPDOMessage { get; set; }
	}

}