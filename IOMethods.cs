using System;
using System.IO;
//using learningNS;
//using word = Microsoft.Office.Interop.Word;

namespace IOMethNS
{
	class IOMethodsCLS
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

/*
		public static void SearchWordinFile()
		{
			string filePathR;
			string filePathW;
			string searchWord;
			StreamReader reader;
			StreamWriter writer;

			Console.WriteLine(SearchWordinFileStr);
			Console.WriteLine("Reading file:");
			filePathR = UserFileInput();

			Console.WriteLine("Writing file:");
			filePathW = UserFileInput();
			writer = new StreamWriter(filePathW);
			reader = new StreamReader(filePathR);

			Console.WriteLine("input search word:");
			searchWord = Console.ReadLine();
			Console.WriteLine($"searching for: **{searchWord}** \n inside {filePathR} \n and writing results in {filePathW}");
			using (reader)
			{
				using (writer)
				{
					string readerStr = reader.ReadToEnd();
					string[] allReadTexts = ch13CLS.Ch13Ex10(readerStr); //"." separates text into substrings
					Console.WriteLine(allReadTexts.Length);
					for(int i=0; i<allReadTexts.Length; i++)
					{
						int index = allReadTexts[i].IndexOf(searchWord);
						if(index!=-1)
						{
							Console.WriteLine(allReadTexts[i]+".");
							writer.WriteLine(allReadTexts[i]+".");
						}
					}
				}
			}
		}
		*/
		/*
		public static void SearchWordinFile1()
		{
			string filePathR;
			string filePathW;
			string searchWord;
			StreamReader reader;
			StreamWriter writer;
			Console.WriteLine(SearchWordinFileStr);
			Console.WriteLine("Reading file:");
			string readerStr = readUserFileTest1();
			Console.WriteLine("Writing file:");
			filePathW = UserFileInput();
			writer = new StreamWriter(filePathW);
			Console.WriteLine("input search word:");
			searchWord = Console.ReadLine();
			using (writer)
			{
				string[] allReadTexts = ch13CLS.Ch13Ex10(readerStr); //"." separates text into substrings
				Console.WriteLine(allReadTexts.Length);
				for(int i=0; i<allReadTexts.Length; i++)
				{
					int index = allReadTexts[i].IndexOf(searchWord);
					if(index!=-1)
					{
						Console.WriteLine(allReadTexts[i]+".");
						writer.WriteLine(allReadTexts[i]+".");
					}
				}
			}
			
		}
		*/
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
}

