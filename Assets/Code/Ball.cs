using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    private Rigidbody2D _rigidbody;
    private Inputs _inputs;

    // Start is called before the first frame update
    // Use this for initialization
    void Start()
    {
        // Works if the component is attached to the same GameObject this 
        // script is attached to.
        _rigidbody = GetComponent<Rigidbody2D>();

        _inputs = new Inputs();

        _inputs.Game.Enable();
        
    }

    // Update is called once per frame
    // Implement game logic here
    void Update()
    {
        if (_inputs.Game.Launch.WasPerformedThisFrame())
        {
            _rigidbody.AddForce((Vector2.up + Vector2.right).normalized * _speed, ForceMode2D.Impulse);
        }
    }
}
