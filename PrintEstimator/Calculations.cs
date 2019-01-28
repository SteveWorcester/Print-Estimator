using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintEstimator
{
    class Calculations
    {
        /// <summary>
        /// Returns the time to accelerate from beginning speed to max speed; can also be used for deceleration
        /// </summary>
        /// <param name="acceleration"></param>
        /// <param name="beginningSpeed"></param>
        /// <param name="maxSpeed"></param>
        /// <returns></returns>
        public double AccelerationTime(double acceleration, double beginningSpeed, double maxSpeed)
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
        public double AccelerationDistance(double acceleration, double beginningSpeed, double maxSpeed)
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
        public double MaxSpeedTravelTime(double maxSpeed, double distanceInMilimeters)
        {
            return distanceInMilimeters / maxSpeed;
        }
    }
}
