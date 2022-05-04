using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class OnButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
{
    public Image imgToMove;
    private Vector2 _startPos = new Vector2(-7, 10);
    private Vector2 _endPos = new Vector2(0, 0);
    private Button _button;
    private bool _shouldChangeColor = false;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    private void Start()
    {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        if (!_button.interactable)
        {
            _shouldChangeColor = true;
        }
    }
    private void Update()
    {
        if (_shouldChangeColor)
        {
            imgToMove.color = new Color32(60, 60, 60, 255);
            imgToMove.GetComponentInChildren<TMP_Text>().color = new Color32(145,145,145,255);
            _shouldChangeColor = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            imgToMove.rectTransform.anchoredPosition = _endPos;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            imgToMove.rectTransform.anchoredPosition = _startPos;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_button.interactable)
        {
            imgToMove.rectTransform.anchoredPosition = _startPos;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (_button.interactable)
        {
            imgToMove.rectTransform.anchoredPosition = _endPos;
        }
    }
    
    private void TaskOnClick()
    {
        if (_button.interactable)
        {
            imgToMove.rectTransform.anchoredPosition = _startPos;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (_button.interactable)
        {
            imgToMove.rectTransform.anchoredPosition = _startPos;
        }
    }
}
