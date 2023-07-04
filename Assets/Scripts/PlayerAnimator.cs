using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerAnimator : MonoBehaviour
{
    private InputMaster _inputMaster;
    private InputAction _moveInput;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform groudCheckRayPoint;
    [SerializeField] private Transform spineTransform;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _moveInput = _inputMaster.Actions.Move;
    }

    private void Update()
    {
        SetAnimatorState();
    }

    private void SetAnimatorState()
    {
        bool isWalking = _moveInput.ReadValue<Vector2>() != Vector2.zero;
        bool isGrounded = Physics.Raycast(groudCheckRayPoint.position, Vector3.down, 1f);

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("isWalking", isWalking);
    }

    public void DrawAnimation()
    {
        spineTransform.DOKill();
        spineTransform.DOLocalMoveY(0, 0.05f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            spineTransform.DOLocalMoveY(0.4192617f, 0.1f);
        });
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
