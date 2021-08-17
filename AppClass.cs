using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	
	public class App // 
    {
		private int appID;
		private string appTextDisplay;

		public App(int id, string text)
		{
			this.appID = id;
			this.appTextDisplay = text;
		}
		
		public int AppID
		{
			get{return this.appID;}
		}
		public string AppTextDisplay
		{
			get{return this.appTextDisplay;}
		}

		public void ConstructMenu()
		{
		} 

		//public void AppMethods(int select)
		public void AppMethods(int select, Func<int> MethodDelegate)
		{
			// delegate method inside app file will contain a switch(int)
			//	 that will trigger other methods
		}




    }
	class TestApp
	{
		
	}
}

