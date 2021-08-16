using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using IOMethNS;

namespace ShellMenuNS
{
	
	
	public class CompleteMenu:IMenu<IFrame<IFrameItem>,IFrameItem>
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
					//Actions.ReadMenuTextLines(",", "ItemsDataFile.txt");
					//delegate here:
					Actions.ReadMenuTextLinesDeleg(",", "ItemsDataFile.txt",ShowFileContent);
					break;
				case 2:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("triggering action 2");
					Console.WriteLine("reading complete file");
					Actions.ReadMenuFile();
					break;
				case 3:
					Console.SetCursorPosition(0,31);
					Console.WriteLine("Delegates TBD");
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

		public void ShowFileContent(List<string[]> readMenuTextList)
		{
			Console.WriteLine("delegate func");
			foreach (string[] textsArray in readMenuTextList)
                {
                    foreach (string word in textsArray)
                    {
                        Console.Write(word + "|");
                    }
                    Console.WriteLine();
                }
		}
		public static Dictionary<int, IFrameItem> CreateItemsList(List<string[]> readItemsList,string itemsFile)
		{
			Dictionary<int, IFrameItem> allItemsDict = new Dictionary<int, IFrameItem>();
			foreach (string[] textsArray in readItemsList)
            {
                int itemNr, posCol, posRow, link;
				string labelText;
				bool dyn, actTrig;

                int.TryParse(textsArray[0], out itemNr);
				labelText = textsArray[1];
				int.TryParse(textsArray[2], out posCol);
				int.TryParse(textsArray[3], out posRow);
				bool.TryParse(textsArray[4], out dyn);
				bool.TryParse(textsArray[5], out actTrig);
				int.TryParse(textsArray[6], out link);
				IFrameItem item = new FrameItemDisplay(itemNr, labelText, posCol, posRow, dyn, actTrig, link);
				allItemsDict.Add(item.FrameItemNr,item);
            }
            return allItemsDict;
		}

		public static List<IFrame<IFrameItem>> CreateFramesList(List<string[]> readFramesList, string itemsFile)
		{
			List<IFrame<IFrameItem>> framesList = new List<IFrame<IFrameItem>>();

			foreach (string[] frameParams in readFramesList)
            {
				int frameNr, rows,  cols,  activeItemKey; //first four elements from frameParams
				int[] frameItemKeys = new int[frameParams.Length-4]; //next elements will form an int[]
				
				int.TryParse(frameParams[0], out frameNr);
				int.TryParse(frameParams[1], out rows);
				int.TryParse(frameParams[2], out cols);
				int.TryParse(frameParams[3], out activeItemKey);
				
				for(int i=0; i<frameItemKeys.Length;i++)
				{
					int.TryParse(frameParams[i+4], out frameItemKeys[i]);
				}
				Dictionary<int, IFrameItem> allItemsDict = Actions.ParseItemList(itemsFile);
                Dictionary<int, IFrameItem> itemsDictframe = new Dictionary<int, IFrameItem>();
				foreach(int key in frameItemKeys)
				{
					itemsDictframe.Add(key,allItemsDict[key]);
				} 

				IFrame<IFrameItem> frame = new FrameDisplay(frameNr, rows, cols, itemsDictframe, activeItemKey);
				framesList.Add(frame);
				itemsDictframe.Clear();
			}

			return framesList;
		}

    }

	class TestCompleteMenu
	{
		
	}
}

