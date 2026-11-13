using System.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class Bot : MonoBehaviour
{
    Bulle bulle;
    private float tpsRep = 0.7f;
    private float chances = 2f;
    private Coroutine corout;
    public Score score;
    public Animator anim;
    public AnimatorController animCamp;
    public AnimatorController animInfi;
    public bool coroutStarted { get; private set; }

    void Awake()
    {
        bulle = FindAnyObjectByType<Bulle>();
        anim.runtimeAnimatorController = GameManager.Instance.mode == Mode.Infini ? animInfi : animCamp;

    }
    void OnEnable()
    {
        bulle.OnBulleStart += StartBot;
    }
    void OnDisable()
    {
        bulle.OnBulleStart -= StartBot;
    }
    public void ChangeSpeed()
    {
        anim.SetFloat("multSpeed", score.mult); //update de l'animation suivant la mult
    }

    public void StartBot(float delai)
    {
        corout = StartCoroutine(LaunchBot(delai));
    }

    public void StopBot()
    {
        if (coroutStarted)
        {
            StopCoroutine(corout);
        }
    }

    public IEnumerator LaunchBot(float delai)
    {
        coroutStarted = true;
        float tpsBot = Random.Range(0.1f, tpsRep);
        bool trouve = false;
        if (delai >= tpsBot)
        {
            yield return new WaitForSeconds(tpsBot);
            trouve = chances > Random.Range(0f, 4f);
        }
        if (trouve)
        {
            AddScore(GameManager.points, 1);
        }
        else
        {
            AddScore(0, 0);
        }
        coroutStarted = false;
    }

    public void NewBot(int lvl, float screenTime) //création du bot selon le level
    {
        if (GameManager.Instance.mode==Mode.Infini)
        {
            anim.SetInteger("lvl", Random.Range(1, 6));
        }
        else
        {
            anim.SetInteger("lvl", lvl);
        }
        tpsRep = screenTime;
        switch (lvl)
        {
            case 1: chances = 2.5f; break;
            case 2: chances = 3f; break;
            case 3: chances = 3.5f; break;
            case 4: chances = 4f; break;
            default: chances = 4f; break;
        }
    }
    public void AddScore(int points, int mult)
    {
        score.UpdateScore(points, mult);
    }
}
