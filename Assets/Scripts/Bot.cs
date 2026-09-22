using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour
{
    public int lvl;
    public float tpsRep;
    private Coroutine corout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBot()
    {
        corout = StartCoroutine(LaunchBot());
    }

    public void StopBot()
    {
        StopCoroutine(corout);
    }

    public IEnumerator LaunchBot()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0,tpsRep));
            Action();
        }
    }

    public bool Action()
    {
        return true;
    }
}
