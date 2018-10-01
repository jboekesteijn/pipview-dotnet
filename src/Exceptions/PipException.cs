using System;
using System.Runtime.Serialization;

namespace PipView.Exceptions
{
	[Serializable]
	public class PipException : Exception
	{
		public PipException() : base() { }
		public PipException(string message) : base(message) { }
		public PipException(string message, System.Exception inner) : base(message, inner) { }
		protected PipException(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}
