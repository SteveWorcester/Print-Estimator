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
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double ZCoordinate { get; set; }
        public double LastXCoordinate { get; set; }
        public double LastYCoordinate { get; set; }
        public double LastZCoordinate { get; set; }

        public double FeedRateInMmPerSecond { get; set; } // must be in millimeters per second squared format; GCode comes out as millimeters per minute squared
        public double ExtrudeLength { get; set; }
        public double LastExtrudeLength { get; set; }
        public double TotalExtrudeLength { get; set; }

        /// <summary>
        /// Turns a parsed GCode into a usable collection
        /// </summary>
        /// <param name="parsedFile"></param>
        /// <returns></returns>
        public List<KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>>> CreateMovementList(List<List<string>> parsedFile)
        {
            List<KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>>> movementList = new List<KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>>>();
            for (int i = 0; i < parsedFile[i].Count; i++)
            {
                bool success = false;
                Enums.Movement enumChanger;
                success = Enum.TryParse(parsedFile[i][0], out enumChanger);
                if (success)
                {
                    parsedFile[i].Remove(parsedFile[i][0]);
                    List<KeyValuePair<Enums.Parameter, double>> parameterEnumList = CreateParameterList(parsedFile[i]);
                    movementList.Add(new KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>> (enumChanger, parameterEnumList));
                }
                else
                {
                    parsedFile.Remove(parsedFile[i]);
                    Console.WriteLine($"Removing movement code: {parsedFile[i]}");
                }
            }
            return movementList;
        }

        private List<KeyValuePair<Enums.Parameter, double>> CreateParameterList (List<string> parameterStringList)
        {
            List<KeyValuePair<Enums.Parameter, double>> parameterList = new List<KeyValuePair<Enums.Parameter, double>>();
            for (int i = 0; i < parameterStringList.Count; i++)
            {
                bool firstLetterSuccess = false;
                string firstLetter = parameterStringList[i][0].ToString();
                parameterStringList.Remove(firstLetter);
                Enums.Parameter enumChanger;
                firstLetterSuccess = Enum.TryParse(firstLetter, out enumChanger);

                bool coordinateSuccess = false;
                double coordinate;
                coordinateSuccess = double.TryParse(parameterStringList[i], out coordinate);
                
                if (firstLetterSuccess && coordinateSuccess)
                {
                    parameterList.Add(new KeyValuePair<Enums.Parameter, double>(enumChanger, coordinate));
                }
                else
                {
                    parameterStringList.Remove(parameterStringList[i]);
                    Console.WriteLine($"Removing parameter: {firstLetter}{parameterStringList[i]}");
                }
            }
            return parameterList;
        }

        /// <summary>
        /// Calculates the time of a full GCode file
        /// </summary>
        /// <param name="parsedFile"></param>
        /// <returns></returns>
        public long CalculateTime(List<KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>>> gCode)
        {
            
            Calculations Calculate = new Calculations();
            double totalTimeInSeconds = 0;
            for (int i = 0; i < gCode.Count; i++)
            {
                List<double> coordinatesList = new List<double>() { XCoordinate, YCoordinate, ZCoordinate,
                    LastXCoordinate, LastYCoordinate, LastZCoordinate };

                switch (gCode[i].Key)
                {
                    case Enums.Movement.G0:
                    case Enums.Movement.G1:
                        ParseCoordinates(gCode[i].Value);
                        double distance = Calculate.CalculateDistanceBetweenPoints(coordinatesList);
                        totalTimeInSeconds += Calculate.CalculateMaxSpeedTravelTime(FeedRate, distance, ExtrudeLength);
                        break;
                    case Enums.Movement.G2:
                        break;
                    case Enums.Movement.G3:
                        break;
                    case Enums.Movement.G10:
                        break;
                    case Enums.Movement.G11:
                        break;
                    case Enums.Movement.G42:
                        break;
                    case Enums.Movement.G92:
                        ParseCoordinates(gCode[i].Value); // this is for setting position, not for direct movement; only ParseCoordinates is needed
                        break;
                    case Enums.Movement.M0:
                        break;
                    case Enums.Movement.M1:
                        break;
                    case Enums.Movement.M2:
                        break;
                    case Enums.Movement.M101:
                        break;
                    case Enums.Movement.M102:
                        break;
                    case Enums.Movement.M103:
                        break;
                    default:
                        break;
                }

            }
            return 0;
        }

        private void ParseCoordinates(List<KeyValuePair<Enums.Parameter, double>> parameterList)
        {
            for (int i = 0; i < parameterList.Count; i++)
            {
                switch (parameterList[i].Key)
                {
                    case Enums.Parameter.X:
                        LastXCoordinate = XCoordinate;
                        XCoordinate = parameterList[i].Value;
                        break;
                    case Enums.Parameter.Y:
                        LastYCoordinate = YCoordinate;
                        YCoordinate = parameterList[i].Value;
                        break;
                    case Enums.Parameter.Z:
                        LastZCoordinate = ZCoordinate;
                        ZCoordinate = parameterList[i].Value;
                        break;
                    case Enums.Parameter.F:
                        double tempSqrtHolder = Math.Sqrt(parameterList[i].Value) * 60; // GCode measures feedrate in mm/min^2; *60 turns it into mm/x^s
                        FeedRateInMmPerSecond = tempSqrtHolder * tempSqrtHolder;
                        break;
                    case Enums.Parameter.E:
                        LastExtrudeLength = ExtrudeLength;
                        if (parameterList[i].Value == 0)
                        {
                            TotalExtrudeLength = 0;
                        }
                        else
                        {
                            ExtrudeLength = parameterList[i].Value ;
                            TotalExtrudeLength += ExtrudeLength;
                        }
                        break;
                    default:
                        parameterList.Remove(parameterList[i]);
                        break;
                }
            }
        }
    }

}




