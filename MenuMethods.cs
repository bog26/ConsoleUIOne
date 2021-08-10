using System;
using System.IO;
using System.Collections.Generic;
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

			static public void ReadMenuFile()
			{
				string path = Directory.GetCurrentDirectory();
				string file = "ItemsDataFile.txt";
				try
				{
				StreamReader reader = new StreamReader(file);
				using (reader)
					{
						string readerStr = reader.ReadToEnd();
						Console.WriteLine(readerStr);
						//return readerStr;
					}
				}
				catch (FileNotFoundException e)
				{
					Console.WriteLine($"There was an issue! {e.Message}");
					//return null;
				}
				catch (IOException e)
				{
					Console.WriteLine($"Cannot read file! Details: {e.Message}");
					//return null;
				}
				finally
				{
					Console.WriteLine("finished");
				}	
			}
			static public void ReadMenuFileLines()
			{
				string file = "ItemsDataFile.txt";
				try
				{
				StreamReader reader = new StreamReader(file);
				using (reader)
					{
						while(!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							if (!line.StartsWith("*"))
							{
								Console.WriteLine(line);
							}
							
						}
					}
				}
				catch (FileNotFoundException e)
				{
					Console.WriteLine($"There was an issue! {e.Message}");
				}
				catch (IOException e)
				{
					Console.WriteLine($"Cannot read file! Details: {e.Message}");
				}

				finally
				{
					Console.WriteLine("finished");
				}	

			}

			//static public Dictionary<int,IFrameItem> ReadFrameItems()
			static public List<string[]> ReadMenuTextLines(string separator, string fileName)
			{
				string[] textArr;
				try
				{
				StreamReader reader = new StreamReader(fileName);
				List<string[]> readMenuTextList = new List<string[]>(); 
				using (reader)
					{
						while(!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							if (!line.StartsWith("*"))
							{
								textArr = line.Split(separator);
								string[] lineTrimmedArr = new string[textArr.Length];
								for(int i=0; i<textArr.Length; i++)
								{
									lineTrimmedArr[i] = textArr[i].Trim(' ','"');
								}
								readMenuTextList.Add(lineTrimmedArr);
							}
						}
					}
				foreach(string[] textsArray in readMenuTextList)
				{
					foreach(string word in textsArray)
					{
						Console.Write(word +"|");		
					}
					Console.WriteLine();
				}
				return readMenuTextList;
				}
				catch (FileNotFoundException e)
				{
					Console.WriteLine($"There was an issue! {e.Message}");
					return null;
				}
				catch (IOException e)
				{
					Console.WriteLine($"Cannot read file! Details: {e.Message}");
					return null;
				}
				finally
				{
					Console.WriteLine("finished");
				}
			}
			static public Dictionary<int,IFrameItem> ParseItemLine(List<string[]> ItemList)
			{
				Dictionary<int,IFrameItem> allItemsDict = new Dictionary<int,IFrameItem>();
				foreach(string[] textsArray in ItemList)
				{
					int itemNr;
					int.TryParse(textsArray[0], out itemNr);
					//tbd parse for all elements in textsArray, except textsArray[1], which is already string!!
					//item = new FrameItemDisplay(10,"LabelTen",4,8,false,true,3);
					//allItemsDict.Add(item.itemNr,item)
				}
				return allItemsDict;

			}


			public static string[] stringSplit(string text)
        	{
            	char StrSeparator =  'y';
            	string[] textArr = text.Split(StrSeparator);
            	return textArr;
        	}


		}
}

