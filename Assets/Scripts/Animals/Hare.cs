using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class Hare : Animal
    {
        private DesiredVelocityProvider _desiredVelocityProvider;

        public Hare()
        {
            _desiredVelocityProvider = new WanderVelocityProvider(this);
        }

        protected override Vector3 GetDesiredVelocity()
        {
            var creatures = GetSeenCreatures(new HashSet<string>());
            if (creatures.Any() && !IsRunning)
            {
                _desiredVelocityProvider = new FleeVelocityProvider(this);
                IsRunning = true;
            }
            else if (!creatures.Any() && IsRunning)
            {
                _desiredVelocityProvider = new WanderVelocityProvider(this);
                IsRunning = false;
            }

            var desiredVelocity = _desiredVelocityProvider.GetDesiredVelocity(creatures.ToArray());
            if (!GetSeenObstacles(Velocity, out var obstacles)) 
                return desiredVelocity;
            var avoidanceVelocity = GetObstacleAvoidanceVelocity(obstacles);
            desiredVelocity = (desiredVelocity + avoidanceVelocity * 2) / 2;

            return desiredVelocity;
        }
    }
}


