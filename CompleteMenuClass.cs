﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using IOMethNS;

namespace ShellMenuNS
{
	
	
	class CompleteMenu:IMenu<IFrame<IFrameItem>,IFrameItem>
    {
		private List<IFrame<IFrameItem>> displayFrames;
		private List<IFrame<IFrameItem>> dynamicFrames;
		private IFrame<IFrameItem> crtDisplayFrame;
		
		public CompleteMenu()
		{
			this.crtDisplayFrame = null;
			this.displayFrames = new List<IFrame<IFrameItem>>();
			this.dynamicFrames = new List<IFrame<IFrameItem>>();
		}
		public CompleteMenu(int crtFrameIndex, List<IFrame<IFrameItem>> dispFrames)
		{
			this.displayFrames = new List<IFrame<IFrameItem>>();
			foreach(IFrame<IFrameItem> item in dispFrames)
			{
				this.displayFrames.Add(item);
			}
			this.dynamicFrames = new List<IFrame<IFrameItem>>();
			foreach(IFrame<IFrameItem> item in dispFrames)
			{
				if(item.IsDynamic == true)
				{
					this.dynamicFrames.Add(item);
				}
			}
			this.crtDisplayFrame = this.displayFrames[crtFrameIndex];
		}
		public List<IFrame<IFrameItem>> DisplayFrames
		{
			get{return this.displayFrames;}
			set{this.displayFrames = value;}
		}
		public List<IFrame<IFrameItem>> DynamicFrames
		{
			get{return this.dynamicFrames;}
			set{this.dynamicFrames = value;}
		}
		public IFrame<IFrameItem> CrtDisplayFrame
		{
			get{return this.crtDisplayFrame;}
			set{this.crtDisplayFrame = value;}
		}
		public static List<IFrame<IFrameItem>> ReadFramesFromJsonFile()
		{
			List<IFrame<IFrameItem>> frameList = new List<IFrame<IFrameItem>>();
			return frameList;
		}

		public void ConstructFrameList(List<IFrame<IFrameItem>> dispFrames)
		{
			foreach(IFrame<IFrameItem> item in dispFrames)
			{
				this.displayFrames.Add(item);
			}
		}
		public void AddFrame(IFrame<IFrameItem> frame)
		{
			this.displayFrames.Add(frame);
			//Console.WriteLine("adding frame");
			if(frame.IsDynamic == true)
				{
					this.dynamicFrames.Add(frame);
				}
			this.crtDisplayFrame = this.displayFrames[0];
		}
		public static void ExportCompleteMenuToJsonFile(CompleteMenu menu)
		{

		}
		public void ShowItemFrame()
		{
			consoleDisplay.DisplayItemFrame(this.crtDisplayFrame.CrtDisplayItem.Column,
				this.crtDisplayFrame.CrtDisplayItem.Row,this.crtDisplayFrame.CrtDisplayItem.TextDisplay.Length,
				 "-","|");
		}
		public void DeleteItemFrame()
		{
			consoleDisplay.DisplayItemFrame(this.crtDisplayFrame.CrtDisplayItem.Column,
				this.crtDisplayFrame.CrtDisplayItem.Row,this.crtDisplayFrame.CrtDisplayItem.TextDisplay.Length,
				 " "," ");
		}
		
		public void DisplayCRTFrame()
		{
			Console.Clear();
			consoleDisplay.DisplayLabel(1,1,"FrameNr.: "+this.crtDisplayFrame.FrameNr.ToString());
			ShowItemFrame();
			foreach(KeyValuePair<int,IFrameItem> pair in this.crtDisplayFrame.DisplayItemsDict)
			{
				consoleDisplay.DisplayLabel(pair.Value.Column, pair.Value.Row, pair.Value.TextDisplay);
			}			
			consoleDisplay.DisplayOuterFrame(0,0,12,10,"-","|");
		}

