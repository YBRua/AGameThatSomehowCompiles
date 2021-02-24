using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapZoom : MonoBehaviour
{
    public GameObject MinimapUI;
    private GameObject miniCamera;
    private int zoomState;

    private void Start()
    {
        zoomState = 0;
    }

    
}
