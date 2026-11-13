using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Bulle bulle;
    private Coroutine corout;
    public Score score;
    Animator anim;
    public AudioSource audioBooing;
    public AudioSource audio67;
    public bool pvp;
    public bool coroutStarted { get; private set; }
    int index;

    void Awake()
    {
        bulle = FindAnyObjectByType<Bulle>();
        index = GetComponent<Controller>().GetIndex();
    }
    private void OnEnable()
    {
        bulle.OnBulleFind += CheckInputResponse;
        bulle.OnBulleStart += StartPlayer;
    }
    private void OnDisable()
    {
        bulle.OnBulleFind -= CheckInputResponse;
        bulle.OnBulleStart -= StartPlayer;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        pvp = GameManager.Instance.mode == Mode.PvP;
    }

    public void ChangeSpeed()
    {
        anim.SetFloat("multSpeed", score.mult);
    }

    public void StartPlayer(float delai)
    {
        corout = StartCoroutine(LaunchPlayer(delai));
    }

    public void StopPlayer()
    {
        if (coroutStarted)
        {
            StopCoroutine(corout);
        }
    }

    public IEnumerator LaunchPlayer(float delai) 
    {
        coroutStarted = true;
        yield return new WaitForSeconds(delai);
        AddScore(0, 0); //si le player n'appuie sur rien
        coroutStarted = false;
    }

    public void AddScore(int points,int _mult)
    {
        if (_mult == 0) { PlaySound(audioBooing, audio67); }
        score.UpdateScore(points, _mult);
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

    public void CheckInputResponse(int index,bool trouve)
    {
        if (this.index == index)
        {
            if (trouve)
            {
                StopPlayer();
                AddScore(GameManager.points, 1);
            }
            else
            {
                StopPlayer();
                AddScore(0, 0);
            }
        }
    }
}
