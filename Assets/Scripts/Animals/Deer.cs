using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class Deer : Animal
    {
        private readonly SeparateVelocityProvider _separateVelocityProvider;
        private readonly CohesionVelocityProvider _cohesionVelocityProvider;
        private readonly AlignmentVelocityProvider _alignmentVelocityProvider;
        private DesiredVelocityProvider _desiredVelocityProvider;

        [SerializeField, Range(2, 10)]
        private float dangerSenseDistance = 5;
        
        [SerializeField, Range(0.5F, 5)]
        private float cohesionWeight = 1;
        
        [SerializeField, Range(0.5F, 5)]
        private float separationWeight = 1;
        
        [SerializeField, Range(0.5F, 5)]
        private float alignmentWeight = 1;

        public Deer()
        {
            _desiredVelocityProvider = new WanderVelocityProvider(this);
            _separateVelocityProvider = new SeparateVelocityProvider(this);
            _cohesionVelocityProvider = new CohesionVelocityProvider(this);
            _alignmentVelocityProvider = new AlignmentVelocityProvider(this);
        }

        protected override void ApplySteeringForce()
        {
            var desiredVelocity = GetDesiredVelocity();
            var steeringForce = desiredVelocity - Velocity;
            var flockForces = GetFlockForces();
            ApplyForce(((steeringForce + flockForces)/2).normalized * SteeringForceLimit);
        }

        private Vector3 GetFlockForces()
        {
            var deers = GetSeenCreatures(new HashSet<string>() { "Hare", "Wolf", "Player" });
            var cohesionForce = _cohesionVelocityProvider.GetDesiredVelocity(deers) - Velocity;
            var separationForce = _separateVelocityProvider.GetDesiredVelocity(deers) - Velocity;
            var alignmentForce = _alignmentVelocityProvider.GetDesiredVelocity(deers) - Velocity;

            var flockForces = (
                cohesionForce * cohesionWeight + 
                separationForce * separationWeight +
                alignmentForce * alignmentWeight) / 3;
            
            return flockForces;
        }

        protected override Vector3 GetDesiredVelocity()
        {
            var creatures = GetSeenCreatures(new HashSet<string>(){"Hare", "Deer"}, dangerSenseDistance);
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
            Debug.DrawRay(transform.position, desiredVelocity, Color.yellow);
            desiredVelocity = (desiredVelocity + avoidanceVelocity * avoidanceWeight) / 2;
            Debug.DrawRay(transform.position, desiredVelocity, Color.red);

            return desiredVelocity;
        }
    }
}