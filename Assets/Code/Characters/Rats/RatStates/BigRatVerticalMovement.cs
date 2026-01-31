using Systems.TagClassGenerator;
using UnityEngine;

namespace Assets.Code.Characters.Rats.RatStates
{
    public class BigRatVerticalMovement : RatState
    {
        private IStatable.MovementAxis _otherState;

        public BigRatVerticalMovement(params object[] args) : base(args)
        {
            _otherState = (IStatable.MovementAxis)args[4];
        }

        public override void Tick(float deltaTime)
        {
            RB.AddForce(Vector3.up * MovementForce * deltaTime, ForceMode.Force);
        }

        public override void OnCollision(Collision collider)
        {
        }

        public override void OnCollisionExit(Collision collider)
        {
            if (collider.collider.CompareTag(Tags.Wall))
                StateManager.SetState(_otherState);
        }
    }
}
