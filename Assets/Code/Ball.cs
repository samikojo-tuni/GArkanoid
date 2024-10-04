using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float _speed = 5;
        private Inputs _inputs;
        private Transform _transform;
        private Vector2 _velocity = Vector2.zero;

        // Start is called before the first frame update
        // Use this for initialization
        void Start()
        {
            _inputs = new Inputs();
            _inputs.Game.Enable();
            _transform = transform;
        }

        // Update is called once per frame
        // Implement game logic here
        void Update()
        {
            if (_inputs.Game.Launch.WasPerformedThisFrame())
            {
                _velocity = (Random.insideUnitCircle + Vector2.up * 1.5f).normalized;
            }

            _transform.position += new Vector3(_velocity.x, _velocity.y, 0) * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Wall wall = other.GetComponent<Wall>();
            if (wall == null)
            {
                // Did not collide with wall, exit.
                return;
            }

            // Was the wall hazard? If so, destroy the ball!
            if (wall.IsHazard)
            {
                Destroy(gameObject);
                GameManager.Lives--;
                return;
            }

            Vector2 normal = wall.Normal;

            Vector2 u = Vector2.Dot(_velocity, normal) * normal;
            Vector2 w = _velocity - u;
            _velocity = w - u;
        }
    }
}