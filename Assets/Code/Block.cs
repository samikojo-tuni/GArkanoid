using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private int _score = 1;

        public int Score { get { return _score; } }

        // TODO: Implement breaking blocks and adding score!
        // TODO: Use Health for the block!

        private void Start()
        {
        }

        public void Break()
        {
            // TODO: Destroying blocks may not be the best idea...
            Destroy(gameObject);
            GameManager.Score += _score;
        }
    }
}
