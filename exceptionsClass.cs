using System;
using System.IO;
namespace exceptionsNS
{
	//class IOMethodsCLS
	class exceptionsCLS
    	{
        	public static void DirSetExceptionWrap(string dir)
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
				Console.WriteLine("end of DirSetExceptionWrap");
			}
		
        	}
			public static void StreamExceptionWrap()
			{
				Console.WriteLine("StreamExceptionWrap TBD");
			}
			
			//public static void readFile(string file)
			public static void readFileExceptionWrap(string file)
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

				finally
				{
					Console.WriteLine("finished");
				}	
			}

			public static void readUserFileTest()
			{
				Console.WriteLine("please insert directory");
				string dirPath = Console.ReadLine();
				string correctedDirPath = @dirPath;
				//Console.WriteLine(correctedFilePath);
				DirSetExceptionWrap(correctedDirPath);
				Console.WriteLine("please insert file name");
				string filePath = Console.ReadLine();
				string correctedFilePath = @filePath;
				readFileExceptionWrap(filePath);
			}	

    	}


}

