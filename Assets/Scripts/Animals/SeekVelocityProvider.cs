using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class SeekVelocityProvider : DesiredVelocityProvider
    {
        private float _arrivalRange;
        public SeekVelocityProvider(Animal animal, float arrivalRange) : base(animal)
        {
            _arrivalRange = arrivalRange;
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets = null)
        {
            var desiredVelocity = targets![0].position - Animal.transform.position;
            var koeff = 1F;
            if (desiredVelocity.magnitude < _arrivalRange)
                koeff = desiredVelocity.magnitude / _arrivalRange;

            return desiredVelocity.normalized * Animal.RunVelocityLimit * koeff;
            
        }
    }
}