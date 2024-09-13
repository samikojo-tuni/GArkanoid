using UnityEngine;

namespace GA.GArkanoid
{
  public class Paddle : MonoBehaviour
  {
    private IMover _mover;
    private Inputs _inputs;

    private void Awake()
    {
      _mover = GetComponent<IMover>();
      _inputs = new Inputs();
    }

    private void OnEnable()
    {
      _inputs.Game.Enable();
    }

    private void OnDisable()
    {
      _inputs.Game.Disable();
    }

    private void Update()
    {
      // Read the input 
      float input = _inputs.Game.Move.ReadValue<float>();
      _mover.Move(new Vector2(input, 0));
    }
  }
}