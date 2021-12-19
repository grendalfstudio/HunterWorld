using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Animals;
using Assets.Scripts.Game;
using Common;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class Wolf : Animal
    {
        private DesiredVelocityProvider _desiredVelocityProvider;
        private bool _hasTarget = false;
        private Transform _target;
        private float _secondsWithoutTarget;
        private bool _caughtTarget = false;
        private float _waitingTime = 0;

        [SerializeField, Range(15, 90)] private float canLiveWithoutTargetSeconds = 30;

        [SerializeField, Range(1, 5)] private float arrivalRange = 2;

        [SerializeField, Range(2, 15)] private float chaseRange = 8;

        public Wolf()
        {
            _desiredVelocityProvider = new WanderVelocityProvider(this);
        }

        private void Update()
        {
            if (_target == null)
                _hasTarget = false;
            if (_secondsWithoutTarget > canLiveWithoutTargetSeconds)
                transform.root.gameObject.GetComponent<AnimalsController>().KillTheAnimal(this.gameObject);
            if (_caughtTarget && _waitingTime < 2)
            {
                _waitingTime += Time.deltaTime;
                return;
            }
            ApplySteeringForce();
            ApplyForces();
        }

        protected override Vector3 GetDesiredVelocity()
        {
            if (!_hasTarget)
            {
                _secondsWithoutTarget += Time.deltaTime;
                var creatures = GetSeenCreatures(new HashSet<string>() { "Wolf" });
                if (creatures.Any())
                {
                    _desiredVelocityProvider = new SeekVelocityProvider(this, arrivalRange);
                    _target = creatures.GetRandomElement();
                    IsRunning = true;
                    _hasTarget = true;
                }
                else if (!creatures.Any() && IsRunning)
                {
                    _desiredVelocityProvider = new WanderVelocityProvider(this);
                    IsRunning = false;
                }
            }

            //wolf dies not of starvation, but of feeling himself useless,
            //so if he at least found the target, set the counter to zero
            _secondsWithoutTarget = 0;
            if (_hasTarget && Vector3.Distance(transform.position, _target.position) > chaseRange)
                _hasTarget = false;
            var desiredVelocity = _desiredVelocityProvider.GetDesiredVelocity(new[] { _target });
            if (!GetSeenObstacles(Velocity, out var obstacles))
                return desiredVelocity;
            var avoidanceVelocity = GetObstacleAvoidanceVelocity(obstacles);
            Debug.DrawRay(transform.position, desiredVelocity, Color.yellow);
            desiredVelocity = (desiredVelocity + avoidanceVelocity * avoidanceWeight) / 2;
            Debug.DrawRay(transform.position, desiredVelocity, Color.red);

            return desiredVelocity;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (_hasTarget && col.gameObject == _target.gameObject)
            {
                _caughtTarget = true;
                _waitingTime = 0;
            }
                
        }
    }
}
