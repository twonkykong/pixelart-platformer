using UnityEngine;

public class CursorState : MonoBehaviour
{
    private void Awake()
    {
        SetCursorLockState(true);
        SetCursorVisibleState(false);
    }

    public void SetCursorLockState(bool value)
    {
        if (value) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }

    public void SetCursorVisibleState(bool value)
    {
        Cursor.visible = value;
    }
}
