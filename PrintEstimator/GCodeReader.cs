using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class GCodeReader
    {
        //TODO: add error checking here
        public List<List<string>> FileParser(string fileLocation)
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
}
