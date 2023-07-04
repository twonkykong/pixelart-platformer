using UnityEngine;

public class PictureGrid : MonoBehaviour
{
    [SerializeField] private Transform pictureTransform;
    private Texture2D _pictureTexture;

    public void Init(Texture2D pictureTexture) => _pictureTexture = pictureTexture;
    
    public float CalculateRatio()
    {
        return pictureTransform.localScale.x / _pictureTexture.width;
    }

    public Vector3 CalculatePosition(Vector2 pixelCoord)
    {
        float ratio = CalculateRatio();

        Vector3 offset = pictureTransform.right * (pictureTransform.localScale.x / 2 - (ratio/2)) +
            pictureTransform.up * (pictureTransform.localScale.y / 2 - (ratio/2));

        Vector3 pixelCoord3D = pictureTransform.right * pixelCoord.x + pictureTransform.up * pixelCoord.y;

        return pictureTransform.position - offset + (pixelCoord3D * ratio);
    }
}
