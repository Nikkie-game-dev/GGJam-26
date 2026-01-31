

using Systems.TagClassGenerator;
using UnityEngine;

namespace Code.Characters.Rats.RatStates
{
    public class BigRatHorizontalMovement : RatHorizontalMovement
    {
        IStatable.MovementAxis _otherState;

        bool _canMove = false;

        public BigRatHorizontalMovement(params object[] args) : base(args)
        {
            _otherState = (IStatable.MovementAxis)args[4];
        }

        public override void Tick(float deltaTime)
        {
            RB.AddForce((_goingLeft ? Vector3.left : Vector3.right) * ((_canMove ? MovementForce : MovementForce * 0.25f) * deltaTime), ForceMode.Force);
        }

        public override void OnCollision(Collision collision)
        {
            if (collision.collider.CompareTag(Tags.Ground))
                _canMove = true;

            if (collision.collider.CompareTag(Tags.Wall))
            {
                StateManager.SetState(_otherState);
                _canMove = false;
            }
        }
    }
}
