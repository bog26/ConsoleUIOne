using System.Collections.Generic;

namespace ShellMenuNS
{
	public interface IFrame<T> 
    {
		int FrameNr {get;set;}
		string FrameTitle {get;set;}
		int[] GridSize {get;set;}
		List<T> DisplayItems {get;set;}
		Dictionary<int,T> DisplayItemsDict {get;set;}
		int CursorPosition {get;set;}
		List<int> OrderedKeys {get;set;}
		T CrtDisplayItem {get;set;}
		bool IsDynamic {get;set;}
		Dictionary<int,T> SortItems(Dictionary<int,T> dict);
		void UpdateFrameItem(T item);
	

    }
}

	