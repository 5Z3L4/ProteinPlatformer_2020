using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResponseHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    private TMP_Text text;
    private Color32 outlineColor;
    private float outlineWidth;
    [SerializeField] private Color32 highlightOutlineColor;
    [SerializeField] private float highlightWidth;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        text.outlineColor = new Color32(60, 60, 60, 255);
        outlineColor = text.outlineColor;
        text.outlineWidth = highlightWidth;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        text.outlineColor = highlightOutlineColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        text.outlineColor = outlineColor;
    }

    public void OnSelect(BaseEventData eventData)
    {
        text.outlineColor = highlightOutlineColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        text.outlineColor = outlineColor;
    }
}
