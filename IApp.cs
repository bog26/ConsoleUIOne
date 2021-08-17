using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	public interface IApp<T,U>:IMenu<T,U>
    {
		int AppID {get;}
		string AppTextDisplay {get;}
		void ConstructMenu();
		void AppMethods(int select, Func<int> MethodDelegate);


    }
}

	