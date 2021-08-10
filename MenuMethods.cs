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


		}
}

