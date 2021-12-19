using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class CohesionVelocityProvider : DesiredVelocityProvider
    {
        public CohesionVelocityProvider(Animal animal) : base(animal)
        {
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets = null)
        {
            var pos = Animal.transform.position;
            var summarizedVector = new Vector3();
            var count = 0;
            foreach (var target in targets)
            {
                var distance = Vector3.Distance(pos, target.position);
                if (!(distance > 0)) 
                    continue;
                
                summarizedVector += target.position - Animal.transform.position;
                count++;
            }

            if (count <= 0) 
                return Vector3.zero;
            
            summarizedVector /= count;
            return summarizedVector.normalized * Animal.WanderVelocityLimit;
        }
    }
}