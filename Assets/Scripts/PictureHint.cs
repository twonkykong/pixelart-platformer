using UnityEngine;

public class PictureHint : MonoBehaviour
{
    [SerializeField] private PictureGrid pictureGrid;
    [SerializeField] private Renderer pictureRenderer;

    [SerializeField] private Transform hintObjectTransform;
    [SerializeField] private Animation hintObjectAnimation;
    [SerializeField] private float hintObjectHeight = 1.5f;

    private Texture2D _finalTexture;

    public void Init(Texture2D pictureTexture) => _finalTexture = pictureTexture;
    
    public void ShowHint()
    {
        Texture2D currentTexture = (Texture2D)pictureRenderer.material.mainTexture;

        if (!TextureComparisonUtility.CompareTextures(currentTexture, _finalTexture, out Vector2 incorrectPixel, true))
        {
            if (incorrectPixel.x < Vector2.zero.x) return;
            hintObjectTransform.position = pictureGrid.CalculatePosition(incorrectPixel) + Vector3.up * hintObjectHeight;
            hintObjectAnimation.Play();
        }
    }
}
