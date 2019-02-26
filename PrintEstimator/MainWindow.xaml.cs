using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintEstimator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            Calculations DefaultPrinter = new Calculations();
            DefaultPrinter.Acceleration = Double.Parse(TbxAccelerationSpeed.ToString()) ; // mm/sec squared
            DefaultPrinter.RetractAcceleration = Double.Parse(TbxRetractAcceleration.ToString()); // mm/sec squared
            DefaultPrinter.XSpeed = Double.Parse(TbxXSpeed.ToString()); // mm/s
            DefaultPrinter.YSpeed = Double.Parse(TbxYSpeed.ToString()); // mm/s
            DefaultPrinter.ZSpeed = Double.Parse(TbxZSpeed.ToString()); // mm/s

            string filePath = Console.ReadLine();

            GCodeReader NewFile = new GCodeReader();
            List<List<string>> parsedFile = NewFile.FileParser(filePath);

            GCodeLogic GCode = new GCodeLogic();
            var EncodedFile = GCode.CreateMovementList(parsedFile);
            var totalTimeInSeconds = GCode.CalculateTime(DefaultPrinter, EncodedFile);
            var totalTimeInMinutes = totalTimeInSeconds / 60;
            var totalTimeInHours = totalTimeInMinutes / 60;
            var totalTimeInDays = totalTimeInHours / 24;


            TxtOutput.Text = $"Total in Seconds: {totalTimeInSeconds}\n" +
                             $"Total in Minutes: {totalTimeInMinutes}\n" +
                             $"Total in Hours:   {totalTimeInHours}\n" +
                             $"Total in Days:    {totalTimeInDays}";
        }
    }
}
