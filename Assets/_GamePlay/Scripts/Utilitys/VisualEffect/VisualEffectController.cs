using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VisualEffectController : MonoBehaviour
{
    [SerializeField]
    private VisualEffect effect;

    public void SetColor(Color color)
    {     
        effect.SetVector4("HitColor", color);
    }


    public void Play()
    {
        //effect.SetVector4()
        gameObject.SetActive(true);
        effect.Play();
    }

    public void Stop()
    {
        effect.Stop();
        gameObject.SetActive(false);
    }
}
