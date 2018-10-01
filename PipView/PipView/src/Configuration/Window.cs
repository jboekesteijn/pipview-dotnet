namespace PipView.Configuration
{
	public class Window
	{
		private int left;

		public int Left
		{
			get { return left; }
			set { left = value; }
		}

		private int top;

		public int Top
		{
			get { return top; }
			set { top = value; }
		}

		public Window()
		{
			left = 10;
			top = 10;
		}
	}
}
