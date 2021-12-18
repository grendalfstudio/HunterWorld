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

        // Update is called once per frame
        void Update()
        {
            ApplySteeringForce();
            ApplyForces();
        }

        protected override Vector3 GetDesiredVelocity()
        {
            var creatures = GetSeenCreatures();
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

        private IEnumerable<Transform> GetSeenCreatures()
        {
            var angle = 0F;
            var vector = Velocity.normalized;           
            var raycastHits = new LinkedList<Transform>();

            for (var i = 0; i < RaysToCast; i++)
            {
                foreach (var col in Collider2D)
                {
                    col.enabled = false;
                }
                var hit = Physics2D.Raycast(transform.position, vector, ViewRadius);
                //Debug.DrawRay(transform.position, vector * ViewRadius, Color.blue, 1);
                foreach (var col in Collider2D)
                {
                    col.enabled = true;
                }
                if (hit.collider != null && !hit.transform.gameObject.tag.Equals("Obstacle") && !raycastHits.Contains(hit.transform))
                {
                    raycastHits.AddLast(hit.transform);
                }
                angle += 360 / RaysToCast;
                var rotate = Quaternion.Euler(0, 0, angle);
                vector = rotate * vector;
            }

            return raycastHits;
        }
    }
}


