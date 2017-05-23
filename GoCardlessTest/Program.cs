using System;
using Vecsoft.GoCardlessApi;

namespace GoCardlessTest
	{
	class Program
		{
		public const String TestKey = ""; // Put your API key here

		public static Context Context { get; private set; }

		[STAThread]
		public static void Main(string[] args)
			{
			Context = new Context(TestKey);
			new Eto.Forms.Application().Run(new MainForm());
			}
		}
	}
