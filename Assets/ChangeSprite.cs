using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    public Sprite newImg;
    public InterlocutorDialogue lastInterlocutorDialogue;
    public PlayerResponses lastPlayerResponse;
    public bool shoudlOverrideValues = false;
    public bool changeInterlocutorImg = false;
    public float width;
    public float height;
    public float posX;
    public float posY;
    public Image playerImgToChange;
    public Image interlocutorImgToChange;

    private bool _shouldChangeSprite = true;
    private void Start()
    {
        if (shoudlOverrideValues)
        {
            width = playerImgToChange.rectTransform.sizeDelta.x;
            height = playerImgToChange.rectTransform.sizeDelta.y;
            posX = playerImgToChange.rectTransform.anchoredPosition.x;
            posY = playerImgToChange.rectTransform.anchoredPosition.y;
        }
    }
    void Update()
    {
        if (changeInterlocutorImg)
        {
            ChangeImg(interlocutorImgToChange);
        }
        else
        {
            ChangeImg(playerImgToChange);
        }
    }
    private void ChangeImg(Image imgToChange)
    {
        if (_shouldChangeSprite)
        {
            if (imgToChange != null)
            {
                if (lastInterlocutorDialogue != null && lastInterlocutorDialogue.isOver)
                {
                    imgToChange.sprite = newImg;
                    imgToChange.rectTransform.sizeDelta = new Vector2(width, height);
                    imgToChange.rectTransform.anchoredPosition = new Vector2(posX, posY);
                    _shouldChangeSprite = false;
                }
                if (lastPlayerResponse != null && lastPlayerResponse.isOver)
                {
                    imgToChange.sprite = newImg;
                    imgToChange.rectTransform.sizeDelta = new Vector2(width, height);
                    imgToChange.rectTransform.anchoredPosition = new Vector2(posX, posY);
                    _shouldChangeSprite = false;
                }
            }
        }
    }
}
