using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	//construct a new App object , which contains a CompleteMenu object
	//	for the main menu.
	//as for app switch methods, each will construct the corresponding app menu:
	//	TimeApp, FilesApp, encryptApp, etc.

	//public static class MainApp
	public static class DatabaseApp
    {
		public const string appName = "Database";
		public static void ConstructDatabaseApp()
		{
			App DatabaseApp = new App(0, appName);
			DatabaseApp.Application.CrtApp = DatabaseApp;
			DatabaseApp.Application.UpdateFrame(AppSwitch);
		}
		public static void AppSwitch(int ItemLink)
		{
			switch (ItemLink)
			{
				case -1:
					MainApp.ConstructMainApp();
					break;
				case 0:
					ConstructDatabaseApp();
					break;

				case 1:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("WIP ..."+"   ");
					break;
				case 2:
					Console.SetCursorPosition(0,33); 
					Console.WriteLine("WIP ..."+"   ");
					break;
				case 3:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("WIP ..."+"   ");
					break;
				case 4:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("WIP ..."+"   ");
					break;
				default:
					
					break;

			}
		}
    }
	public static class TestDatabaseApp
	{
		
	}
}

