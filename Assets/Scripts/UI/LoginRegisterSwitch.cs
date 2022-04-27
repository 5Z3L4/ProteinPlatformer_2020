using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

public class LoginRegisterSwitch : MonoBehaviour, IPointerClickHandler
{
    public GameObject RegisterPanel; 
    public GameObject LoginPanel; 
    // add callbacks in the inspector like for buttons
    public UnityEvent onClick;
    public TMP_Text errorText;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        onClick.Invoke();
    }
    public void LoadPanel()
    {
        if (LoginPanel.activeInHierarchy)
        {
            LoginPanel.SetActive(false);
            RegisterPanel.SetActive(true);
        }
        else if (RegisterPanel.activeInHierarchy)
        {
            RegisterPanel.SetActive(false);
            LoginPanel.SetActive(true);
        }
        errorText.SetText(string.Empty);
    }
}
