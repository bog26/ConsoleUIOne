using System;
using System.Collections.Generic;
using System.IO;
using IOMethNS;

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
					//Console.SetCursorPosition(0,33);
					//Console.WriteLine("Create data WIP ..."+"   ");
					CreateUserDefinedIntDataSet();
					break;
				case 2:
					Console.SetCursorPosition(0,33); 
					Console.WriteLine("Query1 WIP ..."+"   ");
					break;
				case 3:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("Query2 WIP ..."+"   ");
					break;
				case 4:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("Query3 ..."+"   ");
					break;
				default:
					
					break;
			}
		}
		public static void CreateUserDefinedIntDataSet()
		{
			Console.SetCursorPosition(0,33);
			Console.WriteLine("Dataset creation");
			string dataFile =  IOMethodsCLS.UserDefinedFilePath();
			OrderedIntDataset(dataFile);
			dataFile =  IOMethodsCLS.UserDefinedFilePath();
			QuadrPolynIntDataset(dataFile);
		}
		public static void OrderedIntDataset(string path)
		{
			try
			{
				StreamWriter writer = new StreamWriter(path);
				Console.WriteLine("please insert dataset size");
				int userInput = int.Parse(Console.ReadLine());
				using (writer)
				{
					
					writer.WriteLine("*"+"Read me: this creates a list of ordered integers");
					for(int i=0; i<userInput;i++)
					{
						
						writer.Write(i);
						if(i!=userInput-1)
						{
							writer.Write(",");
						}
						
					}
					
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine($"There was an issue! {e.Message}");
			}
			catch (IOException e)
			{
				Console.WriteLine($"Cannot read file! Details: {e.Message}");
			}
			finally
			{
				Console.WriteLine("finished");
			}
		}

		public static void QuadrPolynIntDataset(string path)
		{
			try
			{
				StreamWriter writer = new StreamWriter(path);
				Console.WriteLine("creating a dataset using quadratic polynom: a*x*x+b*x+c");
				Console.WriteLine("please insert dataset size");
				int datasetSize = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert a");
				int a = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert b");
				int b = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert c");
				int c = int.Parse(Console.ReadLine());

				using (writer)
				{
					
					//writer.WriteLine("*"+"Read me: this creates a dataset using quadratic polynom: a*x*x+b*x+c");
					writer.WriteLine("*"+$"Read me: this dataset is created using the quadratic polynom: {a}*x*x+{b}*x+{c}, where x is between 0 and {datasetSize-1}");
					for(int i=0; i<datasetSize;i++)
					{
						
						writer.Write(a*i*i+b*i+c);
						if(i!=datasetSize-1)
						{
							writer.Write(",");
						}
						
					}
					
				}
			}
			catch (FileNotFoundException e)
			{
				Console.WriteLine($"There was an issue! {e.Message}");
			}
			catch (IOException e)
			{
				Console.WriteLine($"Cannot read file! Details: {e.Message}");
			}
			finally
			{
				Console.WriteLine("finished");
			}
		}





    }
	public static class TestDatabaseApp
	{
		
	}
}

