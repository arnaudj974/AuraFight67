using UnityEngine;

public class GameJvAI : MonoBehaviour
{
    public Bubble bulle;
    public Score scorePlayer;
    public Score scoreBot;

    public Bot bot;
    public int points = 10;
    private bool changedKey=false;
    private bool isBotStarted = false;

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
            isBotStarted = false;
        }
        else
        {
            if (bot.trouve)
            {
                scoreBot.AddScore(points);
                bot.trouve = false;
            }
            if (!isBotStarted)
            {
                bot.StartBot(bulle.screenTime);
                isBotStarted = true;
            }
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
