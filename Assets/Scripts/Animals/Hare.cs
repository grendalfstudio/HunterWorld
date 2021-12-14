using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class Hare : MonoBehaviour
    {
        private const float Epsilon = 0.01f;
    
        private Vector3 _velocity;

        private Vector3 _acceleration;

        [SerializeField]
        private float _mass = 1;

        [SerializeField, Range(1, 20)]
        private float _velocityLimit = 1;

        [SerializeField, Range(1, 20)]
        private float _steeringForceLimit = 1;

        [SerializeField] 
        private float _circleDistance = 2;

        [SerializeField]
        private float _circleRadius = 1;

        [SerializeField, Range(1, 75)]
        private int _rotateAngle = 10;

        private int _angle;
    
        public float VelocityLimit => _velocityLimit;

        public void ApplyForce(Vector3 force)
        {
            var acceleration = force / _mass;
            _acceleration += acceleration;
        }

        // Update is called once per frame
        void Update()
        {
            ApplySteeringForce();
            ApplyForces();
        }

        private void ApplySteeringForce()
        {
            var desiredVelocity = GetDesiredVelocity();
            var steeringForce = desiredVelocity - _velocity;
            ApplyForce(steeringForce.normalized * _steeringForceLimit);
        }

        private Vector3 GetDesiredVelocity()
        {
            var rndValue = Random.value;
            if (rndValue < 0.5f)
                _angle += _rotateAngle;
            else if (rndValue < 1)
                _angle -= _rotateAngle;
            
            var futurePosition = transform.position + _velocity.normalized * _circleDistance;
            var vector = new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad), 0, Mathf.Sin(_angle * Mathf.Deg2Rad));
            return (futurePosition + vector - transform.position).normalized * _velocityLimit;
        }

        private void ApplyForces()
        {
            _velocity += _acceleration * Time.deltaTime;
            _velocity = Vector3.ClampMagnitude(_velocity, _velocityLimit);
            if (_velocity.magnitude < Epsilon)
            {
                _velocity = Vector3.zero;
                return;
            }

            transform.position = _velocity * Time.deltaTime;
            _acceleration = Vector3.zero;
        }
    }
}


