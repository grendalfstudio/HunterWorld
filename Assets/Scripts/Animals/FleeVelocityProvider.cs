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
            var vectorSummarized = targets
                .Select(t => t.position - Animal.transform.position)
                .Aggregate(Vector3.zero, (current, vector) => 
                    new Vector3(current.x + vector.x, current.y + vector.y));

            var desiredVelocity = new Vector3(
                vectorSummarized.x / targets.Length,
                vectorSummarized.y / targets.Length);
            
            return -(desiredVelocity.normalized * Animal.RunVelocityLimit);
        }
    }
}