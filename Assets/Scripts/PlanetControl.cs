using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetControl : MonoBehaviour 
{
    [SerializeField]
    AllManager AM;

    [SerializeField]
    float speed;

    void OnMouseDown()
    {
        AM.Planet.RotateChanges(speed, true);
    }

    void OnMouseUp()
    {
        AM.Planet.RotateChanges(0f, false);
    }
        
}
