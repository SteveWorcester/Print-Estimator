using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{

    class GCodeLogic
    {
        public enum Movement
        {
            //TODO: Fill Movement
            G0, // linear move w/ wait
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

        public void GCodeSelector(int x, int y, int z, int s, int c, Movement gCode)
        {
            //TODO: fill switch

            // Only GCodes used for estimating time are listed. All other GCodes will default-break to fetch next GCode.
            switch (gCode)
            {
                case Movement.G0:
                    //linear move
                    break;
                case Movement.G1:
                    // linear move
                    break;
                case Movement.G2:
                    //clockwise arc
                    break;
                case Movement.G3:
                    //counterclockwise arc
                    break;
                default:
                    break;
            }
        } 
    }
}
