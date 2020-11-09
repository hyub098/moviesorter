using System;
using System.IO;
using System.Linq;

namespace MoviesSorter
{
    class Program
    {
        private const string Path = "D:\\迅雷下载";
        private const int Threshold = 5;

        static void Main(string[] args)
        {
            string[] allfiles = Directory.GetFiles(Path, "*.*", SearchOption.TopDirectoryOnly);

            var filesWithJustNames = allfiles.Select(f => f.Substring(Path.Length + 1)).ToList();

            for(var i = 0; i < filesWithJustNames.Count(); i++)
            {
                var currentNameToCheck = filesWithJustNames[i];
                var allFilesWithSameStartingName = filesWithJustNames.Where(f => f.StartsWith(currentNameToCheck.Substring(0, Threshold)));
                if (allFilesWithSameStartingName.Count() < 2)
                {
                    File.Move($"{Path}\\{currentNameToCheck}", $"{Path}\\single episode\\{currentNameToCheck}"); 
                    continue;
                }

                var directoryName = currentNameToCheck.Split('.')[0];
                var directoryPath = $"{Path}\\{directoryName}";
                Directory.CreateDirectory(directoryPath);

                foreach (var match in allFilesWithSameStartingName)
                {
                    Console.WriteLine(match);
                    File.Move($"{Path}\\{match}", $"{directoryPath}\\{match}");
                }
                filesWithJustNames.RemoveAll(f => f.StartsWith(currentNameToCheck.Substring(0, 4)));
            }

        }
    }
}
