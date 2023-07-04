using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private InputMaster _inputMaster;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioClip footstepAudioClip;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Actions.Jump.performed += _ => PlayJumpSound();
    }

    private void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpAudioClip);
    }

    public void PlayFootstepSound()
    {
        audioSource.PlayOneShot(footstepAudioClip);
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Actions.Jump.performed -= _ => PlayJumpSound();
        _inputMaster.Disable();
    }
}
