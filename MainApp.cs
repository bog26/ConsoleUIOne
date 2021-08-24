using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	//construct a new App object , which contains a CompleteMenu object
	//	for the main menu.
	//as for app switch methods, each will construct the corresponding app menu:
	//	TimeApp, FilesApp, encryptApp, etc.

	//public static class MainApp
	public static class MainApp
    {
		public const string appName = "Main Menu";
		public static void ConstructMainApp()
		{
			App MainApp = new App(0, appName);
			MainApp.Application.CrtApp = MainApp;
			MainApp.Application.UpdateFrame(AppSwitch);
		}
		public static void AppSwitch(int ItemLink)
		{
			switch (ItemLink)
			{
				case 1:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("constructing TimeApp"+"   ");
					break;
				case 2:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("constructing FilesApp"+"   ");
					break;
				case 3:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("constructing EncryptApp"+"   ");
					break;
				case 4:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("constructing Database"+"   ");
					break;
				default:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("default app");
					break;

			}
		}
    }
	public static class TestMainApp
	{
		
	}
}

