using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public List<AudioSource> listAudio = new List<AudioSource>();
    public Sprite spOn;
    public Sprite spOff;
    private bool soundEnabled=true;

    public void Change()
    {
        foreach (AudioSource audio in listAudio)
        {
            if (soundEnabled) { audio.volume = 0f; }
            else { audio.volume = 100f; }
        }
        GetComponent<Image>().sprite = soundEnabled ? spOff : spOn;
        soundEnabled = !soundEnabled;
    }
}