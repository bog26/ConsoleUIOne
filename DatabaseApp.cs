using System;
using System.Collections.Generic;
using System.IO;
using IOMethNS;
using System.Linq;

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
					Query1();
					break;
				case 3:
					Query2();
					break;
				case 4:
					Console.SetCursorPosition(0,33);
					Console.WriteLine("Query3 ..."+"   ");
					break;
				case 6:
					Console.SetCursorPosition(0,33);
					Sort1();
					break;
				case 9:
					Console.SetCursorPosition(0,33);
					Group1();
					break;	
				case 11:
					Console.SetCursorPosition(0,33);
					Join1();
					break;
				case 12:
					Console.SetCursorPosition(0,33);
					Nest1();
					break;
				case 13:
					//DBObjConstruction ProductsAndCategoryDB = new DBObjConstruction();
					break;
				case 14:
					Console.SetCursorPosition(0,33);
					PerformanceTest1();
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
			//OrderedIntDataset(dataFile);
			//dataFile =  IOMethodsCLS.UserDefinedFilePath();
			//QuadrPolynIntDataset(dataFile);
			MultilineQuadrPolynIntDataset(dataFile);
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
		public static void MultilineQuadrPolynIntDataset(string path)
		{
			try
			{
				StreamWriter writer = new StreamWriter(path);
				Console.WriteLine("creating a dataset using quadratic polynom: a*x*x+b*x+c");
				Console.WriteLine("please insert dataset size");
				int datasetSize = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert start value for a");
				int a = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert start value for b");
				int b = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert start value for c");
				int c = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert increment");
				int increment = int.Parse(Console.ReadLine());
				Console.WriteLine("please insert nr of sets to be generated");
				int setsNr = int.Parse(Console.ReadLine());

				using (writer)
				{
					writer.WriteLine("*"+$"Read me: each dataset is created using the quadratic polynom: {a}*x*x+{b}*x+{c}, where x is between 0 and {datasetSize-1}. Nr of datasets: {setsNr}. Increment:{increment}");
					
					for(int i=0; i<setsNr;i++)
					{
						for(int j=0; j<datasetSize;j++)
						{
							writer.Write(a*j*j+b*j+c);
							if(j!=datasetSize-1)
							{
								writer.Write(",");
							}
						}
						writer.WriteLine();	
						a+=increment; 	
						b+=increment; 
						c+=increment;
						//a+=increment*i; 	
						//b+=increment*i; 
						//c+=increment*i;	

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
		public static void Query1()
		{
			Console.SetCursorPosition(0,50);
			Console.WriteLine("Reading data");
			string readDatasetFileName =  IOMethodsCLS.UserDefinedFilePath();
			Console.WriteLine($"reading file:\n{readDatasetFileName}\n");
			List<int[]> readDataset = ParseDataset(readDatasetFileName);
			Console.WriteLine("Query1: selecting integers divisible with user-provided int divisor");
			Console.WriteLine("please insert divisor");
			int divisor = int.Parse(Console.ReadLine());
			foreach(int[] dataset in readDataset)
			{
				var multipleNr =
					from num in dataset
					where num % divisor == 0
					select num;
				foreach(var item in multipleNr)
				{
					Console.Write(item + " ");
				}
				Console.WriteLine();
			}
		}
		public static void Query2()
		{
			Console.SetCursorPosition(0,50);
			Console.WriteLine("Reading data");
			string readDatasetFileName =  IOMethodsCLS.UserDefinedFilePath();
			Console.WriteLine($"reading file:\n{readDatasetFileName}\n");
			List<int[]> readDataset = ParseDataset(readDatasetFileName);
			Console.WriteLine("Query2: selecting integers inside an user-provided int inteval");
			Console.WriteLine("please insert minimum value");
			int minVal = int.Parse(Console.ReadLine());
			Console.WriteLine("please insert maximum value");
			int maxVal = int.Parse(Console.ReadLine());
			foreach(int[] dataset in readDataset)
			{
				var inRangeNr =
					from num in dataset
					where (num > minVal) & (num < maxVal)
					select num;
				foreach(var item in inRangeNr)
				{
					Console.Write(item + " ");
				}
				Console.WriteLine();
			}
		}

		public static void Sort1()
		{
			Console.SetCursorPosition(0,50);
			Console.WriteLine("Reading data");
			string readDatasetFileName =  IOMethodsCLS.UserDefinedFilePath();
			Console.WriteLine($"reading file:\n{readDatasetFileName}\n");
			List<int[]> readDataset = ParseDataset(readDatasetFileName);
			foreach(int[] dataset in readDataset)
			{
				var sorted =
				from item in dataset 
				orderby item ascending
				select item;
				foreach(var item in sorted)
				{
					Console.Write(item+" ");
				}
				Console.WriteLine();
			}
		}
		public static void Group1()
		{
			Console.SetCursorPosition(0,50);
			Console.WriteLine("Reading data");
			string readDatasetFileName =  IOMethodsCLS.UserDefinedFilePath();
			Console.WriteLine($"reading file:\n{readDatasetFileName}\n");
			List<int[]> readDataset = ParseDataset(readDatasetFileName);
			Console.WriteLine("please insert divisor");
			int divisor = int.Parse(Console.ReadLine());
			foreach(int[] dataset in readDataset)
			{
				var numberGroups =
				from item in dataset
				group item by (item % divisor == 0) into groupedElements 
				select new {Divisible = groupedElements.Key, Items = groupedElements};
				foreach(var groupedElements in numberGroups)
				{
					Console.WriteLine($"Numbers divisibility by {divisor} {groupedElements.Divisible}");
					foreach(var item in groupedElements.Items)
					{
						Console.WriteLine(item);
					}
				}
				Console.WriteLine("\nNext Dataset:");
			}
		}

		public static void Join1()
		{
			//move to a create dataset method; 
			DBObjConstruction ProductsAndCategoryDB = new DBObjConstruction();
			ProductsAndCategoryDB.Products.Add(new DBProduct(){Name="Cherry",CategoryID =1});
			
			var productsWithCategories =
				from product in ProductsAndCategoryDB.Products
				join category in ProductsAndCategoryDB.Categories
					on product.CategoryID equals category.ID
				select new {Name = product.Name, Category = category.Name};
			foreach(var item in productsWithCategories)
			{
				Console.WriteLine(item);
			}
		}

		public static void Nest1()
		{
			Console.SetCursorPosition(0,50);
			Console.WriteLine("nesting");
			DBObjConstruction ProductsAndCategoryDB = new DBObjConstruction();
			var productsWithCategories =
				from product in ProductsAndCategoryDB.Products
				select new {
					Name = product.Name,
					Category =
						(from category in ProductsAndCategoryDB.Categories
						where category.ID == product.CategoryID
						select category.Name).First()
				};
			foreach(var item in productsWithCategories)
			{
				Console.WriteLine(item);
			}
		}
		public static void PerformanceTest1()
			{
				Console.SetCursorPosition(0,33);
				int nrOfAddedElements = 50000000;
				Console.WriteLine($"Performance tests. Adding {nrOfAddedElements} elements");
				
				List<int> list1 = new List<int>();
				DateTime startTime = DateTime.Now;
				list1.AddRange(Enumerable.Range(1,nrOfAddedElements));
				Console.WriteLine($"Duration using extension method: {DateTime.Now-startTime}");

				List<int> list2 = new List<int>();
				startTime = DateTime.Now;
				for(int i=0; i<nrOfAddedElements; i++)
				{
					list2.Add(i);
				}
				Console.WriteLine($"Duration using for loop: {DateTime.Now-startTime}");

				Console.WriteLine("LINQ performance");
				List<int> list3 = new List<int>();
				list3.AddRange(Enumerable.Range(1,nrOfAddedElements));
				startTime = DateTime.Now;
				for(int i=0; i<1000; i++)
				{
					var elements = list3.Where(e => e>2000);
				}
				Console.WriteLine($"Duration with no execution: {DateTime.Now-startTime}");

				startTime = DateTime.Now;
				for(int i=0; i<1000; i++)
				{
					var elements = list3.Where(e => e>2000).First();
					Console.Write(elements+ " ");
				}
				Console.WriteLine($"Duration with execution: {DateTime.Now-startTime}");


			}
		
		public static List<int[]> ParseDataset(string fileName)
		{
			List<string[]> readDataString =new List<string[]>();
			List<int[]> convertedData =new List<int[]>();

			readDataString = Actions.ReadMenuTextLines(",",fileName);
			convertedData = ConvertToIntArrList(readDataString);
			return convertedData;
		}

		public static List<int[]> ConvertToIntArrList(List<string[]> inputData)
		{
			List<int[]> outputData =new List<int[]>();

			for(int i=0; i<inputData.Count; i++)
			{
				int[] crtOutputArr = new int[inputData[i].Length]; 
				for(int j=0; j<crtOutputArr.Length; j++)
				{
					int.TryParse(inputData[i][j], out crtOutputArr[j]);
				}
				outputData.Add(crtOutputArr);
			}

			return outputData;
		} 




    }
	public static class TestDatabaseApp
	{
		
	}
}

