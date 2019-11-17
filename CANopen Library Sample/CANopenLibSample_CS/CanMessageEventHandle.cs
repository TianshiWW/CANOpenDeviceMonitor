
using System;
using System.Collections.Generic;
using System.Text;

namespace CANopenLib
{
	public class CanMessageEventHandle : EventArgs
	{
		public CanMessageEventHandle(CanMessage _CanMessage)
		{
			this.CanMessage = _CanMessage;
		}

		// The fire event will have two pieces of information-- 
		// 1) Where the fire is, and 2) how "ferocious" it is.  

		public CanMessage CanMessage { get; set; }
	}	//end of class FireEventArgs

}