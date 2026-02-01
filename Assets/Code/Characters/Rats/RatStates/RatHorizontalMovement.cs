using System;
using Systems.LayerClassGenerator;
using Systems.TagClassGenerator;
using UnityEngine;

namespace Code.Characters.Rats.RatStates
{
    public class RatHorizontalMovement : RatState
    {
        protected bool _goingLeft = false;

        public RatHorizontalMovement(params object[] args) : base(args)
        {
        }

        public override void Tick(float deltaTime)
        {
            RB.AddForce((_goingLeft ? Vector3.forward : Vector3.back) * (MovementForce * deltaTime), ForceMode.Force);
        }

        public override void OnCollision(Collision collider)
        {
            _goingLeft = !_goingLeft;
        }
    }
}
