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
		
	}
}

