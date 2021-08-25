using System;
using System.IO;
using System.Collections.Generic;
//using learningNS;
//using word = Microsoft.Office.Interop.Word;

namespace IOMethNS
{
	public static class IOMethodsCLS
    	{
        	public static void DirSet(string dir)
        	{
			//https://docs.microsoft.com/en-us/dotnet/api/system.io.directorynotfoundexception?view=net-5.0
			
				try
				{
					Directory.SetCurrentDirectory(dir);
					Console.WriteLine($"crt dir set: {dir}");
				}
				catch (DirectoryNotFoundException dirEx)
				{
					Console.WriteLine($"Directory not found: {dirEx.Message}");
				
				}
				finally
				{
					Console.WriteLine("end of DirSet");
				}
        	}

			public static void readFile(string file)
			//public static void readFileExceptionWrap(string file)
			{
				try
				{
					StreamReader reader = new StreamReader(file);
					//using (StreamReader reader = new StreamReader(file))
					using (reader)
					{
						while(!reader.EndOfStream)
						{
							string line = reader.ReadLine();
							Console.WriteLine(line);
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

			public static string readFile1(string file)
			{
				try
				{
				StreamReader reader = new StreamReader(file);
				using (reader)
					{
						string readerStr = reader.ReadToEnd();
						return readerStr;
					}
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
			public static string[] stringSplit(string text)
        	{
            	char StrSeparator = 'y';
            	string[] textArr = text.Split(StrSeparator);
            	return textArr;
        	}
			public delegate void PrintConfigFile(List<string[]> readMenuTextList);
			static public void ReadMenuTextLinesDeleg(string separator, string fileName, PrintConfigFile printConfigFile)
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
                	printConfigFile(readMenuTextList);
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

			public static void ShowFileContent(List<string[]> readMenuTextList)
			{
				Console.WriteLine("delegate func");
				foreach (string[] textsArray in readMenuTextList)
                	{
                    	foreach (string word in textsArray)
                    	{
                        	Console.Write(word + "|");
                    	}
                    	Console.WriteLine();
                	}
			}
			static public void ShowCrtDirectory()
        	{
            	string path = Directory.GetCurrentDirectory();
            	Console.WriteLine($"current directory:{path}");
        	}
			public static string UserDefinedFilePath()
			{
				Console.WriteLine("please insert directory");
				string dirPath = Console.ReadLine();
				string correctedDirPath = @dirPath;
				Console.WriteLine("please insert file name");
				string filePath = Console.ReadLine();
				string correctedFilePath = @filePath;
				string completePath = correctedDirPath + "\\" + correctedFilePath;
				return completePath;
			}
    	}
	public class IOMethodsCLSTesting
	{
		public static void readUserFileTest()
			{
				Console.WriteLine("please insert directory");
				string dirPath = Console.ReadLine();
				string correctedDirPath = @dirPath;
				//Console.WriteLine(correctedFilePath);
				IOMethodsCLS.DirSet(correctedDirPath);
				Console.WriteLine("please insert file name");
				string filePath = Console.ReadLine();
				string correctedFilePath = @filePath;
				IOMethodsCLS.readFile(filePath);
			}

		public static string readUserFileTest1()
			{
				Console.WriteLine("please insert directory");
				string dirPath = Console.ReadLine();
				string correctedDirPath = @dirPath;
				//Console.WriteLine(correctedFilePath);
				//IOMethodsCLS.DirSet(correctedDirPath);
				Console.WriteLine("please insert file name");
				string filePath = Console.ReadLine();
				string correctedFilePath = @filePath;
				string completePath = correctedDirPath + "\\" + correctedFilePath;
				string text = IOMethodsCLS.readFile1(completePath);
				return text;
			}	
		static string SearchWordinFileStr = @"*******
The method searches for a word in a text file. Writes sentences containing 
that word into a new text file.
******";
/*
searches for a word in a text file. Writes sentences containing 
that word into a new text file 
*/
		public static string UserFileInput()
		{
			Console.WriteLine("please insert directory");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			Console.WriteLine("please insert file name");
			string filePath = Console.ReadLine();
			string correctedFilePath = @filePath;
			string completePath = correctedDirPath + "\\" + correctedFilePath;
			return completePath;
		}

		public static string UserDirInput()
		{
			Console.WriteLine("please insert directory");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			return correctedDirPath;
		}

		public static void readWordFile()
			{
				string filePathRWord = UserFileInput();
				FileStream fStream = new FileStream(filePathRWord, FileMode.Open, FileAccess.Read);
 				StreamReader sReader = new StreamReader(fStream);
 				string text = sReader.ReadToEnd();
 				sReader.Close();
				Console.WriteLine(text);
				//FileStream fstream = new FileStream(filePathRWord);
				//FileStream(filePathRWord,FileMode.Open,FileAccess.Read);
				//StreamReader sreader = new StreamReader(fstream);
				//txtFileContent.Text = sreader.ReadToEnd();
			}
	}
/*
	public class JsonMethods
	{
		





		public static void CompleteMenuTesting() //creates complete menu from json, >> issue
		{
			Console.WriteLine("please insert directory!");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			IOMethodsCLS.DirSet(correctedDirPath);
			string fileName1 = "jsonStringFrame1.json";
			string fileName2 = "jsonStringFrame2.json";
			string fileName3 = "jsonStringFrame3.json";
			string fileName4 = "jsonStringFrame4.json";
			string[] frameFiles = new string[]{fileName1,fileName2,fileName3,fileName4};
			string JsonFrameObjString1 = File.ReadAllText(frameFiles[0]);
			string JsonFrameObjString2 = File.ReadAllText(frameFiles[1]);
			string JsonFrameObjString3 = File.ReadAllText(frameFiles[2]);
			string JsonFrameObjString4 = File.ReadAllText(frameFiles[3]);
			string[] jsonStrings = new string[]{JsonFrameObjString1,JsonFrameObjString2,JsonFrameObjString3,JsonFrameObjString4};
			//List<FrameDisplay> framesList = new List<FrameDisplay>();
			List<FrameDisplay> framesList = new List<FrameDisplay>();
			//CompleteMenu menu0 = new CompleteMenu();
			foreach(string json in jsonStrings)
			{
				FrameDisplay FrameObj;
				Console.WriteLine("\n*****showing FrameDisplay json*****\n"+json);
				FrameObj = JsonSerializer.Deserialize<FrameDisplay>(json);
				framesList.Add(FrameObj);
				Console.WriteLine("checking FrameObj");
				Console.WriteLine("frames in framesList: "+framesList.Count);
				//Console.WriteLine("nr of DisplayItems in frame: "+FrameObj.DisplayItems.Count); //not ok ,null
				Console.WriteLine("FrameNr: "+FrameObj.FrameNr);
				//Console.WriteLine(framesList[framesList.Count-1].DisplayItems.Count);
				//menu0.AddFrame(FrameObj);
			}
			CompleteMenu menu0 = new CompleteMenu(0,framesList);
			Console.WriteLine("checking CompleteMenu object");
			Console.WriteLine("Count DisplayFrames "+menu0.DisplayFrames.Count);
			var options = new JsonSerializerOptions { WriteIndented = true };
			string jsonStringMenu0 = JsonSerializer.Serialize<CompleteMenu>(menu0,options);
			//string jsonStringMenu0 = JsonSerializer.Serialize<CompleteMenu>(menu0);
			Console.WriteLine("\n***json for menu0:***\n"+jsonStringMenu0);
			string menu0FileName1 = WriteStringToFileTest(jsonStringMenu0);
			Console.WriteLine($"new saved file name is: {menu0FileName1}");
		}
		public static string WriteStringToFileTest(string text)
		{
			Console.WriteLine("please insert directory!");
			string dirPath = Console.ReadLine();
			string correctedDirPath = @dirPath;
			IOMethodsCLS.DirSet(correctedDirPath);
			Console.WriteLine("please insert Filename!");
			//Ex:"jsonStringFrame2.json";
			string fileName = Console.ReadLine();
			File.WriteAllText(fileName, text);
			return fileName;
		}

	}
*/	
}

