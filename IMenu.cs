using System.Collections.Generic;

namespace ShellMenuNS
{
	public interface IMenu<T,U> 
    {
		public delegate void SwApp(int choice);
		List<T> DisplayFrames {get;set;}
		List<T> DynamicFrames {get;set;}
		T CrtDisplayFrame {get;set;}
		App CrtApp{get;set;}
		
		//void UpdateFrame();
		void DisplayCRTFrame();
		void ShowItemFrame();
		void DeleteItemFrame();
		void SelectCurrentFrameItem(int crtIndex);
		//void keyRead(int crtIndex, int length, out int newIndex, out bool enter);
		//int ActionOnEnter(U FrameItem, int OrderedKeysCrtIndex);
		//void ActionTrigger(int ItemLink);
		//void UpdateFrame(SwApp swApp);

    }
}

	