using System;
using System.IO;
using System.Collections.Generic;
//using learningNS;
//using word = Microsoft.Office.Interop.Word;

namespace ShellMenuNS
{
    public class Actions
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
                    while (!reader.EndOfStream)
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
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (!line.StartsWith("*"))
                        {
                            textArr = line.Split(separator);
                            string[] lineTrimmedArr = new string[textArr.Length];
                            for (int i = 0; i < textArr.Length; i++)
                            {
                                lineTrimmedArr[i] = textArr[i].Trim(' ', '"');
                            }
                            readMenuTextList.Add(lineTrimmedArr);
                        }
                    }
                }
                foreach (string[] textsArray in readMenuTextList)
                {
                    foreach (string word in textsArray)
                    {
                        Console.Write(word + "|");
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
        //static public Dictionary<int, IFrameItem> ParseItemList(List<string[]> ItemList)
		static public Dictionary<int, IFrameItem> ParseItemList(string ItemListFile)
        {
			Console.WriteLine("parsing item list file");
            Dictionary<int, IFrameItem> allItemsDict = new Dictionary<int, IFrameItem>();
			List<string[]> ItemList = ReadMenuTextLines(",", ItemListFile);
            foreach (string[] textsArray in ItemList)
            {
                int itemNr, posCol, posRow, link;
				string labelText;
				bool dyn, actTrig;

                int.TryParse(textsArray[0], out itemNr);
				labelText = textsArray[1];
				int.TryParse(textsArray[2], out posCol);
				int.TryParse(textsArray[3], out posRow);
				bool.TryParse(textsArray[4], out dyn);
				bool.TryParse(textsArray[5], out actTrig);
				int.TryParse(textsArray[6], out link);
				IFrameItem item = new FrameItemDisplay(itemNr, labelText, posCol, posRow, dyn, actTrig, link);
				allItemsDict.Add(item.FrameItemNr,item);
            }
            return allItemsDict;

        }
		//static public List<IFrame<IFrameItem>> ParseFrameList(Dictionary<int, IFrameItem> frameItemsDict)
		
        static public List<IFrame<IFrameItem>> ParseFrameList(string frameFile, string itemsFile)
        {
			Console.WriteLine("parsing frame list file");
			//List<string[]> readFramesList = ReadMenuTextLines(",", file);
            List<string[]> readFramesList = ReadMenuTextLines(",", frameFile);
			List<IFrame<IFrameItem>> framesList = new List<IFrame<IFrameItem>>();;
			foreach (string[] frameParams in readFramesList)
            {
				int frameNr, rows,  cols,  activeItemKey; //first four elements from frameParams
				int[] frameItemKeys = new int[frameParams.Length-4]; //next elements will form an int[]
				
				int.TryParse(frameParams[0], out frameNr);
				int.TryParse(frameParams[1], out rows);
				int.TryParse(frameParams[2], out cols);
				int.TryParse(frameParams[3], out activeItemKey);
				
				for(int i=0; i<frameItemKeys.Length;i++)
				{
					int.TryParse(frameParams[i+4], out frameItemKeys[i]);
				}
				Dictionary<int, IFrameItem> allItemsDict = ParseItemList(itemsFile);
                Dictionary<int, IFrameItem> itemsDictframe = new Dictionary<int, IFrameItem>();
				foreach(int key in frameItemKeys)
				{
					itemsDictframe.Add(key,allItemsDict[key]);
				} 

				IFrame<IFrameItem> frame = new FrameDisplay(frameNr, rows, cols, itemsDictframe, activeItemKey);
				framesList.Add(frame);
				itemsDictframe.Clear();
			}

			return framesList;
		}
/*
        static public IFrame<IFrameItem> ConstructFrameFromLine()
        {
            IFrame<IFrameItem> frame;

            return frame;
        } 
*/


        public static string[] stringSplit(string text)
        {
            char StrSeparator = 'y';
            string[] textArr = text.Split(StrSeparator);
            return textArr;
        }


    }
}

