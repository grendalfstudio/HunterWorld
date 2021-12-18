using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class SeparateVelocityProvider : DesiredVelocityProvider
    {
        public SeparateVelocityProvider(Animal animal) : base(animal)
        {
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets = null)
        {
            var desiredSeparation = Animal.Collider2D switch
            {
                CapsuleCollider2D capsule => capsule.size.y,
                CircleCollider2D circle => circle.radius * 2,
                _ => Animal.transform.localScale.x * 2
            };

            var pos = Animal.transform.position;
            var summarizedVector = new Vector3();
            var count = 0;
            foreach (var target in targets)
            {
                var distance = Vector3.Distance(pos, target.position);
                if (!(distance > 0) || !(distance < desiredSeparation)) 
                    continue;
                
                var diff = pos - target.position;
                diff = diff.normalized / distance;
                summarizedVector += diff;
                count++;
            }

            if (count > 0)
                return (summarizedVector / count).normalized * Animal.WanderVelocityLimit;
            
            return Vector3.zero;
        }
    }
}