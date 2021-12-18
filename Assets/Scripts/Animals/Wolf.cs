using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Animals;
using Common;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Wolf : Animal
{
    private DesiredVelocityProvider _desiredVelocityProvider;
    private bool _hasTarget = false;
    private Transform _target;
    private float _secondsWithoutTarget;

    [SerializeField, Range(15, 90)]
    private float canLiveWithoutTargetSeconds = 30;

    public Wolf()
    {
        _desiredVelocityProvider = new WanderVelocityProvider(this);
    }

    private void Update()
    {
        if (_secondsWithoutTarget > canLiveWithoutTargetSeconds) 
            Destroy(this);
        ApplySteeringForce();
        ApplyForces();
    }

    protected override Vector3 GetDesiredVelocity()
    {
        if (!_hasTarget)
        {
            var creatures = GetSeenCreatures(new HashSet<string>(){"Wolf"});
            if (creatures.Any())
            {
                _desiredVelocityProvider = new SeekVelocityProvider(this);
                _target = creatures.GetRandomElement();
                IsRunning = true;
                _hasTarget = true;
            }
            else if (!creatures.Any() && IsRunning)
            {
                _secondsWithoutTarget += Time.deltaTime;
                _desiredVelocityProvider = new WanderVelocityProvider(this);
                IsRunning = false;
            }
        }
        else
        {
            if (_target is null) _hasTarget = false;
        }

        var desiredVelocity = _desiredVelocityProvider.GetDesiredVelocity(new[] { _target });
        if (!GetSeenObstacles(Velocity, out var obstacles)) 
            return desiredVelocity;
        var avoidanceVelocity = GetObstacleAvoidanceVelocity(obstacles);
        desiredVelocity = (desiredVelocity + avoidanceVelocity * 2) / 2;

        return desiredVelocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == _target.gameObject)
            Debug.Log($"{name} hit {other.gameObject.name}");
    }
}
