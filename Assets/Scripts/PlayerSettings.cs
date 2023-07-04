using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private CameraRotation cameraRotation;
    [SerializeField] private PlayerDrawing playerDrawing;

    public void EnableMovement(bool value)
    {
        playerMovement.enabled = value;
    }

    public void EnableRotation(bool value)
    {
        cameraRotation.EnableRotation(value);
    }

    public void EnableDrawing(bool value)
    {
        playerDrawing.enabled = value;
    }
}
