using System.Collections.Generic;

namespace ShellMenuNS
{
	public interface IFrameItem
    {
		int FrameItemNr {get;set;}
		string TextDisplay {get;set;}
		int Column {get;set;}
		int Row {get;set;}
		int[] GridPosition {get;set;}
		bool IsCrtDisplayItem {get;set;}
		bool IsDynamic {get;set;}
		bool IsActionTrigger {get;}
		int Link {get;}
		bool ReadyForUpdate {get;set;}
		Dictionary<int,IFrameItem> ConstructSortedItemsDict();
		Dictionary<int,IFrameItem> ReadItemsFile();
		Dictionary<int,IFrameItem> SortItems(Dictionary<int,IFrameItem> unsortedDict);


    }
}

	