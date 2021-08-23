using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	//public interface IApp<T,U>:IMenu<T,U>
	//public interface IApp<T>
	public interface IApp
    {
		int AppID {get;}
		string AppName {get;}
		List<App> Apps {get;set;}
		string FramesFile {get;}
		string ItemsFile {get;}

		void ConstructMenu();
		int SetAppID();
		//void AppMethods(int select, Func<int> MethodDelegate);

    }
}

	