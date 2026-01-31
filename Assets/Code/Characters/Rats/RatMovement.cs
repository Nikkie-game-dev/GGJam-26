using Assets.Code.Characters.Rats.RatStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Code.Characters.Rats
{
    enum RatsStates
    {
        Verital,
        Orizontal
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public sealed class RatMovement : MonoBehaviour, IStatable
    {
        private Rigidbody _rb;
        private BoxCollider _collider;

        private RatState _currentState;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();

            _currentState = new RatHorizontalMovement(this, _rb, _collider, 400f);
        }

        private void Update()
        {
            _currentState.Tick(Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _currentState.OnCollision(collision);
        }

        public void SetState(IStatable.MovementAxis state)
        {
        }
    }
}
