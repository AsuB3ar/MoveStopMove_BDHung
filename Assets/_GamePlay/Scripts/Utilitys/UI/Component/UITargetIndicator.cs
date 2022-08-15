using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITargetIndicator : MonoBehaviour
{
    public const float WIDTH = 100;
    public const float HEIGHT = 100;
    [SerializeField]
    TMP_Text textLevel;
    [SerializeField]
    Image image;


    public void SetLevel(int level)
    {
        textLevel.text = level.ToString();
    }

    public void SetColor(Color color)
    {
        image.color = color;
    }
}
