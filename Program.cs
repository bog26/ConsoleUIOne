using System;
using System.Collections;
using System.Collections.Generic;
//using System.Math;
//using static test.TestingImport;
//using Objects;
//using static timeToolsNS.timeToolsCLS;
//using static messagesRandNS.messagesRandCLS;
using static exceptionsNS.exceptionsCLS;
//using static learningNS.ch12CLS;
//using static learningNS.ch13CLS;
//using static encryptionNS.encryptionCLS;
//using static learningNS.StudentCLS;
//using learningNS;
using ShellMenuNS;
using IOMethNS;
//using  learningNS;
//using DataStr;

//https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/create

namespace ConsoleUIOne
{
    class Program
    {
        public static void Main(string[] args)
        {
            CompleteMenu menu = MenuInstantiation.Instance;
            menu.UpdateFrame();
        }
        public static void MenuTesting(CompleteMenu menu)
        {
            //menu.DisplayCRTFrame();
            //Console.WriteLine(menu.DisplayFrames[0].DisplayItems[0].FrameItemNr);
        }
        

    }
}
