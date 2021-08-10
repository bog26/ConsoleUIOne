using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	
	public class FrameItemDisplay:IFrameItem
    {
		private int frameItemNr;
		private string textDisplay;
		private int row;
		private int column;
		private int[] gridPosition = new int[2];
		private bool isCrtDisplayItem = false; 
		private bool isDynamic= false;
		private bool isActionTrigger= false; 
		private int link = 0; 
		// isActionTrigger => ActionSwitch method (int Link) 
		// !isActionTrigger => FrameSwitch method (int Link) 
		private bool readyForUpdate= false;
		public FrameItemDisplay()
		{
			this.frameItemNr = 0;
			this.textDisplay = "Menu";
			this.row = 0;
			this.column = 0;
			this.gridPosition = new int[]{this.column, this.row};
			this.isCrtDisplayItem = true;
			this.isDynamic = false;
			this.isActionTrigger = false;
			this.link = 0;
			this.readyForUpdate = false;
		}
		public FrameItemDisplay(int ItemNr, string text, int posCol, int posRow, bool dyn, bool actTrig, int lnk)
		{
			this.frameItemNr = ItemNr;
			this.textDisplay = text;
			this.gridPosition = new int[]{posCol, posRow}; // x,y
			this.column = posCol;
			this.row = posRow;
			this.isCrtDisplayItem = false;
			this.readyForUpdate = false;
			this.isDynamic = dyn;
			this.isActionTrigger = actTrig;
			this.link = lnk;
		}
		public int FrameItemNr
		{
			get{return this.frameItemNr;}
			set{this.frameItemNr = value;}
		}
		public string TextDisplay
		{
			get{return this.textDisplay;}
			set{this.textDisplay = value;}
		}
		public int Column  //x
		{
			get{return this.column;}
			set{this.column = value;}
		}
		public int Row  //y
		{
			get{return this.row;}
			set{this.row = value;}
		}
		public int[] GridPosition  
		{
			get{return new int[2]{this.column, this.row};}
			set{this.gridPosition = value;}
		}
		public bool IsCrtDisplayItem
		{
			get{return this.isCrtDisplayItem;}
			set{this.isCrtDisplayItem = value;}
		}
		public bool IsDynamic
		{
			get{return this.isDynamic;}
			set{this.isDynamic = value;}
		}
		public bool IsActionTrigger
		{
			get{return this.isActionTrigger;}
		}
		public int Link  
		{
			get{return this.link;}
		}
		public bool ReadyForUpdate
		{
			get{return this.readyForUpdate;}
			set{this.readyForUpdate = value;}
		}
		public Dictionary<int,IFrameItem> ConstructSortedItemsDict()
		{
			Dictionary<int,IFrameItem> unSortedDict = new Dictionary<int,IFrameItem>();
			Dictionary<int,IFrameItem> sortedDict = new Dictionary<int,IFrameItem>();
			unSortedDict = ReadItemsFile();
			sortedDict = SortItems(unSortedDict);
			return sortedDict;
		}
		public Dictionary<int,IFrameItem> ReadItemsFile() // reads a file containig items data, returns a dictionary
		{
			Dictionary<int,IFrameItem> itemsDict = new Dictionary<int,IFrameItem>();
			//TBD: reads a file containig items data, returns a dictionary
			//string path = Directory.GetCurrentDirectory();

			return itemsDict;
		}

		public Dictionary<int,IFrameItem> SortItems(Dictionary<int,IFrameItem> unsortedDict) // reads a file containig items data, returns a dictionary
		{
			Dictionary<int,IFrameItem> SortedItemsDict = new Dictionary<int,IFrameItem>();
			List<KeyValuePair<int,IFrameItem>> sortedList = new List<KeyValuePair<int,IFrameItem>>();
			Dictionary<int,IFrameItem> sortedDict = new Dictionary<int,IFrameItem>();
			foreach(KeyValuePair<int,IFrameItem> pair in unsortedDict)
			{
				sortedList.Add(pair);
			}
			KeyValuePair<int,IFrameItem> temp;
			for (int j=0; j<sortedList.Count; j++)
			{
				for(int i=j; i<sortedList.Count; i++)
				{
					if(sortedList[i].Value.Row == sortedList[j].Value.Row )
					{
						if(sortedList[i].Value.Column < sortedList[j].Value.Column)
						{
							temp = sortedList[i];
							sortedList[i] = sortedList[j];
							sortedList[j] = temp;
						}
					}
					else
					{
						if(sortedList[i].Value.Row < sortedList[j].Value.Row)
						{
							temp = sortedList[i];
							sortedList[i] = sortedList[j];
							sortedList[j] = temp;
						}
					}
				}
			}
			foreach(KeyValuePair<int,IFrameItem> pair in sortedList)
			{
				sortedDict.Add(pair.Key,pair.Value);
			}
			
			return SortedItemsDict;
		}
    }
	class TestFrameItemDisplay
	{
		
	}
}

