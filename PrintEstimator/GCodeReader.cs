using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class GCodeReader
    {
        public List<List<string>> FileParser(string fileLocation)
        {
            if (fileLocation.Length > 0 && File.Exists(fileLocation))
            {
                string path = fileLocation;
                using (StreamReader sr = new StreamReader(path))
                {
                    string fullGCodeFile = System.IO.File.ReadAllText(fileLocation);
                    List<string> lineSplit = fullGCodeFile.Split('\n', '\r').ToList();
                    for (int i = 0; i < lineSplit.Count; i++)
                    {
                        string line = lineSplit[i];
                        int index = line.IndexOf(";");
                        if (index > 0)
                        {
                            line = line.Substring(0, index);
                        }
                    }
                    List<List<string>> fullParse = new List<List<string>>();
                    foreach (var line in lineSplit)
                    {
                        List<string> parsedLine = line.Split(' ').ToList();
                        fullParse.Add(parsedLine);
                    }
                    return fullParse;
                }
            }
            else
            {
                Console.WriteLine("File not found. Please try again: ");
            }
            string tryAgain = Console.ReadLine();
            return FileParser(tryAgain);
        }
    }
}
