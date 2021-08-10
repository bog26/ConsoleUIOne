using System;
using System.IO;
//using learningNS;
//using word = Microsoft.Office.Interop.Word;

namespace ShellMenuNS
{
	class Actions
    	{
        	static public void ShowCrtDirectory()
			{
				string path = Directory.GetCurrentDirectory();
				Console.WriteLine($"current directory:{path}");
			}
		}
}

