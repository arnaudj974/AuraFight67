using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int playerIndex;
    private Coroutine corout;
    public Score score;
    Animator anim;
    public AudioSource audioBooing;
    public AudioSource audio67;
    public bool pvp;
    public bool CoroutStarted{ get; private set; }
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("multSpeed", score.mult);
    }

    public void StartPlayer(float delai)
    {
        corout = StartCoroutine(LaunchPlayer(delai));
    }

    public void StopPlayer()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchPlayer(float delai) //ajoute un 0 à la listMult si le joueur n'input pas dans le delai imparti
    {
        CoroutStarted = true;
        yield return new WaitForSeconds(delai);
        AddScore(0, 0);
        CoroutStarted = false;
    }

    public void AddScore(int points,int _mult)
    {
        if (_mult == 0) { PlaySound(audioBooing, audio67); }
        score.AddtoMult(_mult);
        score.AddScore(points);
        score.ShowScore();
        if (score.mult == 5) { PlaySound(audio67, audioBooing); }
    }
    public void PlaySound(AudioSource audioPlay, AudioSource audioStop)
    {
        if (pvp)
        {
            if (!audioPlay.isPlaying && !audioStop.isPlaying) { audioPlay.Play(); }
        }
        else
        {
            if (audioStop.isPlaying) { audioStop.Stop(); }
            if (!audioPlay.isPlaying) { audioPlay.Play(); }
        }
    }
    public void Clear()
    {
        audio67.Stop();
        audioBooing.Stop();
        score.Clear();
    }

}
