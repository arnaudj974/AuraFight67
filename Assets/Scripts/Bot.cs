using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{
    public int lvl;
    public float tpsRep;
    public float chances = 2.0f;
    private Coroutine corout;
    public bool trouve;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBot(float delai)
    {
        corout = StartCoroutine(LaunchBot(delai));
    }

    public void StopBot()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchBot(float delai)
    {
        float tpsBot = Random.Range(0.1f, tpsRep);
        trouve = false;
        if (delai >= tpsBot)
        {
            yield return new WaitForSeconds(tpsBot);
            trouve = chances > Random.Range(0f, 4f);
        }
    }
}
