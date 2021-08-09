using System.Collections.Generic;

namespace ShellMenuNS
{
	public interface IMenu<T,U> 
    {
		List<T> DisplayFrames {get;set;}
		List<T> DynamicFrames {get;set;}
		T CrtDisplayFrame {get;set;}
		
		void UpdateFrame();
		void DisplayCRTFrame();
		void ShowItemFrame();
		void DeleteItemFrame();
		void SelectCurrentFrameItem(int crtIndex);
		//void keyRead(int crtIndex, int length, out int newIndex, out bool enter);
		int ActionOnEnter(U FrameItem, int OrderedKeysCrtIndex);
		void ActionTrigger(int ItemLink);
    }
}

	