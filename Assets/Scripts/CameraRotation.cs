using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _lookInput;

    [SerializeField] private Transform horizontalRotationTransform;
    [SerializeField] private Transform verticalRotationTransform;

    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float smoothRotationSpeed = 0.5f;

    private bool _canLook = true;
    [SerializeField] private bool _flag;

    [SerializeField] private float _horizontalLook;
    [SerializeField] private float _verticalLook;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _lookInput = _inputMaster.Actions.Look;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        if (!_canLook) return;
        if (_flag)
        {
            _flag = false;
            return;
        }

        Vector2 lookInput = _lookInput.ReadValue<Vector2>() * sensitivity;

        _horizontalLook += lookInput.x;
        if (_horizontalLook > 360) _horizontalLook -= 360;
        else if (_horizontalLook <= 0) _horizontalLook += 360;

        _verticalLook -= lookInput.y;
        _verticalLook = Mathf.Clamp(_verticalLook, -90, 90);

        RotateSmoothly(horizontalRotationTransform, new Vector3(0, _horizontalLook, 0), smoothRotationSpeed);
        RotateSmoothly(verticalRotationTransform, new Vector3(_verticalLook, 0, 0), smoothRotationSpeed);
    }

    private void RotateSmoothly(Transform obj, Vector3 nextEulerAngles, float time)
    {
        Quaternion currentRotation = obj.localRotation;
        Quaternion nextRotation = Quaternion.Euler(nextEulerAngles);

        obj.localRotation = Quaternion.Slerp(currentRotation, nextRotation, time);
    }

    public void EnableRotation(bool value)
    {
        _canLook = value;
        if (!value) _flag = true;
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _lookInput.performed -= _ => RotateCamera();
        _inputMaster.Disable();
    }
}
