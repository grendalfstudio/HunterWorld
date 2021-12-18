using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Animals;
using Assets.Scripts.Game;
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

    [SerializeField, Range(1, 5)] 
    private float arrivalRange = 2;

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
        ApplySteeringForce();
        ApplyForces();
    }

    protected override Vector3 GetDesiredVelocity()
    {
        if (!_hasTarget)
        {
            _secondsWithoutTarget += Time.deltaTime;
            var creatures = GetSeenCreatures(new HashSet<string>(){"Wolf"});
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

        var desiredVelocity = _desiredVelocityProvider.GetDesiredVelocity(new[] { _target });
        if (!GetSeenObstacles(Velocity, out var obstacles)) 
            return desiredVelocity;
        var avoidanceVelocity = GetObstacleAvoidanceVelocity(obstacles);
        desiredVelocity = (desiredVelocity + avoidanceVelocity * 2) / 2;

        return desiredVelocity;
    }

    //not working
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(_hasTarget && col.gameObject == _target.gameObject)
            Debug.Log($"{name} hit {col.gameObject.name}");
    }

    //also not working
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(_hasTarget && col.gameObject == _target.gameObject)
            Debug.Log($"{name} hit {col.gameObject.name}");
    }
}
