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
        GameManager.Instance.StartGame();
    }
}
