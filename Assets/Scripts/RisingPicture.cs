using UnityEngine;
using DG.Tweening;

public class RisingPicture : MonoBehaviour
{
    [SerializeField] private PictureGrid pictureGrid;
    [SerializeField] private Renderer pictureRenderer;
    [SerializeField] private RisingPixelData[] risingPixelsData;
    [SerializeField] private GameObject risingPixelPrefab;
    [SerializeField] private Transform risingPixelParent;

    [SerializeField] private Renderer debugLastTexture, debugCurrentTexture;

    [SerializeField] private Texture2D _lastTexture;
    [SerializeField] private Texture2D _currentTexture;


    public void RisePixels()
    {
        if (_lastTexture == null)
        {
            _currentTexture = (Texture2D)pictureRenderer.material.mainTexture;
            _lastTexture = new Texture2D(_currentTexture.width, _currentTexture.height);
            _lastTexture.filterMode = FilterMode.Point;
        }

        if (TextureComparisonUtility.CompareTextures(_currentTexture, _lastTexture, out _)) return;

        Color[] currentPixels = _currentTexture.GetPixels();
        Color[] lastPixels = _lastTexture.GetPixels();

        float ratio = pictureGrid.CalculateRatio();

        for (int i = 0; i < currentPixels.Length; i += 1)
        {
            if (currentPixels[i] == lastPixels[i]) continue;

            foreach (RisingPixelData risingPixelData in risingPixelsData)
            {
                if (risingPixelData.PixelColor != currentPixels[i]) continue;

                int x = i % _currentTexture.width;
                int y = i / _currentTexture.width;
                Vector2 pixelCoord = new Vector2(x, y);

                Transform newRisingPixel = Instantiate(risingPixelPrefab, risingPixelParent).transform;
                newRisingPixel.localScale = new Vector3(ratio, 0, ratio);
                newRisingPixel.position = pictureGrid.CalculatePosition(pixelCoord);

                newRisingPixel.GetComponent<Renderer>().material.color = currentPixels[i];

                //dotween
                float duration = risingPixelData.Duration;
                newRisingPixel.DOScaleY(risingPixelData.Height, duration);
                newRisingPixel.DOLocalMoveY(risingPixelData.Height/2, duration);
            }
        }

        _lastTexture.SetPixels(currentPixels);
        _lastTexture.Apply();
    }
}
