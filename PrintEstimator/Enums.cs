using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class Enums
    {
        public enum Movement
        {
            //TODO: Fill Movement; is currently only partial
            G0 = 0, // linear move w/ wait
            G1, // linear move
            G2, // Clockwise arc move
            G3, // Counterclockwise arc move
            G10, // Retraction
            G11, // Unretract
            G42, // Fast Move
            G92, // Set Position
            M0, // Stop
            M1, // Conditional Stop
            M2, // Program End
            M101, // Start Extruder - may not be used?
            M102, // Retract Extruder - may not be used?
            M103, // Stop Extruders - may not be used?
        }

        //public Movement ConvertToEnum(string command)
        //{
        //    if (command == "G0")
        //    {
        //        
        //    }
        //
        //}
        
}
}
