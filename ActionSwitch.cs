using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	
	public static class ActionSwitch 
    {
		static public void ActionSw(int appId, int itemLink)
		{
			switch (appId)
			{
				case 0:
					Console.WriteLine("triggering Main methods");
					// mainapp.methods(itemLink)
					break;
				case 1:
					Console.WriteLine("triggering TimeApp methods");
					//TimeApp.methods(itemLink)
					break;
				case 2:
					Console.WriteLine("triggering FilesApp methods");
					//FilesApp.methods(itemLink)
					break;
				case 3:
					Console.WriteLine("triggering EncryptApp methods");
					//EncryptApp.methods(itemLink)
					break;
			}
		}



    }
	class TestActionSwitch
	{
		
	}
}

