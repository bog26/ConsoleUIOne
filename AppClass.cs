using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	
	public class App:IApp
    {
		private int appID;
		private string appName;
		private List<App> apps = null;
		private string framesFile;
		private string itemsFile;
		private CompleteMenu application;

		public App(int id, string text)
		{
			this.appID = id;
			this.appName = text;
			string completeFileName = ConstructFileName(text);
			string itemsFile;
			string framesFile;
			ReadConfigFile(completeFileName, out itemsFile, out framesFile);
			this.application = new CompleteMenu(0,Actions.ParseFrameListDeleg(framesFile, itemsFile, CompleteMenu.CreateFramesList));

		}
		public static string ConstructFileName(string text)
		{
			string completeFileName = text + ".txt";
			return completeFileName;
		}
		public static void ReadConfigFile(string file, out string items, out string frames)
		{
			List<string[]> framesAndItemsConfigFiles = new List<string[]>();
			framesAndItemsConfigFiles = Actions.ReadMenuTextLines(",",file);
			items = framesAndItemsConfigFiles[0][0];
			frames = framesAndItemsConfigFiles[0][1];
		}

		public static CompleteMenu ConstructMenu(string FramesFile, string ItemsFile)
		{
			CompleteMenu menu = new CompleteMenu(0,Actions.ParseFrameListDeleg(FramesFile, ItemsFile, CompleteMenu.CreateFramesList)); 
			return menu;
		}

		public int AppID
		{
			get{return this.appID;}
		}
		public string AppName
		{
			get{return this.appName;}
		}
		public List<App>  Apps
		{
			get{return this.apps;}
			set{this.apps = value;}
		}
		

		public string FramesFile
		{
			get{return this.framesFile;}
		}
		
		public string ItemsFile
		{
			get{return this.itemsFile;}
		}
		public void ConstructMenu()
		{

		} 
		public int SetAppID()
		{
			int appIdNr = 0;
			return appIdNr;
		}
		public CompleteMenu Application
		{
			get{return this.application;}
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

