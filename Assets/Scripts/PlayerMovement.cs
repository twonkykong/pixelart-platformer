using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _moveInput;
    private InputAction _jumpInput;

    [SerializeField] private Transform cameraCenter;
    [SerializeField] private Transform movingPlatformPoint;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float smoothTurnSpeed = 0.25f;
    [SerializeField] private float groundNormalAngleLimit = 45f;
    [SerializeField] private float gravity = 15f;

    private CharacterController _characterController;
    private Transform _thisObjectTransform;

    private float _velocity;

    private bool IsJumping() => _jumpInput.phase == InputActionPhase.Performed;
    private bool IsGrounded() => _characterController.isGrounded;
    
    private void Awake()
    {
        _inputMaster = new InputMaster();
        _moveInput = _inputMaster.Actions.Move;
        _jumpInput = _inputMaster.Actions.Jump;

        _characterController = GetComponent<CharacterController>();
        _thisObjectTransform = transform;
    }

    private void FixedUpdate()
    {
        Move();
        ApplyGravity();
    }

    private void Move()
    {
        Vector3 inputDir = _moveInput.ReadValue<Vector2>().normalized * moveSpeed;
        Vector3 moveDir = cameraCenter.right * inputDir.x + cameraCenter.forward * inputDir.y;

        float angle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
        Quaternion currentRotation = _thisObjectTransform.rotation;
        Quaternion nextRotation = Quaternion.Euler(new Vector3(currentRotation.x, angle, currentRotation.z));

        if (moveDir != Vector3.zero) _thisObjectTransform.rotation = Quaternion.Slerp(_thisObjectTransform.rotation, nextRotation, smoothTurnSpeed);

        if (IsGrounded())
        {
            if (IsJumping())
            {
                _velocity += jumpForce;
            }
        }
        else movingPlatformPoint.SetParent(null);

        moveDir.y = _velocity;

        _characterController.Move(moveDir * Time.deltaTime + CalculateAdditionalVelocity());
    }

    private void ApplyGravity()
    {
        if (IsGrounded()) _velocity = -1f;
        else
        {
            _velocity -= gravity * Time.deltaTime;
        }
    }

    private Vector3 _lastPlatformPosition;

    private Vector3 CalculateAdditionalVelocity()
    {
        if (movingPlatformPoint.parent == null) return Vector3.zero;

        Vector3 lastPos = _lastPlatformPosition;
        _lastPlatformPosition = movingPlatformPoint.position;
        return movingPlatformPoint.position - lastPos;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        float angle = Vector3.Angle(hit.normal, Vector3.up);
        if (angle > groundNormalAngleLimit) return;

        if (hit.collider.TryGetComponent(out Animator _))
        {
            movingPlatformPoint.position = hit.point;

            if (hit.transform != movingPlatformPoint.parent)
            {
                movingPlatformPoint.SetParent(hit.transform);
                _lastPlatformPosition = movingPlatformPoint.position;
            }
        }
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Disable();
    }
}
