using UnityEngine;
using UnityEngine.UI;

public class GameStartButtonHandler : MonoBehaviour
{
    public Button m_StartButton;

     void Start()
    {
        m_StartButton.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("Start button pressed, starting game...");
        // could probably be handled by EventBus but this is simple enough and will probably do for now
        GameManager.Instance.SetState(GameManager.GameState.Playing);
    }
}
