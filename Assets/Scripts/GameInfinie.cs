using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameInfinie : MonoBehaviour
{
    private float screenTime;
    private float waitTime;
    public float baseScreenTime = 0.9f;
    public float baseWaitTime = 0.25f;
    public float minScreenTime = 0.15f;
    public float minWaitTime = 0.05f;
    public float difficultyRate = 0.05f;
    public int lvl = 0;
    public GameMaster gm;
    public Bubble bulle;
    public Player player;
    public Bot bot;
    public Background back;
    public int points = 10;
    public TextMeshProUGUI txtLvl;
    private bool changedKey = true;
    private bool isPlayerStarted = false;
    private bool isBotStarted = false;

    void Start()
    {
        screenTime = baseScreenTime;
        waitTime = baseWaitTime;
        OnStart(true);
        player.pvp = false;
        gm.pvp = false;

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
                        bot.StartBot(screenTime, points);
                        isBotStarted = true;
                    }
                    if (!isPlayerStarted) //lance le player
                    {
                        player.StartPlayer(screenTime);
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
                if (player.score.Erreur()) { OnStart(false); }
            }
        }
    }
    private int GetResult()
    {
        if (player.score.score >= bot.score.score)
        {
            return 0;
        }
        else
        {
            return 2;
        }

    }

    public void OnStart(bool newLvl)
    {
        if (!newLvl)//cas retry
        {
            bulle.StopBulle();
            player.StopPlayer();
            bot.StopBot();
            gm.StopGameMaster();
        }
        if (gm.res == 0 && newLvl)// si win
        {
            UpdateDifficulty();
        }
        bot.NewBot(4, true, screenTime);
        back.ChangeBackground(Random.Range(1,4));
        txtLvl.text = "LEVEL " + lvl.ToString();
        player.Clear();
        bulle.Clear();
        gm.Clear();
        gm.StartGameMaster(UpdateTimeGame());
        bulle.StartBulle(gm.timeIntro, waitTime, screenTime);
    }

    public void UpdateDifficulty()
    {
        lvl++;
        screenTime = minScreenTime + (baseScreenTime - minScreenTime) * Mathf.Exp(-difficultyRate * lvl);
        waitTime = minWaitTime + (baseWaitTime - minWaitTime) * Mathf.Exp(-difficultyRate * lvl);
    }
    public float UpdateTimeGame()
    {
        float cycle = screenTime + waitTime;
        int cycleCount = Mathf.Max(1, Mathf.RoundToInt(gm.timeGame / cycle));
        return cycleCount * cycle;
    }
}

