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

        }
        
        public List<KeyValuePair<Enums.Movement, List<string>>> CreateMovementList(List<List<string>> parsedFile)
        {
            List<KeyValuePair<Enums.Movement, List<string>>> movementList = new List<KeyValuePair<Enums.Movement, List<string>>>();
            for (int i = 0; i < parsedFile[i].Count; i++)
            {
                Enums.Movement enumChanger;
                bool success = Enum.TryParse(parsedFile[i][0], out enumChanger);
                if (success)
                {
                    parsedFile[i].Remove(parsedFile[i][0]);
                    movementList.Add(new  KeyValuePair<Enums.Movement, List<string>>(enumChanger, parsedFile[i]));
                }
                else
                {
                    parsedFile.Remove(parsedFile[i]);
                }
            }
            return movementList;
        }
        public List<KeyValuePair<Enums.Movement, List<Enums.Parameter>>> CreateParameterList (List<KeyValuePair<Enums.Movement, List<string>>> movementList)
        {
            List<KeyValuePair<Enums.Movement, List<Enums.Parameter>>> fullMovementAndParameterList = new List<KeyValuePair<Enums.Movement, List<Enums.Parameter>>>();
            foreach (var item in movementList)
            {
                // dafuq. figure out how to iterate through this shit.....
            }
            return 0;
        }
        /// <summary>
        /// Calculates the time of a full GCode file
        /// </summary>
        /// <param name="parsedFile"></param>
        /// <returns></returns>
        public double CalculateTime(List<List<string>> parsedFile)
        {


            long totalTimeInSeconds = 0;
            for (int i = 0; i < parsedFile[i].Count; i++)
            {

                for (int j = 0; j < parsedFile[i][j].Length; j++)
                {

                    switch (parsedFile[i][j])
                    {
                        //Calculations calculate = new Calculations();
                        //case Enums.Movement.G0:
                        //
                        //    break;
                    }
                }
            }
            return 0;
        }
    }
    }




