using UnityEngine;
using UnityEngine.UI; // use the old UI system

public class LivesUI : MonoBehaviour
{
    public static LivesUI instance;
    public Text livesText; // replace TextMeshProUGUI with Text

    void Awake()
    {
        instance = this;
    }

    public void UpdateLives(int lives)
    {
        livesText.text = "Lives: " + lives;
    }
}