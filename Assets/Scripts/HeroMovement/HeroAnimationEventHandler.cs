using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroAnimationEventHandler : MonoBehaviour
{

    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }
    public void HeroStepSound()
    {
        _source.Stop();
        _source.Play();
    }
}
