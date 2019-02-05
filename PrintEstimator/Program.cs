using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Remove Defaults once speeds and accelerations can be modified  or set automatically in-program

            #region Default speeds and accelerations
            Calculations DefaultPrinter = new Calculations();
            DefaultPrinter.Acceleration = 250; // mm/sec squared
            DefaultPrinter.RetractAcceleration = 25; // mm/sec squared
            DefaultPrinter.XSpeed = 40; // mm/s
            DefaultPrinter.YSpeed = 40; // mm/s
            DefaultPrinter.ZSpeed = 5; // mm/s
            #endregion

            Console.WriteLine($"Acceleration: {DefaultPrinter.Acceleration} \n" +
                              $"Retract Acceleration: {DefaultPrinter.RetractAcceleration} \n" +
                              $"X Speed: {DefaultPrinter.XSpeed} \n" +
                              $"Y Speed: {DefaultPrinter.YSpeed} \n" +
                              $"Z Speed: {DefaultPrinter.ZSpeed} \n" +
                              $"Are these settings correct? y/n");
            char correctSettings = Console.ReadKey().KeyChar;
            switch (correctSettings)
            {
                case 'n': 
                    Console.WriteLine("can't change these yet");
                    break;
                case 'y':
                default:
                    Console.WriteLine("Enter the local filepath of your G Code: ");
                    string filePath = Console.ReadLine();

                    GCodeReader NewFile = new GCodeReader();
                    List<List<string>> parsedFile = NewFile.FileParser(filePath);

                    GCodeLogic GCode = new GCodeLogic();
                    var EncodedFile = GCode.CreateMovementList(parsedFile);
                    GCode.CalculateTime(EncodedFile);
                    break;
            }
        }
    }
}
