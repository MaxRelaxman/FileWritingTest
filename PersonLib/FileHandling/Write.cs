using PersonLib.models;
using System.IO;
using System.Reflection.PortableExecutable;

namespace PersonLib.FileHandling
{
    public static class Writer
    {
        public static void WritePeople(List<Person> people)
        {
            CreateDirectoryIfNotExist(Settings.FilePath);
            CreateDirectoryIfNotExist(Settings.SpouseFilePath);

            foreach (Person person in people)
            {
                WritePerson(person);
            }
        }

        private static void CreateDirectoryIfNotExist(string dir)
        {
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

        private static void WritePerson(Person pValue)
        {
            string FileName = Path.Combine(Settings.FilePath, Settings.PersonFilename);
            string strOutputLine = pValue.BuildOutputString();

            WriteFile(FileName, strOutputLine);

            if (pValue.spouse != null)
            {
                WriteSpouse(pValue.spouse);
            }
        }

        

        public static void WriteSpouse(PersonSpouse sValue)
        {
            WriteFile(sValue.GenerateFilename(), sValue.BuildOutputString());
        }

        private static void WriteFile(string FileName, string outData)
        {
            //append or create a new file
            if (File.Exists(FileName))
            {
                using (StreamWriter outStream = File.AppendText(FileName))
                {
                    try
                    {
                        outStream.WriteLine(outData);
                    }
                    catch 
                    {
                        Console.WriteLine("Error writing file");
                    }
                }
            }
            else
            {
                using (StreamWriter outStream = File.CreateText(FileName))
                {
                    try
                    {
                        outStream.WriteLine(outData);
                    }
                    catch
                    {
                        Console.WriteLine("Error writing file");
                    }
                }
            }
        }
    }
}