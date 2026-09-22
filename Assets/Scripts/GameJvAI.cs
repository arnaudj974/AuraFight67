using UnityEngine;

public class GameJvAI : MonoBehaviour
{
    public Bubble bulle;
    public Score scorePlayer;
    public Score scoreBot;

    public Bot bot;
    public int points = 10;
    private bool changedKey=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulle.StartBulle();
    }

    // Update is called once per frame
    void Update()
    {
        if (bulle.GetInputAsked() == ' ')
        {
            changedKey = true;
        }
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(bulle.GetInputAsked().ToString()) && changedKey)
            {
                scorePlayer.AddScore(points);
            }
            changedKey = false;
        }
    }

}
