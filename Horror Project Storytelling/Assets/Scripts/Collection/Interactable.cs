using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual void Awake()
    {
        gameObject.layer = 6; // layer of the interactable 
    }
    public abstract void OnInteract();

    public abstract void OnFocus();

    public abstract void OnLoseFocus();
}


// Tutorial Used: https://www.youtube.com/watch?v=AQc-NM2Up3M&list=PLfhbBaEcybmgidDH3RX_qzFM0mIxWJa21