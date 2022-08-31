using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float intensity;
    [SerializeField]
    Image image;
    void Start()
    {
        image.material.SetFloat("_Intensity", 50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
