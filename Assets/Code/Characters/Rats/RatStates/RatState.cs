using UnityEngine;

namespace Assets.Code.Characters.Rats.RatStates
{
    public abstract class RatState
    {
        private IStatable _statable;
        private Rigidbody _rb;
        private BoxCollider _collider;
        private float _movementForce;


        protected IStatable StateManager => _statable;
        protected Rigidbody RB => _rb;
        protected BoxCollider BoxCollider => _collider;
        protected float MovementForce => _movementForce;

        public RatState(params object[] args)
        {
            _statable = (IStatable)args[0];
            _rb = (Rigidbody)args[1];
            _collider = (BoxCollider)args[2];
            _movementForce = (float)args[3];
        }

        public abstract void Tick(float deltaTime);

        public abstract void OnCollision(Collision collider);

        public virtual void OnCollisionExit(Collision collider)
        {
        }
    }
}
