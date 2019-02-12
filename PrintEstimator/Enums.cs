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
            //TODO: Fill Movement; is currently only partial - close to complete?
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
            M103 // Stop Extruders - may not be used?
        }

        public enum Parameter
        {
            X, // X coordinate
            Y, // Y coordinate
            Z, // Z coordinate
            F, // Feedrate - The maximum movement rate between the start and end point. The feedrate set here applies to subsequent moves that omit this parameter.
            E, // Extrude this amount of filament to get the x,y coordinate
            S, // ?
            C, // Cycles?
            P, // ?
            T  // Temp?
        }
    }
}
