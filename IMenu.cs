using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using IOMethNS;

namespace ShellMenuNS
{
	
	interface IMenu<T> 
    {
		List<T> DisplayFrames {get;set;}
		List<T> DynamicFrames {get;set;}
    }
}

	