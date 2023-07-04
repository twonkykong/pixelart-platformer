using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PixelNumbersSpawner : MonoBehaviour
{
    [SerializeField] private PaletteGenerator paletteGenerator;
    [SerializeField] private PictureGrid pictureGrid;

    [SerializeField] private Transform numbersParent;
    [SerializeField] private Transform pictureTransform;
    [SerializeField] private GameObject numberPrefab;

    private Texture2D _pictureTexture;

    public void Init(Texture2D pictureTexture)
    {
        _pictureTexture = pictureTexture;
        SpawnNumbers();
    }

    public void SpawnNumbers()
    {
        List<Color> colorPalette = paletteGenerator.ColorPalette;
        Color[] pixels = _pictureTexture.GetPixels();

        for (int y = 0; y < _pictureTexture.height; y+=1)
        {
            for (int x = 0; x < _pictureTexture.width; x+=1)
            {
                Vector2 pixelCoord = new Vector2(x, y);
                GameObject newNumberObject = Instantiate(numberPrefab, numbersParent);
                newNumberObject.transform.position = pictureGrid.CalculatePosition(pixelCoord) - pictureTransform.forward * 0.2f;

                int pixelIndex = (_pictureTexture.width * y) + x;
                Color currentColor = pixels[pixelIndex];

                string colorIndex = colorPalette.IndexOf(currentColor).ToString();
                if (colorIndex == "-1") colorIndex = "*";
                newNumberObject.GetComponent<TextMeshPro>().text = colorIndex.ToString();
            }
        }
    }
}
