using Code.Characters.Rats.RatStates;
using System.Collections;
using Systems.TagClassGenerator;
using UnityEngine;

namespace Assets.Code.Characters.Rats.RatStates
{
    public class RatFallState : RatState
    {
        BoxCollider collider;

        RaycastHit hit;

        public RatFallState(params object[] args) : base(args)
        {
            collider = (BoxCollider)args[3];
        }

        public override void OnCollision(Collision collider)
        {
        }

        public override void Tick(float deltaTime)
        {
            if (Physics.Raycast(RB.transform.position, Vector3.down, out hit, collider.size.x * 2))
                StateManager.SetState(IStatable.MovementAxis.Horizontal);
            else if (Physics.Raycast(RB.transform.position, Vector3.down * RB.linearVelocity.x, out hit, collider.size.x * 2))
                StateManager.SetState(IStatable.MovementAxis.Horizontal);
        }
    }
}