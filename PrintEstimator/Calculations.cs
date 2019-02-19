using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class Calculations
    {
        public double Acceleration { get; set; }
        public double XSpeed { get; set; }
        public double YSpeed { get; set; }
        public double ZSpeed { get; set; }
        public double RetractAcceleration { get; set; }
        public double RetractSpeed { get; set; }
        public double FSpeed { get; set; } // max feedrate

        /// <summary>
        /// Calculates the time it takes to travel from old to new coordinates.
        /// </summary>
        /// <param name="coordinatesList"></param>
        /// <param name="extrudeLength"></param>
        /// <returns>Time in seconds</returns>
        public double CalculateLineTime(List<double> coordinatesList, double extrudeLength)
        {
            double totalLineTime = 0;
            double distance = CalculateDistanceBetweenPoints(coordinatesList);
            
            // Tweak this after estimate tests. Also tweak CalulateMaxSpeedTravelTime
            // estimator: the shorter the distance, the more likely the hotend will not be at full stop (curves). 
            // the longer the distance, the more likely this is building a full layer with straight lines.
            double accelerationDistance;
            if (extrudeLength < .01 && extrudeLength > 0)
            {
                accelerationDistance = CalculateAccelerationDistance(Acceleration, XSpeed, XSpeed);
                totalLineTime += CalculateAccelerationTime(Acceleration, XSpeed, XSpeed);
            }
            else if (extrudeLength < .05 && extrudeLength > 0)
            {
                accelerationDistance = CalculateAccelerationDistance(Acceleration, XSpeed/2, XSpeed);
                totalLineTime += CalculateAccelerationTime(Acceleration, XSpeed/2, XSpeed);
            }
            else if (extrudeLength < 0)
            {
                accelerationDistance = CalculateAccelerationDistance(Acceleration, 0, XSpeed); // 0 because all retractions stop before move; start from 0 speed.
                totalLineTime += CalculateAccelerationTime(RetractAcceleration, 0, RetractSpeed);

            }
            else
            {
                accelerationDistance = CalculateAccelerationDistance(Acceleration, 0, XSpeed); // 0 because longer lines usually stop before move
                totalLineTime += CalculateAccelerationTime(Acceleration, 0, XSpeed);
            }

            if (accelerationDistance > distance)
            {
                accelerationDistance = distance;
            }


            double MaxSpeedTravelTime = CalculateMaxSpeedTravelTime(XSpeed, accelerationDistance, distance);


            return MaxSpeedTravelTime;
        }

        private double CalculateDistanceBetweenPoints(List<double> coordinatesList)
        {
            
            double xDistance = Math.Abs(coordinatesList[0] - coordinatesList[3]); // new X minus last X
            double yDistance = Math.Abs(coordinatesList[1] - coordinatesList[4]); // new Y minus last Y
            double zDistance = Math.Abs(coordinatesList[2] - coordinatesList[5]); // new Z minus last Z

            double sqXDistance = xDistance * xDistance;
            double sqYDistance = yDistance * yDistance;
            double triHypotenuse = Math.Sqrt(sqXDistance + sqYDistance);

            if (zDistance != 0)
            {
                double sqTriHypotenuse = triHypotenuse * triHypotenuse;
                double sqZDistance = zDistance * zDistance;
                return Math.Sqrt(Math.Abs(sqTriHypotenuse + sqZDistance)); // this *should* only return z axis moves in practice because the Z axis should move by itself when printing; however, this math is in here in case of special print settings.
            }
            else
            {
                return triHypotenuse;
            }
        }

        /// <summary>
        /// Returns the distance it takes to reach max speed; can also be used for deceleration
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="beginningSpeed"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private double CalculateAccelerationDistance(double acceleration, double beginningSpeed, double maxSpeed)
        {
            var time = Math.Abs(CalculateAccelerationTime(acceleration, beginningSpeed, maxSpeed));
            var startingVelocityTime = beginningSpeed * time;
            var squareTime = time * time;
            var accelFromStart = acceleration * .5 * squareTime;
            var totalDistance = startingVelocityTime + accelFromStart;
            return totalDistance;
        }

        /// <summary>
        /// Returns the time to accelerate from beginning speed to max speed; can also be used for deceleration
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="beginningSpeed"></param>
        /// <param name="maxSpeed"></param>
        /// <returns></returns>
        private double CalculateAccelerationTime(double acceleration, double beginningSpeed, double maxSpeed)
        {
            var startFromSpeed = maxSpeed - beginningSpeed;
            var time = startFromSpeed / acceleration;
            return time;
        }

        /// <summary>
        /// Returns the time it takes to travel while going at max speed
        /// </summary>
        /// <param name="maxSpeed"></param>
        /// <param name="distanceInMilimeters"></param>
        /// <returns></returns>
        private double CalculateMaxSpeedTravelTime(double maxSpeed, double accelerationDistance, double totalDistance)
        {
            double maxSpeedTravelDistance = totalDistance - accelerationDistance;
            return maxSpeedTravelDistance / maxSpeed;
        }
    }
}
