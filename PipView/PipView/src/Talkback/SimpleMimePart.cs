using System;
using System.Collections.Generic;
using System.Text;

namespace PipView.Talkback
{
	class SimpleMimePart
	{
		private string name;

		public string Name
		{
			get { return name; }
			//set { name = value; }
		}	

		private string fileName;

		public string FileName
		{
			get { return fileName; }
			//set { fileName = value; }
		}

		private string contentType;

		public string ContentType
		{
			get { return contentType; }
			//set { contentType = value; }
		}

		private string data;

		public string Data
		{
			get { return data; }
			//set { data = value; }
		}	

		public SimpleMimePart(string name, string fileName, string contentType, string data)
		{
			this.name = name;
			this.fileName = fileName;
			this.contentType = contentType;
			this.data = data;
		}
	}
}
