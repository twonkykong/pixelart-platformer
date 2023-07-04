using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onActivate;
    [SerializeField] private UnityEvent onDeactivate;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonOn;
    [SerializeField] private AudioClip buttonOff;

    public void Activate()
    {
        animator.SetBool("isActivated", true);
        onActivate?.Invoke();

        audioSource.PlayOneShot(buttonOn);
    }

    public void Deactivate()
    {
        onDeactivate?.Invoke();
        animator.SetBool("isActivated", false);

        audioSource.PlayOneShot(buttonOff);
    }
}
