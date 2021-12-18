using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class SeekVelocityProvider : DesiredVelocityProvider
    {
        public SeekVelocityProvider(Animal animal) : base(animal)
        {
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets = null)
        {
            var desiredVelocity = (targets![0].position - Animal.transform.position).normalized * Animal.RunVelocityLimit;
            return desiredVelocity;
        }
    }
}