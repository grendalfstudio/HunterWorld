using System;
using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class AlignmentVelocityProvider : DesiredVelocityProvider
    {        
        public AlignmentVelocityProvider(Animal animal) : base(animal)
        {
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets = null)
        {
            var summarizedVector = new Vector3();
            var count = 0;
            foreach(var target in targets) {
                var distance = Vector3.Distance(Animal.transform.position,target.position);
                if ((!(distance > 0))) 
                    continue;
                var type = Animal.GetType();
                var component = target.gameObject.GetComponent(type);
                summarizedVector += ((Animal)component).Velocity;
                count++;
            }

            if (count <= 0)
                return Vector3.zero;
            
            summarizedVector = summarizedVector / count * Animal.WanderVelocityLimit;
            return summarizedVector;
        }
    }
}