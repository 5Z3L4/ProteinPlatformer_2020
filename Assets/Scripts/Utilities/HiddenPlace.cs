using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenPlace : MonoBehaviour
{
    Tilemap background;
    Color transparent = new Color(255,255,255,0);
    public bool isPlayerInside;
    private void Awake()
    {
        background = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayerInside) return;
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DoAThingOverTime(Color.white, transparent, 0.5f));
            isPlayerInside = true;
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        print("works");
        float alpha = background.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            background.color = newColor;
            yield return null;
        }
    }

    IEnumerator DoAThingOverTime(Color start, Color end, float duration)
    {
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            //right here, you can now use normalizedTime as the third parameter in any Lerp from start to end
            background.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        background.color = end; //without this, the value will end at something like 0.9992367
    }


    public void CheckIfPlayerIsInside()
    {
        if (!isPlayerInside) return;
        isPlayerInside = false;
        StartCoroutine(DoAThingOverTime(transparent, Color.white, 0.5f));
    }
}
