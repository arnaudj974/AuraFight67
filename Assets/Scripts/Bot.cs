using System.Collections;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private float tpsRep = 0.7f;
    private float chances = 2f;
    private Coroutine corout;
    public Score score;
    public Animator anim;
    void Start()
    {

    }

    void Update()
    {
        anim.SetFloat("multSpeed", score.mult); //update de l'animation suivant la mult
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
            AddScore(points, 1);
        }
        else
        {
            AddScore(0, 0);
        }
    }

    public void NewBot(int lvl) //création du bot selon le level
    {
        anim.SetInteger("lvl", lvl);
        switch (lvl)
        {
            case 1:
                chances = 2.5f;
                break;
            case 2:
                chances = 3f;
                break;
            case 3:
                chances = 3.5f;
                break;
            case 4:
                chances = 4f;
                break;
        }
    }
    public void AddScore(int points, int mult)
    {
        score.AddtoMult(mult);
        score.AddScore(points);
        score.ShowScore();
    }
}
