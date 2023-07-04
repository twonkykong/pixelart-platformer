using UnityEngine;

public class TextureComparisonUtility
{
    public static bool CompareTextures(Texture2D texA, Texture2D texB, out Vector2 incorrectPixel, bool debug = false)
    {
        Color[] pixelsA = texA.GetPixels();
        Color[] pixelsB = texB.GetPixels();

        incorrectPixel = Vector2.one * -1;

        for (int i = 0; i < pixelsA.Length; i += 1)
        {
            if (pixelsA[i] != pixelsB[i])
            {
                if (debug)
                {
                    int column = i % texA.width;
                    int row = i / texA.width;

                    incorrectPixel = new Vector2(column, row);
                    Debug.Log("incorrect pixel: " + incorrectPixel);
                }

                return false;
            }
        }

        return true;
    }
}
