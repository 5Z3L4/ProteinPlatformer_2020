using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image imgToMove;
    private Vector2 _startPos;
    private Vector2 _endPos;
    private void Start()
    {
        _startPos = new Vector2(0, 0);
        _endPos = new Vector2(-7, 10);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        imgToMove.rectTransform.anchoredPosition = _endPos;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        imgToMove.rectTransform.anchoredPosition = _startPos;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        imgToMove.rectTransform.anchoredPosition = _startPos;
    }
}
