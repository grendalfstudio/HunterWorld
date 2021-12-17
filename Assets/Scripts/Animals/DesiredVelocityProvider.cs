using UnityEngine;

namespace Assets.Scripts.Animals
{
    public abstract class DesiredVelocityProvider
    {
        protected Animal Animal;

        protected DesiredVelocityProvider(Animal animal)
        {
            Animal = animal;
        }

        public abstract Vector3 GetDesiredVelocity(Transform[] targets = null);
    }
}