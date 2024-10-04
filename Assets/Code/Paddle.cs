using UnityEngine;

namespace GA.GArkanoid
{
  public class Paddle : MonoBehaviour
  {
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private Transform _startPoint;

    private IMover _mover;
    private Inputs _inputs;

    #region Unity messages
    private void Awake()
    {
      _mover = GetComponent<IMover>();
      _inputs = new Inputs();
    }

    private void OnEnable()
    {
      _inputs.Game.Enable();

      GameManager.CurrentBall = CreateBall();
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

    #endregion

    #region Internal functionality

    private Ball CreateBall()
    {
      // Quaternion.identity is zero rotation
      Ball ball = Instantiate(_ballPrefab, _startPoint.position, Quaternion.identity, _startPoint);
      return ball;
    }

    #endregion
  }
}