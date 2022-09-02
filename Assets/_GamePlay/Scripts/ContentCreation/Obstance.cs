using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstance : MonoBehaviour
{
    [SerializeField]
    GameObject opaqueObject;
    
    public void SetTransparent(bool value)
    {
        opaqueObject.SetActive(!value);
    }
}
