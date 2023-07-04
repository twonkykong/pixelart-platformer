using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField] private string colorId;
    [SerializeField] private Color color;

    private Renderer _renderer;

    public string ColorId
    {
        get { return colorId; }
    }

    public Color Color
    {
        get { return color; }
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void Init(int id, Color newColor)
    {
        colorId = id.ToString();
        color = newColor;

        _renderer.material.color = color;
    }
}
