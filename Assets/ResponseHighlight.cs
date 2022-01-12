using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResponseHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text text;
    private Color32 outlineColor;
    private Color32 fontColor;
    private float outlineWidth;
    [SerializeField] private Color32 highlightOutlineColor;
    [SerializeField] private float highlightWidth;
    [SerializeField] private Color32 highlightedFontColor;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        outlineColor = text.outlineColor;
        fontColor = text.faceColor;
        outlineWidth = 0;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.faceColor = highlightedFontColor;
        text.outlineWidth = highlightWidth;
        text.outlineColor = highlightOutlineColor;
        print("chuj");
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.faceColor = fontColor;
        text.outlineColor = outlineColor;
        text.outlineWidth = outlineWidth;
        print("cipa");
    }

}
