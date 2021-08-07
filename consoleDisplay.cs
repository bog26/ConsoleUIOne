using System;
using System.Collections.Generic;

namespace ShellMenuNS
{
	class consoleDisplay
    {
		//https://stackoverflow.com/questions/5435460/console-application-how-to-update-the-display-without-flicker
		public static int xCalc(int col)
		{
			int xCursor;
			xCursor = col*5;
			return xCursor;
		}
		public static int yCalc(int row)
		{
			int yCursor;
			yCursor = row*2;
			return yCursor;
		}
		public static void DisplayOuterFrame(int startCol, int startRow, int cols, int rows, string hLine, string vLine)
		{
			Console.SetCursorPosition(xCalc(startCol), yCalc(startRow));
			for(int i=0;i<xCalc(cols);i++)
			{
				Console.Write(hLine);
			}
			for(int i=1;i<yCalc(rows);i++)
			{
				Console.SetCursorPosition(startRow,i);
				Console.Write(vLine);
				Console.SetCursorPosition(xCalc(cols)-1,i);
				Console.Write(vLine);
			}
			Console.SetCursorPosition(xCalc(startCol), yCalc(rows));
			for(int i=0;i<xCalc(cols);i++)
			{
				Console.Write(hLine);
			}
		}

		public static void DisplayItemFrame(int startCol, int startRow, int textLenght, string hLine, string vLine)
		{
			Console.SetCursorPosition(xCalc(startCol)-1,yCalc(startRow)-1);
			for(int i=0;i<=textLenght+1;i++)
			{
				Console.Write(hLine);
			}
			Console.SetCursorPosition(xCalc(startCol)-1,yCalc(startRow));
			Console.Write(vLine);
			Console.SetCursorPosition(xCalc(startCol)-1+textLenght+1,yCalc(startRow));
			Console.Write(vLine);
			Console.SetCursorPosition(xCalc(startCol)-1,yCalc(startRow)+1);
			for(int i=0;i<=textLenght+1;i++)
			{
				Console.Write(hLine);
			}
		}

		public static void DisplayLabel(int x, int y, string text)
		{
			Console.SetCursorPosition(xCalc(x), yCalc(y));
			Console.Write(text);
		}
	}
		
}

