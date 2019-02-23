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
            G1 = 0, // linear move
            G92, // Set Position
        }

        public enum Parameter
        {
            X, // X coordinate
            Y, // Y coordinate
            Z, // Z coordinate
            F, // Feedrate - The maximum movement rate between the start and end point. The feedrate set here applies to subsequent moves that omit this parameter.
            E, // Extrude this amount of filament to get the x,y coordinate
        }
    }
}
