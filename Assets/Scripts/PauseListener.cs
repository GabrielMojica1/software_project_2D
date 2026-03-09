using UnityEngine;

public class PauseListener : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.State == GameManager.GameState.Playing)
            {
                GameManager.Instance.PauseGame();
            }
            else if (GameManager.Instance.State == GameManager.GameState.Paused)
            {
                GameManager.Instance.ResumeGame();
            }
        }
    }
}