		public void SelectCurrentFrameItem(int crtIndex) //ok
		{
			//debug>>
			Console.SetCursorPosition(0,30);
			Console.WriteLine($"using index: {crtIndex} in crt frame nr: {this.crtDisplayFrame.FrameNr}, with OrderedKeys count: {this.crtDisplayFrame.OrderedKeys.Count} ");
			//<<debug
			this.crtDisplayFrame.CrtDisplayItem =  this.crtDisplayFrame.DisplayItemsDict[this.crtDisplayFrame.OrderedKeys[crtIndex]];	
		}
		public void UpdateFrame()
		{
			DisplayCRTFrame();
			int crtIndex = this.crtDisplayFrame.CursorPosition;
			int length = this.crtDisplayFrame.DisplayItemsDict.Count;
			int index;
			bool itemPressed;
			while(true)
			{
				length = this.crtDisplayFrame.DisplayItemsDict.Count;
				keyRead(crtIndex, length, out index, out itemPressed);
				if(itemPressed)
				{
					index = ActionOnEnter(this.CrtDisplayFrame.CrtDisplayItem, index);
				}
				DeleteItemFrame();
				crtIndex = index; 
				SelectCurrentFrameItem(index); 
				ShowItemFrame();
			}
		}
		public static void keyRead(int crtIndex, int length, out int newIndex, out bool enter)
		{
			ConsoleKeyInfo _Key = Console.ReadKey();
			enter = false;
			switch (_Key.Key)
            	{
                case ConsoleKey.RightArrow:
					newIndex = crtIndex+1 ;
					if(newIndex == length)
					{
						newIndex = 0;
					}
					break;
				case ConsoleKey.LeftArrow:
					newIndex = crtIndex-1;
					if(newIndex <0)
					{
						newIndex = length-1;
					}
					break;	
				case ConsoleKey.Enter:
					newIndex = crtIndex;
					enter = true;
					break;
				default:
					newIndex = crtIndex;
                    break;
				}
		}
		public int ActionOnEnter(IFrameItem FrameItem, int OrderedKeysCrtIndex)
		{
			if(FrameItem.IsActionTrigger)
			{
				ActionTrigger(FrameItem.Link);
				return OrderedKeysCrtIndex;
			}
			else
			{
				int FrameNrToDisplay =this.crtDisplayFrame.CrtDisplayItem.Link;
				foreach(IFrame<IFrameItem> frame in this.displayFrames)
				{
					if(frame.FrameNr == FrameNrToDisplay)
					{
						this.crtDisplayFrame = frame;
						DisplayCRTFrame();
					}
				}
				return 0; //displying new frame => selected display item will be this.crtDisplayFrame.OrderedKeys[0] 
			}
		}
		public void ActionTrigger(int ItemLink)
		{
			switch (ItemLink)
			{
				case 1:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering action 1");
					Actions.ReadMenuTextLines(",", "ItemsDataFile.txt");
					break;
				case 2:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering action 2");
					Console.WriteLine("reading complete file");
					Actions.ReadMenuFile();
					break;
				case 3:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering action 3");
					break;
				case 4:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering action 4");
					Console.WriteLine("Reading lines one by one");
					Actions.ReadMenuFileLines();
					break;
				case 5:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering action 5");
					
					break;
				default:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering default action");
					break;
			}
		}

    }

	class TestCompleteMenu
	{
		/*
		public static void CompleteMenuTesting() //creates complete menu from json, >> issue
		{
			Console.WriteLine("please insert directory!");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			IOMethodsCLS.DirSet(correctedDirPath);
			string fileName1 = "jsonStringFrame1.json";
			string fileName2 = "jsonStringFrame2.json";
			string fileName3 = "jsonStringFrame3.json";
			string fileName4 = "jsonStringFrame4.json";
			string[] frameFiles = new string[]{fileName1,fileName2,fileName3,fileName4};
			string JsonFrameObjString1 = File.ReadAllText(frameFiles[0]);
			string JsonFrameObjString2 = File.ReadAllText(frameFiles[1]);
			string JsonFrameObjString3 = File.ReadAllText(frameFiles[2]);
			string JsonFrameObjString4 = File.ReadAllText(frameFiles[3]);
			string[] jsonStrings = new string[]{JsonFrameObjString1,JsonFrameObjString2,JsonFrameObjString3,JsonFrameObjString4};
			//List<FrameDisplay> framesList = new List<FrameDisplay>();
			List<FrameDisplay> framesList = new List<FrameDisplay>();
			//CompleteMenu menu0 = new CompleteMenu();
			foreach(string json in jsonStrings)
			{
				FrameDisplay FrameObj;
				Console.WriteLine("\n*****showing FrameDisplay json*****\n"+json);
				FrameObj = JsonSerializer.Deserialize<FrameDisplay>(json);
				framesList.Add(FrameObj);
				Console.WriteLine("checking FrameObj");
				Console.WriteLine("frames in framesList: "+framesList.Count);
				//Console.WriteLine("nr of DisplayItems in frame: "+FrameObj.DisplayItems.Count); //not ok ,null
				Console.WriteLine("FrameNr: "+FrameObj.FrameNr);
				//Console.WriteLine(framesList[framesList.Count-1].DisplayItems.Count);
				//menu0.AddFrame(FrameObj);
			}
			CompleteMenu menu0 = new CompleteMenu(0,framesList);
			Console.WriteLine("checking CompleteMenu object");
			Console.WriteLine("Count DisplayFrames "+menu0.DisplayFrames.Count);
			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonStringMenu0 = JsonSerializer.Serialize<CompleteMenu>(menu0,options);
			//string jsonStringMenu0 = JsonSerializer.Serialize<CompleteMenu>(menu0);
			Console.WriteLine("\n***json for menu0:***\n"+jsonStringMenu0);
			string menu0FileName1 = WriteStringToFileTest(jsonStringMenu0);
			Console.WriteLine($"new saved file name is: {menu0FileName1}");
		}
		public static string WriteStringToFileTest(string text)
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
		public static void CompleteMenuTestingObj() // create objects without json
		{

		}

		*/
		public static Dictionary<int,IFrameItem> ConstructItemsDict()
		{
			Console.WriteLine("constructing display items");
			Dictionary<int,IFrameItem> itemsListDict = new Dictionary<int,IFrameItem>();
			FrameItemDisplay item;
			item = new FrameItemDisplay(1,"LabelOne",1,4,false,true,1);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(2,"LabelTwo",4,4,false,false,3);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(3,"LabelThree",8,4,false,false,2);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(4,"LabelFour",1,4,false,false,4);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(5,"LabelFive",8,4,false,true,2);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(6,"LabelSix",4,1,false,true,4);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(7,"LabelSeven",8,1,false,true,5);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(8,"LabelEight",8,8,false,false,1);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(9,"LabelNine",1,8,false,false,3);
			itemsListDict.Add(item.FrameItemNr,item);
			item = new FrameItemDisplay(10,"LabelTen",4,8,false,true,3);
			itemsListDict.Add(item.FrameItemNr,item);
			int crtItem = 3;
			Console.WriteLine($"\n\nItemNr:{itemsListDict[crtItem].FrameItemNr}. Name:{itemsListDict[crtItem].TextDisplay} ");
			//Console.WriteLine($"item object list has {itemsList.Count} items");

			return itemsListDict;
		}
		
