using UnityEngine;

public class UnityInputService : IInputService
{
    public bool GetKey(KeyCode key) => Input.GetKey(key);
    public bool GetKeyDown(KeyCode key) => Input.GetKeyDown(key);
    public float GetDeltaTime() => Time.deltaTime;
}
