using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    private InputMaster _inputMaster;

    [SerializeField] private UnityEvent onOpenColorMenu;
    [SerializeField] private UnityEvent onOpenMenu;

    private void Awake()
    {
        _inputMaster = new InputMaster();

        _inputMaster.Actions.OpenColorMenu.performed += _ => OpenColorMenu();
        _inputMaster.Actions.OpenMenu.performed += _ => OpenMenu();
    }

    private void OpenColorMenu()
    {
        onOpenColorMenu?.Invoke();
    }

    private void OpenMenu()
    {
        onOpenMenu?.Invoke();
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
