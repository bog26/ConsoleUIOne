using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	class FrameDisplay 
    {
		private int frameNr;
		private int[] gridSize;//tuple?
		private List<FrameItemDisplay> displayItems;
		private List<FrameItemDisplay> dynamicItems;
		private Dictionary<int,FrameItemDisplay> displayItemsDict;
		private Dictionary<int,FrameItemDisplay> dynamicItemsDict;
		private int cursorPosition;
		private List<int> orderedKeys; 
		private FrameItemDisplay crtDisplayItem; 
		private bool isDynamic;
		public FrameDisplay()
		{
			this.cursorPosition = 0;
			this.isDynamic = false;
			this.gridSize = new int[]{20,10};
		}
		public FrameDisplay(int frameNr,int rows, int cols)
		{
			this.frameNr = frameNr;
			this.cursorPosition = 0;
			this.isDynamic = false;
			this.gridSize = new int[]{rows,cols};
			this.displayItems = new List<FrameItemDisplay>() ;
		}
		public FrameDisplay(int frameNr,int rows, int cols, List<FrameItemDisplay> dispItems)
		{
			this.frameNr = frameNr;
			this.cursorPosition = 0;
			this.isDynamic = false;
			this.gridSize = new int[]{rows,cols};
			this.displayItems = new List<FrameItemDisplay>() ;
			foreach(FrameItemDisplay item in dispItems)
			{
				this.displayItems.Add(item);
			}
			this.crtDisplayItem = dispItems[cursorPosition];
			foreach(FrameItemDisplay item in dispItems)
			{
				if(item.IsDynamic)
				{
					this.isDynamic = true;
					this.dynamicItems.Add(item);
				}
			}
		}
		public FrameDisplay(int frameNr,int rows, int cols, Dictionary<int,FrameItemDisplay> dispItemsD, int activeItemKey)
		{
			this.frameNr = frameNr;
			this.isDynamic = false;
			this.gridSize = new int[]{rows,cols};
			this.cursorPosition = 0;
			//this.displayItems = new List<FrameItemDisplay>() ;
			this.displayItemsDict = new Dictionary<int, FrameItemDisplay>();
			//sort dispItemsD
			Dictionary<int, FrameItemDisplay> dispItemsDSorted = SortItems(dispItemsD);
			foreach(KeyValuePair<int,FrameItemDisplay> pair in dispItemsDSorted)
			{
				this.displayItemsDict.Add(pair.Key, pair.Value);
				//this.cursorPosition = pair.Key;//??
			}
			foreach(KeyValuePair<int,FrameItemDisplay> pair in dispItemsDSorted)
			{
				if(pair.Value.IsDynamic)
				{
					this.isDynamic = true;
					this.dynamicItemsDict.Add(pair.Key, pair.Value);
				}
			}
			this.orderedKeys = new List<int>();
			foreach(int key in displayItemsDict.Keys)
			{
				this.orderedKeys.Add(key);
			}
			this.crtDisplayItem = dispItemsDSorted[orderedKeys[0]];
		}
		public int FrameNr
		{
			get{return this.frameNr;}
			set{this.frameNr = value;}
		}
		public int[] GridSize
		{
			get{return this.gridSize;}
			set{this.gridSize = value;}
		}

		public List<FrameItemDisplay> DisplayItems
		{
			get{return this.displayItems;}
			set{this.displayItems = value;}
		}
		public Dictionary<int,FrameItemDisplay> DisplayItemsDict
		{
			get{return this.displayItemsDict;}
			set{this.displayItemsDict = value;}
		}

		public int CursorPosition
		{
			get{return this.cursorPosition;}
			set{this.cursorPosition = value;}
		}

		public List<int> OrderedKeys
		{
			get{return this.orderedKeys;}
			set{this.orderedKeys = value;}
		}

		public FrameItemDisplay CrtDisplayItem
		{
			get{return this.crtDisplayItem;}
			set{this.crtDisplayItem = value;}
		}
		public bool IsDynamic
		{
			get{return this.isDynamic;}
			set{this.isDynamic = value;}
		}
		Dictionary<int,FrameItemDisplay> SortItems(Dictionary<int,FrameItemDisplay> dict)
		{
			List<KeyValuePair<int,FrameItemDisplay>> sortedList = new List<KeyValuePair<int,FrameItemDisplay>>();
			Dictionary<int,FrameItemDisplay> sortedDict = new Dictionary<int,FrameItemDisplay>();
			foreach(KeyValuePair<int,FrameItemDisplay> pair in dict)
			{
				sortedList.Add(pair);
			}
			KeyValuePair<int,FrameItemDisplay> temp;
			for (int j=0; j<sortedList.Count; j++)
			{
				for(int i=j; i<sortedList.Count; i++)
				{
					if(sortedList[i].Value.Row ==sortedList[j].Value.Row )
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
			foreach(KeyValuePair<int,FrameItemDisplay> pair in sortedList)
			{
				sortedDict.Add(pair.Key,pair.Value);
			}
			return sortedDict;
		}
		
		public void UpdateFrameItem(FrameItemDisplay item)
		{
			if (item.IsDynamic==true && item.ReadyForUpdate==true)
			{
				Console.WriteLine("updating item TBD");
				//https://stackoverflow.com/questions/888533/how-can-i-update-the-current-line-in-a-c-sharp-windows-console-app
				//https://stackoverflow.com/questions/5435460/console-application-how-to-update-the-display-without-flicker
			}
		}
		public void CreateToolbar()
		{

		}
    }
	class TestFrameDisplay
	{
		
	}
}

