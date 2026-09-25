using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameJvAI : MonoBehaviour
{
    public int lvl = 1;
    public GameMaster gm;
    public Bubble bulle;
    public Player player;
    public Bot bot;
    public int points = 10;
    private bool changedKey = true;
    private bool isBotStarted = false;
    private bool isPlayerStarted = false;

    void Start()
    {
        OnStart(true);
    }
    void Update()
    {
        if (gm.gameStarted)//verifie que le jeu a commencé
        {

            if (gm.gameFinie)//verifie si le temps de jeu est fini
            {
                bulle.StopBulle();
                bulle.Clear();
            }
            else
            {
                gm.res = GetResult();
                if (bulle.GetInputAsked() == ' ') //réinitialisation quand la bulle n'est pas affichée
                {
                    changedKey = true;
                    isBotStarted = false;
                    isPlayerStarted = false;
                }
                else
                {
                    if (!isBotStarted) //démarrer le bot s'il n'est pas lancé au changement d'input demandée
                    {
                        bot.StartBot(bulle.screenTime, points);
                        isBotStarted = true;
                    }
                    if (!isPlayerStarted) //lance le player
                    {
                        player.StartPlayer(bulle.screenTime);
                        isPlayerStarted = true;
                    }
                }
                if (Input.anyKeyDown)
                {
                    player.StopPlayer(); //stop le coroutine pour ne pas réinitialiser le mult
                    if (Input.GetKeyDown(bulle.GetInputAsked().ToString()) && changedKey)
                    {
                        player.AddScore(points, 1);
                    }
                    else
                    {
                        player.AddScore(0, 0);
                    }
                    changedKey = false;
                }
            }
        }
    }
    private int GetResult()
    {
        if (player.score.score > bot.score.score)
        {
            return 0;
        }
        else if (player.score.score < bot.score.score)
        {
            return 2;
        }
        else
        {
            return lvl == 4 ? 3 : 1;
        }
    }

    public void OnStart(bool newLvl)
    {
        if (!newLvl)
        {
            bulle.StopBulle();
            player.StopPlayer();
            bot.StopBot();
            gm.StopGameMaster();
        }
        if (gm.res == 0 && newLvl)
        {
            lvl++;
        }
        player.score.Clear();
        bot.score.Clear();
        bot.NewBot(lvl);
        bulle.Clear();
        gm.Clear();
        gm.StartGameMaster();
        bulle.StartBulle(gm.timeIntro);
    }
}
