using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{

    class GCodeLogic
    {
        public GCodeLogic()
        {
            //nothing
        }
        /// <summary>
        /// Calculates the time of a full GCode file
        /// </summary>
        /// <param name="parsedFile"></param>
        /// <returns></returns>
        public double CalculateTime(List<List<string>> parsedFile)
        {
            for (int i = 0; i < parsedFile[i].Count; i++)
            {
                Enums.Movement foo;
                bool success = Enum.TryParse(parsedFile[i][0], out foo);
            }

            long totalTimeInSeconds = 0;
            for (int i = 0; i < parsedFile[i].Count; i++)
            {
                for (int j = 0; j < parsedFile[i][j].Length; j++)
                {
                    Calculations calculate = new Calculations();
                    switch (parsedFile[i][j])
                    {
                        case Enums.Movement.G0:

                            break;
                    }
                }




            }
            return 0;
        }


    }
}


