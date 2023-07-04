using UnityEngine;

public class PictureInitializer : MonoBehaviour
{
    [SerializeField] private PictureGrid pictureGrid;
    [SerializeField] private PaletteGenerator paletteGenerator;

    [Header("either texture or size (leave texture empty)")]
    [SerializeField] private Texture2D pictureTexture;
    [SerializeField] private Vector2 textureSize;

    [Header("")]
    [SerializeField] private PixelNumbersSpawner pixelNumberSpawner;
    [SerializeField] private Picture picture;
    [SerializeField] private PictureHint pictureHint;
    [SerializeField] private Renderer pictureRenderer;
    [SerializeField] private Renderer finalTexturePreview;

    private void Start()
    {
        InitAll();
    }

    private void InitAll()
    {
        if (pictureTexture == null)
        {
            pictureTexture = new Texture2D((int)textureSize.x, (int)textureSize.y);
            pictureTexture.filterMode = FilterMode.Point;
        }
        else paletteGenerator.Init(pictureTexture);

        pictureGrid.Init(pictureTexture);
        pixelNumberSpawner.Init(pictureTexture);
        picture.Init(pictureTexture);
        pictureHint.Init(pictureTexture);

        if (finalTexturePreview != null)
        {
            finalTexturePreview.material.mainTexture = pictureTexture;
        }

        Texture2D tex = new Texture2D(pictureTexture.width, pictureTexture.height);
        tex.filterMode = FilterMode.Point;
        pictureRenderer.material.mainTexture = tex;
    }
}
