using UnityEngine;
public interface IInputService
{
    bool GetKey(KeyCode key);
    bool GetKeyDown(KeyCode key);
    float GetDeltaTime();
}