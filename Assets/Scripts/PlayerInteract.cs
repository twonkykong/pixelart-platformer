using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private CharacterController _characterController;
    private PushButton _lastPushButton;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private bool IsGrounded() => _characterController.isGrounded;

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            if (_lastPushButton != null)
            {
                _lastPushButton.Deactivate();
                _lastPushButton = null;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        PushButton pushButton = hit.gameObject.GetComponentInParent<PushButton>();
        if (pushButton != null)
        {
            if (_lastPushButton != pushButton)
            {
                _lastPushButton = pushButton;
                _lastPushButton.Activate();
            }
        }
    }
}
