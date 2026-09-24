using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private float tpsRep = 0.9f;
    private float chances = 2f;
    private Coroutine corout;
    public Score score;
    public Animator anim;
    void Start()
    {

    }

    void Update()
    {
        anim.SetFloat("multSpeed", score.mult);
    }

    public void StartBot(float delai, int points)
    {
        corout = StartCoroutine(LaunchBot(delai, points));
    }

    public void StopBot()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchBot(float delai, int points)
    {
        float tpsBot = Random.Range(0.1f, tpsRep);
        bool trouve = false;
        if (delai >= tpsBot)
        {
            yield return new WaitForSeconds(tpsBot);
            trouve = chances > Random.Range(0f, 4f);
        }
        if (trouve)
        {
            score.AddtoMult(1);
            score.AddScore(points);
        }
        else
        {
            score.AddtoMult(0);
            score.ShowScore();
        }
    }

    public void NewBot(int lvl)
    {
        anim.SetInteger("lvl", lvl);
        switch (lvl)
        {
            case 1:
                tpsRep = 0.9f;
                chances = 2.5f;
                break;
            case 2:
                tpsRep = 0.8f;
                chances = 3f;
                break;
            case 3:
                tpsRep = 0.7f;
                chances = 3.5f;
                break;
            case 4:
                tpsRep = 0.7f;
                chances = 4f;
                break;
        }
    }
}
