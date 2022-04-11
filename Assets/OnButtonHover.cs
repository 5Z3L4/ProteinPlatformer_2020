using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public Image imgToMove;
    private Vector2 _startPos = new Vector2(-7, 10);
    private Vector2 _endPos = new Vector2(0, 0);
    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
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

    public void OnSelect(BaseEventData eventData)
    {
        imgToMove.rectTransform.anchoredPosition = _endPos;
    }
    
    private void TaskOnClick()
    {
        imgToMove.rectTransform.anchoredPosition = _startPos;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        imgToMove.rectTransform.anchoredPosition = _startPos;
    }
}
