using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Coroutine corout;
    public Score score;
    public Animator anim;
    public AudioSource audioBooing;
    public AudioSource audio67;
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
        yield return new WaitForSeconds(delai);
        AddScore(0, 0);
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
        if (audioStop.isPlaying) { audioStop.Stop(); }
        if (!audioPlay.isPlaying) { audioPlay.Play(); }
    }
}
