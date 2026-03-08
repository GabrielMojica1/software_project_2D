using UnityEngine;

public class GameStartButtonHandler : MonoBehaviour
{
     public void OnButtonPressed()
    {
        Debug.Log("Start button pressed, starting game...");
        // could probably be handled by EventBus but this is simple enough and will probably do for now
        GameManager.Instance.SetState(GameManager.GameState.Playing);
    }
}
