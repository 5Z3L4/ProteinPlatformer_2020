using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    //przyszlosciowe pod animacje
    public bool isClosed = false;
    // Start is called before the first frame update
    public void OpenDoors()
    {
        isClosed = true;
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
