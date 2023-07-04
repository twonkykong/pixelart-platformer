using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<Color> colorList;

    [SerializeField] private PlayerDrawing playerDrawing;

    [SerializeField] private RawImage currentColorPreview;
    [SerializeField] private TextMeshProUGUI currentId;

    [SerializeField] private RectTransform buttonParent;
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private float oneRowHeight = 105f;

    public void SetCurrentColor(GameObject buttonObject)
    {
        Color newColor = buttonObject.GetComponentInChildren<RawImage>().color;
        string newId = buttonObject.GetComponentInChildren<TextMeshProUGUI>().text;

        playerDrawing.SetCurrentColor(newColor);
        currentColorPreview.color = newColor;
        currentId.text = newId;
    }

    public void AddColor(string id, Color newColor)
    {
        GameObject newButtonObject = Instantiate(buttonPrefab, buttonParent);
        newButtonObject.SetActive(true);

        newButtonObject.GetComponentInChildren<TextMeshProUGUI>().text = id;
        newButtonObject.GetComponentInChildren<RawImage>().color = newColor;

        colorList.Add(newColor);

        if (colorList.Count % 3 == 0)
        {
            buttonParent.sizeDelta += Vector2.up * oneRowHeight;
        }
    }
}
