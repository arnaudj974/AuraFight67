using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    public Sprite spOn;
    public Sprite spOff;
    private bool soundEnabled=true;
    AudioSource[] audioSources;
    private void Start()
    {
        audioSources = FindObjectsByType<AudioSource>();
    }
    public void Change()
    {
        foreach (AudioSource audio in audioSources)
        {
            if (soundEnabled) { audio.volume = 0f; }
            else { audio.volume = 100f; }
        }
        GetComponent<Image>().sprite = soundEnabled ? spOff : spOn;
        soundEnabled = !soundEnabled;
    }
}