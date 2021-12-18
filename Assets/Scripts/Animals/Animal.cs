using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Game;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public abstract class Animal : MonoBehaviour
    {
        private const float Epsilon = 0.01f;
        private Vector3 _acceleration;
        private Vector3 _velocity;

        protected bool IsRunning = false;
        protected int RaysToCast = 36;

        [SerializeField]
        private float mass = 1;

        [SerializeField, Range(1, 20)]
        private float wanderVelocityLimit = 1;

        [SerializeField, Range(1, 40)] 
        private float runVelocityLimit = 2;

        [SerializeField, Range(1, 20)]
        private float steeringForceLimit = 1;

        [SerializeField] 
        private float wanderPointDistance = 2;

        [SerializeField]
        private float circleRadius = 1;

        [SerializeField, Range(1, 75)]
        public int wanderRotateAngle = 10;

        [SerializeField, Range(1, 10)]
        private float viewRadius = 2;

        [SerializeField]
        private Collider2D collider2d;
    
        public float WanderVelocityLimit => wanderVelocityLimit;

        public float RunVelocityLimit => runVelocityLimit;

        public float WanderPointDistance => wanderPointDistance;

        public Vector3 Velocity => _velocity;

        public int WanderRotateAngle => wanderRotateAngle;

        public float ViewRadius => viewRadius;

        public Collider2D Collider2D => collider2d;

        protected void ApplySteeringForce()
        {
            var desiredVelocity = GetDesiredVelocity();
            var steeringForce = desiredVelocity - _velocity;
            ApplyForce(steeringForce.normalized * steeringForceLimit);
        }

        protected abstract Vector3 GetDesiredVelocity();

        protected void ApplyForce(Vector3 force)
        {
            var acceleration = force / mass;
            _acceleration += acceleration;
        }

        protected void ApplyForces()
        {
            _velocity += _acceleration * Time.deltaTime;
            _velocity = Vector3.ClampMagnitude(_velocity, IsRunning ? runVelocityLimit : wanderVelocityLimit);
            if (_velocity.magnitude < Epsilon)
            {
                _velocity = Vector3.zero;
                return;
            }

            transform.position += _velocity * Time.deltaTime;
            _acceleration = Vector3.zero;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, _velocity);
        }

        protected bool GetSeenObstacles(Vector2 direction, out Transform[] walls)
        {
            var angle = -90F;
            var vector = Velocity.normalized;           
            var raycastHits = new LinkedList<Transform>();
            
            for (int i = 0; i < 12; i++)
            {
                var rotate = Quaternion.Euler(0, 0, angle);
                vector = rotate * vector;
                Collider2D.enabled = false;
                var hit = Physics2D.Raycast(transform.position, vector, ViewRadius);
                Collider2D.enabled = true;
                Debug.DrawRay(transform.position, direction, Color.blue, 1);
                if (hit.collider != null && hit.transform.gameObject.tag.Equals("Obstacle") && !raycastHits.Contains(hit.transform))
                {
                    raycastHits.AddLast(hit.transform);
                }

                angle += 15;
            }

            walls = raycastHits.ToArray();
            return raycastHits.Any();
        }

        protected Vector3 GetObstacleAvoidanceVelocity(Transform[] obstacles)
        {
            var summarizedVectors = obstacles
                .Select(w => Velocity - w.position)
                .Aggregate(Vector3.zero, (current, vector) => 
                    new Vector3(current.x + vector.x, current.y + vector.y));
                
            var desiredVelocity = -((Velocity - new Vector3(summarizedVectors.x/obstacles.Length, summarizedVectors.y/obstacles.Length)).normalized * WanderVelocityLimit);
            return desiredVelocity;
        }
    }
}