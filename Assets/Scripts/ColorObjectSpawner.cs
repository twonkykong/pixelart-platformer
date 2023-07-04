using System.Collections.Generic;
using UnityEngine;

public class ColorObjectSpawner : MonoBehaviour
{
    [SerializeField] private PaletteGenerator paletteGenerator;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject colorObjectPrefab;

    private List<Color> _spawnedColors = new List<Color>();

    public void SpawnColorObjects()
    {
        for (int i = 0; i < paletteGenerator.ColorPalette.Count; i+=1)
        {
            if (!_spawnedColors.Contains(paletteGenerator.ColorPalette[i]))
            {
                GameObject newColorObject = Instantiate(colorObjectPrefab, spawnPoints[i].position, Quaternion.identity);
                newColorObject.GetComponent<ColorObject>().Init(i, paletteGenerator.ColorPalette[i]);

                _spawnedColors.Add(paletteGenerator.ColorPalette[i]);
            }
        }
    }
}
