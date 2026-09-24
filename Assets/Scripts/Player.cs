using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private Coroutine corout;
    public Score score;
    public Animator anim;
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

    public IEnumerator LaunchPlayer(float delai)
    {
        yield return new WaitForSeconds(delai);
        score.AddtoMult(0);
        score.ShowScore();
    }
}
