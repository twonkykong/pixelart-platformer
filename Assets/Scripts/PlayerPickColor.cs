using UnityEngine;

public class PlayerPickColor : MonoBehaviour
{
    private InputMaster _inputMaster;

    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private float sphereCastRadius = 4f;
    [SerializeField] private RectTransform buttonObject;
    [SerializeField] private LayerMask layerMask;

    private Transform _thisObjectTransform;
    private GameObject _selectedColorObject;

    private void Awake()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Actions.Use.performed += _ => PickColor();

        _thisObjectTransform = transform;
    }

    private void Update()
    {
        CheckColorObject();
    }

    private void CheckColorObject()
    {
        Collider[] colliders = Physics.OverlapSphere(_thisObjectTransform.position, sphereCastRadius, layerMask);
        if (colliders.Length == 0)
        {
            buttonObject.gameObject.SetActive(false);
            return;
        }

        _selectedColorObject = colliders[0].gameObject;
        buttonObject.gameObject.SetActive(true);

        Vector3 dir = _selectedColorObject.transform.position - _thisObjectTransform.position;
        Vector2 buttonObjectPos = Camera.main.WorldToScreenPoint(_selectedColorObject.transform.position - (dir * 0.2f) + (Vector3.forward * 0.5f));

        buttonObject.position = buttonObjectPos;
    }

    private void PickColor()
    {
        if (_selectedColorObject == null) return;

        ColorObject colorObject = _selectedColorObject.GetComponent<ColorObject>();
        playerInventory.AddColor(colorObject.ColorId, colorObject.Color);
        buttonObject.gameObject.SetActive(false);
        Destroy(_selectedColorObject);
    }

    private void OnEnable()
    {
        _inputMaster.Enable();
    }

    private void OnDisable()
    {
        _inputMaster.Actions.Use.performed -= _ => PickColor();
        _inputMaster.Disable();
    }
}
