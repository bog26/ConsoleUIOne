using System;
using System.IO;
using System.Collections.Generic;
//using learningNS;
//using word = Microsoft.Office.Interop.Word;

namespace ShellMenuNS
{
    public class Actions
    {
        //public delegate void PrintConfigFile(List<string[]> readMenuTextList);
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

        static public Dictionary<int, IFrameItem> ParseItemListDeleg(string ItemListFile, 
            Func<List<string[]>, string, Dictionary<int, IFrameItem>> createItems)
        {
            Dictionary<int, IFrameItem> allItemsDict = new Dictionary<int, IFrameItem>();
			List<string[]> ItemList = ReadMenuTextLines(",", ItemListFile);
            allItemsDict = createItems(ItemList,ItemListFile);
            return allItemsDict;
        }

        static public List<IFrame<IFrameItem>> ParseFrameListDeleg(string frameFile, string itemsFile, 
            Func<List<string[]>, string, List<IFrame<IFrameItem>>> listOfFrames)
        {
            List<IFrame<IFrameItem>> framesList = new List<IFrame<IFrameItem>>();
            List<string[]> readFramesList = ReadMenuTextLines(",", frameFile);
            framesList = listOfFrames(readFramesList, itemsFile) ;
            return framesList;
        }
    }
}

