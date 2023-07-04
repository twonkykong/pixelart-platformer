using UnityEngine;
using UnityEngine.Events;

public class Picture : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    [SerializeField] private UnityEvent OnDoneDrawing;

    private Texture2D _finalTexture;

    public void Init(Texture2D pictureTexture) => _finalTexture = pictureTexture;

    public void Draw(Vector2 textureCoord, Color color)
    {
        Texture2D tex = (Texture2D)renderer.material.mainTexture;

        textureCoord.x *= tex.width;
        textureCoord.y *= tex.height;

        tex.SetPixel((int)textureCoord.x, (int)textureCoord.y, color);
        tex.Apply();

        renderer.material.mainTexture = tex;

        if (TextureComparisonUtility.CompareTextures(tex, _finalTexture, out _))
        {
            OnDoneDrawing?.Invoke();
            Destroy(this);
        }
    }
}
