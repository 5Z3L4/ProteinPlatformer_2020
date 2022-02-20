using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<PlatformFalling> _platforms;
    void Start()
    {
        StartCoroutine(LateStart(0.2f));
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetPlatforms();
    }

    [ContextMenu("Do Something")]
    public void RespawnPlatforms()
    {
        _platforms.ForEach(x => x.RespawnPlatform());
    }

    private void GetPlatforms()
    {
        _platforms = FindObjectsOfType<PlatformFalling>().ToList();
    }
}
