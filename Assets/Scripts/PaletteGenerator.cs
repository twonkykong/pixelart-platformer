using System.Collections.Generic;
using UnityEngine;

public class PaletteGenerator : MonoBehaviour
{
    [SerializeField] private ColorObjectSpawner colorObjectSpawner;
    [SerializeField] private List<Color> colorPalette;

    private Texture2D _pictureTexture;

    public List<Color> ColorPalette
    {
        get { return colorPalette; }
    }

    public void Init(Texture2D tex)
    {
        _pictureTexture = tex;
        GenerateColorPalette();
        colorObjectSpawner.SpawnColorObjects();
    }

    private void GenerateColorPalette()
    {
        for (int x = 0; x < _pictureTexture.width; x+=1)
        {
            for (int y = 0; y < _pictureTexture.height; y+=1)
            {
                Color currentColor = _pictureTexture.GetPixel(x, y);
                if (!colorPalette.Contains(currentColor))
                {
                    colorPalette.Add(currentColor);
                }
            }
        }
    }
}
