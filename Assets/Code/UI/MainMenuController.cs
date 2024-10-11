using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Unity's scene management tools

namespace GA.GArkanoid.UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void StartNewGame()
        {
            GameManager.ChangeState(State.StateType.Level);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
