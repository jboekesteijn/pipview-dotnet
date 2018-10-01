using System;
using System.Runtime.Serialization;

namespace PipView.Exceptions
{
	[Serializable]
	public class ParserException : Exception
	{
		public ParserException() : base() { }
		public ParserException(string message) : base(message) { }
		public ParserException(string message, System.Exception inner) : base(message, inner) { }
		protected ParserException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
