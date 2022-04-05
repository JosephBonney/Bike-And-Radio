using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.Radio;

public class PlayRadio : MonoBehaviour
{
    public RadioPlayer radio;

    public void Play()
    {
        radio.Play();
    }

    public void Stop()
    {
        radio.Stop();
    }
}
