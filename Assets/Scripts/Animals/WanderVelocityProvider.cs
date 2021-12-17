using UnityEngine;

namespace Assets.Scripts.Animals
{
    public class WanderVelocityProvider : DesiredVelocityProvider
    {
        private int _angle;

        public WanderVelocityProvider(Animal animal) : base(animal)
        {
        }

        public override Vector3 GetDesiredVelocity(Transform[] targets = null)
        {
            var rndValue = Random.value;
            if (rndValue < 0.5f)
                _angle += Animal.WanderRotateAngle;
            else if (rndValue < 1)
                _angle -= Animal.WanderRotateAngle;

            var position = Animal.transform.position;
            var futurePosition = position + Animal.Velocity.normalized * Animal.WanderPointDistance;
            var vector = new Vector3(Mathf.Cos(_angle * Mathf.Deg2Rad), Mathf.Sin(_angle * Mathf.Deg2Rad));
            var desiredVelocity = (futurePosition + vector - position).normalized * Animal.WanderVelocityLimit;

            return desiredVelocity;
        }
    }
}