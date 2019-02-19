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
            for (int i = 0; i < parsedFile.Count; i++)
            {
                for (int j = 0; j < parsedFile[i].Count; j++)
                {
                    bool success = false;
                    Enums.Movement enumChanger;
                    success = Enum.TryParse(parsedFile[i][0], out enumChanger);
                    if (success)
                    {
                        List<KeyValuePair<Enums.Parameter, double>> parameterEnumList = CreateParameterList(parsedFile[i]);
                        movementList.Add(new KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>>(enumChanger, parameterEnumList));
                    }
                    else
                    {
                        Console.WriteLine($"Movement code: {parsedFile[i][j]} unused" +
                            $" - this should not be G1" +
                            $" (GCodeLogic.CreateMovementList)");
                    }
                }
            }
            return movementList;
        }

        private List<KeyValuePair<Enums.Parameter, double>> CreateParameterList (List<string> parameterStringList)
        {
            List<KeyValuePair<Enums.Parameter, double>> parameterList = new List<KeyValuePair<Enums.Parameter, double>>();
            for (int i = 1; i < parameterStringList.Count; i++) // 1 because the first string is the movement command, not the parameter
            {
                if (parameterStringList[i].Equals(""))
                {
                    // NOP
                }
                else
                {
                    bool firstLetterSuccess = false;
                    char firstLetter = parameterStringList[i][0];
                    string coordinateString = parameterStringList[i].Remove(0, 1);
                    Enums.Parameter enumChanger;
                    firstLetterSuccess = Enum.TryParse(firstLetter.ToString(), out enumChanger);

                    double coordinate;
                    bool coordinateSuccess = false;
                    coordinateSuccess = double.TryParse(coordinateString, out coordinate);
                    if (firstLetterSuccess && coordinateSuccess)
                    {
                        parameterList.Add(new KeyValuePair<Enums.Parameter, double>(enumChanger, coordinate));
                    }
                    else
                    {
                        Console.WriteLine($"Working on it...");
                    }
                }

            }
            return parameterList;
        }

        /// <summary>
        /// Calculates the time of a full GCode file
        /// </summary>
        /// <param name="parsedFile"></param>
        /// <returns></returns>
        public double CalculateTime(Calculations defaultPrinter, List<KeyValuePair<Enums.Movement, List<KeyValuePair<Enums.Parameter, double>>>> gCode)
        {
            double totalTimeInSeconds = 0;
            for (int i = 0; i < gCode.Count; i++)
            {
                ParseCoordinates(gCode[i].Value);
                List<double> coordinatesList = new List<double>() { XCoordinate, YCoordinate, ZCoordinate,
                    LastXCoordinate, LastYCoordinate, LastZCoordinate };

                switch (gCode[i].Key)
                {
                    case Enums.Movement.G0:
                    case Enums.Movement.G1:
                        ParseCoordinates(gCode[i].Value);
                        totalTimeInSeconds += defaultPrinter.CalculateLineTime(coordinatesList, ExtrudeLength);
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
            Math.Round(totalTimeInSeconds);
            return totalTimeInSeconds;
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
                        double tempSqrtHolder = Math.Sqrt(parameterList[i].Value) / 60; // GCode measures feedrate in mm/min^2; /60 turns it into mm/s^s
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




