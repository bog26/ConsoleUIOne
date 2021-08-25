using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using IOMethNS;
using System.Text.Json;
//using System.Threading.Tasks;

/*
namespace ShellMenuNS_old
{
	
	public class Menu
	{
		//public IList<FrameItemDisplay> LabelsList {get;set;}
		public IList<string> LabelsList {get;set;}
		public Dictionary<string,int> LabelsDict {get;set;}
		public Dictionary<string,FrameItemDisplay> LabelsDictC {get;set;}
	}
	class jsonMenuTesting 
    {
		//public static string menu1StringJSON;
//https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to?pivots=dotnet-5-0
		//public static void constructJsonMenu()
		public static Menu JsonMenuDes; 
		public static string JsonStringForObjConstr0;
		public static Menu constructJsonMenu(string jsonString)
		{
			//menu1StringJSON = @"{""LabelsList"":[""Label1"",""Label2"",""Label3""]}";
			//Menu menu1 = JsonSerializer.Deserialize<Menu>(menu1StringJSON);
			Menu menu1 = JsonSerializer.Deserialize<Menu>(jsonString);
			return menu1;
		}
		public static void testJsonMenu()
		{
			string menu1StringJSON = @"{""LabelsList"":[""Label1"",""Label2"",""Label3""],
					""LabelsDict"":{""one"":1,""two"":2}}";
			//Menu JsonMenuDes = constructJsonMenu(menu1StringJSON);
			Console.WriteLine($"using string for serialization: \n{menu1StringJSON}");
			JsonMenuDes = constructJsonMenu(menu1StringJSON);
			Console.WriteLine(">>>list:");
			foreach(string label in JsonMenuDes.LabelsList)
			{
				Console.Write(label+"   ");
			}
			//Console.Write(JsonMenuDes.LabelsDict["one"]+" ");
			Console.WriteLine("\n>>>key+value pairs:");
			foreach(KeyValuePair<string,int> pair in JsonMenuDes.LabelsDict)
			{
				Console.Write(pair+"   ");
			}
			int number;
			Console.WriteLine("\n>>>getting value for key \"one\":");
			JsonMenuDes.LabelsDict.TryGetValue("one",out number);
			Console.WriteLine(number);
			Console.WriteLine(">>>keys:");
			foreach(string key in JsonMenuDes.LabelsDict.Keys)
			{
				Console.Write(key + " ");
			}
		}
		public static void JsonSerialisationTest()
		{
			Console.WriteLine("\nSerializing");
			string jsonString = "";
			var options = new JsonSerializerOptions { WriteIndented = true };
			jsonString = JsonSerializer.Serialize(JsonMenuDes,options);
			Console.WriteLine(jsonString);
		}
		public static void testConstructJsonFromObj()
		{
			//var menuOne = new FrameItemDisplay();
			//string jsonString1 = JsonSerializer.Serialize<FrameItemDisplay>(menuOne);
			//Console.WriteLine(jsonString1);

			FrameItemDisplay label1 = new FrameItemDisplay();
			string jsonString1 = JsonSerializer.Serialize<FrameItemDisplay>(label1);
			Console.WriteLine("default constructor:\n"+jsonString1);
			Console.WriteLine("y:"+label1.GridPosition[1]);
			label1.Row = 2;
			Console.WriteLine("y:"+label1.GridPosition[1]);
			jsonString1 = JsonSerializer.Serialize<FrameItemDisplay>(label1);
			Console.WriteLine("default constructor + changes:\n"+jsonString1);
			FrameItemDisplay label2 = new FrameItemDisplay(3,"LabelOne",3,5,false,false,4);
			string jsonString2 = JsonSerializer.Serialize<FrameItemDisplay>(label2);
			Console.WriteLine("constructor with arguments:\n\n"+jsonString2);
			//TBD: write to file
			
			string menu1StringJSONComplex = @"{""LabelsList"":[""Label1"",""Label2"",""Label3""],
					""LabelsDict"":{""one"":1,""two"":2},
					""LabelsDictC"":{""label1"":label1}}";
			//JsonStringForObjConstr1 = jsonString2;

			Console.WriteLine("please insert directory!");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			IOMethodsCLS.DirSet(correctedDirPath);
			string fileName = "JsonStringForObjConstr.json";
			File.WriteAllText(fileName, jsonString2);
			JsonStringForObjConstr0 = File.ReadAllText(fileName);

			//using FileStream createStream = File.Create(fileName);
			//await JsonSerializer.SerializeAsync(createStream, label2);
			//JsonMenuDesComplex = constructJsonMenu(menu1StringJSONComplex);
		}
		public static FrameItemDisplay ConstructFrameItemFromJsonFile(string file)
		{
			string JsonString;
			FrameItemDisplay labelObj;
			JsonString = File.ReadAllText(file);
			labelObj = JsonSerializer.Deserialize<FrameItemDisplay>(JsonString);
			return labelObj;
		}
		public static string WriteStringToFile(string text)
		{
			Console.WriteLine("please insert directory!");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			IOMethodsCLS.DirSet(correctedDirPath);
			Console.WriteLine("please insert Filename!");
			//Ex:"jsonStringFrame2.json";
			string fileName = Console.ReadLine();
			File.WriteAllText(fileName, text);
			return fileName;
		}

		public static void testConstructObjFromJsonFile()
		{
			Console.WriteLine("Constructing <FrameItemDisplay> object");
			Console.WriteLine("JSON File content:\n "+JsonStringForObjConstr0);
			FrameItemDisplay labelObjConstruct;
			labelObjConstruct = JsonSerializer.Deserialize<FrameItemDisplay>(JsonStringForObjConstr0);
			Console.WriteLine("Label text: "+labelObjConstruct.TextDisplay);
			
			FrameItemDisplay labelObjConstruct1, labelObjConstruct2, labelObjConstruct3;
			string file1 = "JsonStringForFrameItemObjConstr1.json";
			string file2 = "JsonStringForFrameItemObjConstr2.json";
			string file3 = "JsonStringForFrameItemObjConstr3.json";
			
			labelObjConstruct1 = ConstructFrameItemFromJsonFile(file1);
			labelObjConstruct2 = ConstructFrameItemFromJsonFile(file2);
			labelObjConstruct3 = ConstructFrameItemFromJsonFile(file3);
			List<FrameItemDisplay> DispLabelList = new List<FrameItemDisplay>{labelObjConstruct1,labelObjConstruct2,labelObjConstruct3};
		
			Console.WriteLine("Label texts: "+labelObjConstruct1.TextDisplay + " "+labelObjConstruct2.TextDisplay+" "+labelObjConstruct3.TextDisplay);

			//constructing whole frame obj now
			FrameDisplay Frame1 = new FrameDisplay(1, 10, 10);
			Console.WriteLine("Frame nr:"+Frame1.FrameNr);
			string jsonStringFrame1 = JsonSerializer.Serialize<FrameDisplay>(Frame1);
			Console.WriteLine("JSON for frame1:"+jsonStringFrame1);

			FrameDisplay Frame2 = new FrameDisplay(2, 10, 10, DispLabelList);
			Console.WriteLine("Frame nr:"+Frame2.FrameNr);
			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonStringFrame2 = JsonSerializer.Serialize<FrameDisplay>(Frame2,options);
			Console.WriteLine("JSON for frame2:"+jsonStringFrame2);

			//write to file: moved to method
			string fileName1 = WriteStringToFile(jsonStringFrame2);
			Console.WriteLine($"new saved file name is: {fileName1}");
			
			//constructing frame obj from file
			string JsonFrameObjString;
			FrameDisplay FrameObj;
			JsonFrameObjString = File.ReadAllText(fileName1);
			FrameObj = JsonSerializer.Deserialize<FrameDisplay>(JsonFrameObjString);
			Console.WriteLine("New file constructed frame object nr:\n" + FrameObj.FrameNr);


		}
	}
		
}
*/