		public static List<IFrame<IFrameItem>> ConstructFramesDict()
		{
			Console.WriteLine("constructing frames");
			List<IFrame<IFrameItem>> framesList = new List<IFrame<IFrameItem>>();
			Dictionary<int,IFrameItem> completeItemsDict = new Dictionary<int, IFrameItem>();
			completeItemsDict = ConstructItemsDict();
			Dictionary<int,IFrameItem> itemsDictframe = new Dictionary<int, IFrameItem>();
			
			FrameDisplay frame1;
			int crtItemKey=1;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=2;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=3;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			frame1 = new FrameDisplay(1,10,10,itemsDictframe,1);
			itemsDictframe.Clear();
			framesList.Add(frame1);

			FrameDisplay frame2;
			crtItemKey=2;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=4;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=5;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=8;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			frame2 = new FrameDisplay(2,10,10,itemsDictframe,4);
			itemsDictframe.Clear();
			framesList.Add(frame2);

			FrameDisplay frame3;
			crtItemKey=1;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=6;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=7;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=8;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			frame3 = new FrameDisplay(3,10,10,itemsDictframe,1);
			itemsDictframe.Clear();
			framesList.Add(frame3);

			FrameDisplay frame4;
			crtItemKey=1;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=8;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=9;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			crtItemKey=10;
			itemsDictframe.Add(crtItemKey,completeItemsDict[crtItemKey]);
			frame4 = new FrameDisplay(4,10,10,itemsDictframe,1);
			itemsDictframe.Clear();
			framesList.Add(frame4);
			//FrameDisplay frame = new FrameDisplay(1,10,10,itemsDict);

			return framesList;
		}

		
		
		public static void ConstructMenuDict()
		{
			Console.WriteLine("Constructing menu object");
			List<IFrame<IFrameItem>> framesList;
			framesList = ConstructFramesDict();
			Console.WriteLine("Running tests:");
			Console.WriteLine($"framesList has {framesList.Count} frames");
			Console.WriteLine($"{framesList[1].DisplayItemsDict[5].Row}");
			Console.WriteLine($"{framesList[0].DisplayItemsDict[2].IsActionTrigger}");
			CompleteMenu menu = new CompleteMenu(0,framesList);
			Console.WriteLine($"\nRunning tests for complete menu, current frame: {menu.CrtDisplayFrame.FrameNr}");
			Console.WriteLine($"menu has {menu.DisplayFrames.Count} frames");
			Console.WriteLine($"{menu.DisplayFrames[1].DisplayItemsDict[5].Row}");
			Console.WriteLine($"{menu.DisplayFrames[0].DisplayItemsDict[2].IsActionTrigger}");
			Console.WriteLine($"current frame: {menu.CrtDisplayFrame.FrameNr} ");
			Console.WriteLine("List of ordered keys:");
			foreach(int key in menu.CrtDisplayFrame.OrderedKeys) 
			{
				Console.Write(key + " ");
			}
			Console.WriteLine($"crtDisplayItem: {menu.CrtDisplayFrame.CrtDisplayItem.TextDisplay}");

			//menu.DisplayCRTFrame(); //ok
			menu.UpdateFrame(); // ok
		}
		

	}
}

