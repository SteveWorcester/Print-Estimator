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
        public double FSpeed { get; set; } // max feedrate

        public double CalculateDistanceBetweenPoints(double xCoordinate, double yCoordinate, double zCoordinate, double oldXCoordinate, double oldYCoordinate, double oldZCoordinate)
        {
            double zDistance = Math.Abs(zCoordinate - oldZCoordinate);
            double xDistance = Math.Abs(xCoordinate - oldXCoordinate);
            double yDistance = Math.Abs(yCoordinate - oldYCoordinate);

            double sqXDistance = xDistance * xDistance;
            double sqYDistance = yDistance * yDistance;
            double triHypotenuse = Math.Sqrt(sqXDistance + sqYDistance);

            if (zDistance != 0)
            {
                double sqTriHypotenuse = triHypotenuse * triHypotenuse;
                double sqZDistance = zDistance * zDistance;
                return Math.Abs(sqTriHypotenuse + sqZDistance);
            }
            else
            {
                return triHypotenuse;
            }
        }
        /// <summary>
        /// Returns the time to accelerate from beginning speed to max speed; can also be used for deceleration
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="beginningSpeed"></param>
        /// <param name="maxSpeed"></param>
        /// <returns></returns>
        public double CalculateAccelerationTime(List<KeyValuePair<Enums.Parameter, double>> parameterList)
        {
            var startFromSpeed = maxSpeed - beginningSpeed;
            var time = startFromSpeed / acceleration;
            return time;
        }
        /// <summary>
        /// Returns the distance it takes to reach max speed; can also be used for deceleration
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="beginningSpeed"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public double CalculateAccelerationDistance(double acceleration, double beginningSpeed, double maxSpeed)
        {
            var time = AccelerationTime(acceleration, beginningSpeed, maxSpeed);
            if (time < 0)
            {
                time *= -1;
            }
            var startingVelocityTime = beginningSpeed * time;
            var squareTime = time * time;
            var accelFromStart = acceleration * .5 * squareTime;
            var totalDistance = startingVelocityTime + accelFromStart;
            return totalDistance;
        }
        /// <summary>
        /// Returns the time it takes to travel while going at max speed
        /// </summary>
        /// <param name="maxSpeed"></param>
        /// <param name="distanceInMilimeters"></param>
        /// <returns></returns>
        public double CalculateMaxSpeedTravelTime(double maxSpeed, double distanceInMillimeters, double extrudeLength)
        {
            double travelTime = distanceInMillimeters / maxSpeed;
            double extrudeSpeed = extrudeLength / travelTime;
            return 0;
        }
    }
}
