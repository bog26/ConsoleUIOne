using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	class FrameDisplay:IFrame<IFrameItem> 
    {
		private int frameNr;
		private int[] gridSize;//tuple?
		private List<IFrameItem> displayItems;
		private List<IFrameItem> dynamicItems;
		private Dictionary<int,IFrameItem> displayItemsDict;
		private Dictionary<int,IFrameItem> dynamicItemsDict;

		private int cursorPosition;
		private List<int> orderedKeys; 
		private IFrameItem crtDisplayItem;

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
			this.displayItems = new List<IFrameItem>() ;

		}
		public FrameDisplay(int frameNr,int rows, int cols, List<IFrameItem> dispItems)		
		{
			this.frameNr = frameNr;
			this.cursorPosition = 0;
			this.isDynamic = false;
			this.gridSize = new int[]{rows,cols};
			this.displayItems = new List<IFrameItem>() ;
			foreach(IFrameItem item in dispItems)
			{
				this.displayItems.Add(item);
			}
			this.crtDisplayItem = dispItems[cursorPosition];
			foreach(IFrameItem item in dispItems)
			{
				if(item.IsDynamic)
				{
					this.isDynamic = true;
					this.dynamicItems.Add(item);
				}
			}
		}
		public FrameDisplay(int frameNr,int rows, int cols, Dictionary<int,IFrameItem> dispItemsD, int activeItemKey)	
		{
			this.frameNr = frameNr;
			this.isDynamic = false;
			this.gridSize = new int[]{rows,cols};
			this.cursorPosition = 0;
			this.displayItemsDict = new Dictionary<int, IFrameItem>();
			Dictionary<int, IFrameItem> dispItemsDSorted = SortItems(dispItemsD);
			foreach(KeyValuePair<int,IFrameItem> pair in dispItemsDSorted)
			{
				this.displayItemsDict.Add(pair.Key, pair.Value);
			}
			foreach(KeyValuePair<int,IFrameItem> pair in dispItemsDSorted)
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

		public List<IFrameItem> DisplayItems
		{
			get{return this.displayItems;}
			set{this.displayItems = value;}
		}
		public Dictionary<int,IFrameItem> DisplayItemsDict
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
		public IFrameItem CrtDisplayItem
		{
			get{return this.crtDisplayItem;}
			set{this.crtDisplayItem = value;}
		}
		public bool IsDynamic
		{
			get{return this.isDynamic;}
			set{this.isDynamic = value;}
		}
		public Dictionary<int,IFrameItem> SortItems(Dictionary<int,IFrameItem> dict)
		{
			List<KeyValuePair<int,IFrameItem>> sortedList = new List<KeyValuePair<int,IFrameItem>>();
			Dictionary<int,IFrameItem> sortedDict = new Dictionary<int,IFrameItem>();
			foreach(KeyValuePair<int,IFrameItem> pair in dict)
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
			return sortedDict;
		}
		public void UpdateFrameItem(IFrameItem item)
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

