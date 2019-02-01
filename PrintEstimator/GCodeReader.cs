using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class GCodeReader
    {
        public Enums.Movement CurrentGCode { get; set; }

        public List<List<string>> FileParser(string fileLocation)
        {
            string fullGCodeFile = System.IO.File.ReadAllText(fileLocation);

            List<string> lineSplit = fullGCodeFile.Split('\n').ToList();
            List<List<string>> fullParse = new List<List<string>>();
            foreach (var line in lineSplit)
            {
                List<string> parsedLine = line.Split(' ').ToList();
                fullParse.Add(parsedLine);
            }
            return fullParse;
        }

        public void GCodeSelector(int x, int y, int z, int s, int c, Enums.Movement gCode)
        {
            //TODO: fill switch

            // Only GCodes used for estimating time are listed. All other GCodes will default-break to fetch next GCode.
            switch (gCode)
            {
                case Enums.Movement.G0:
                    //linear move
                    break;
                case Enums.Movement.G1:
                    // linear move
                    break;
                case Enums.Movement.G2:
                    //clockwise arc
                    break;
                case Enums.Movement.G3:
                    //counterclockwise arc
                    break;
                default:
                    break;
            }
        }
    }
}
