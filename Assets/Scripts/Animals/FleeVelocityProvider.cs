using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class FleeVelocityProvider : DesiredVelocityProvider
    {
        public FleeVelocityProvider(Animal animal) : base(animal)
        {
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets)
        {
            if (!targets.Any()) return Vector3.zero;
            var vectorSummarized = new Vector3();
            vectorSummarized = targets.Aggregate(vectorSummarized, (current, transform) => current + transform.position);
            var desiredVelocity = vectorSummarized / targets.Length;
            return -(desiredVelocity.normalized * Animal.RunVelocityLimit);
        }
    }
}