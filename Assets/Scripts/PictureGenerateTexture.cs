using UnityEngine;

[ExecuteInEditMode]
public class PictureGenerateTexture : MonoBehaviour
{
    [SerializeField] private Color firstColor, secondColor;
    [SerializeField] private Vector2 textureSize;
    [SerializeField] private Renderer renderer;

    private void Update()
    {
        renderer.material.mainTexture = GenerateTexture((int)textureSize.x, (int)textureSize.y);
    }

    private Texture2D GenerateTexture(int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        tex.filterMode = FilterMode.Point;

        for (int y = 0; y < height; y += 1)
        {
            for (int x = 0; x < width; x += 1)
            {
                Color pixelColor = firstColor;
                if ((x + y) % 2 == 0) pixelColor = secondColor;

                tex.SetPixel(x, y, pixelColor);
            }
        }

        tex.Apply();
        return tex;
    }
}
