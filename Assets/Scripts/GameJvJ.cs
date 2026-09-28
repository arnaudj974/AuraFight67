using UnityEngine;

public class GameJvJ : MonoBehaviour
{
    public float screenTime = 0.7f;
    public float waitTime = 0.2f;
    public int lvl = 1;
    public GameMaster gm;
    public Bubble bulle1;
    public BubbleJ2 bulle2;
    public Player player1;
    public Player player2;
    public int points = 10;
    private bool changedKey1 = true;
    private bool changedKey2 = true;
    private bool isPlayer1Started = false;
    private bool isPlayer2Started = false;

    void Start()
    {
        OnStart(true);
        gm.pvp = true;
        player1.pvp = true;
        player2.pvp = true;
    }
    void Update()
    {
        if (gm.gameStarted)//verifie que le jeu a commencé
        {

            if (gm.gameFinie)//verifie si le temps de jeu est fini
            {
                bulle1.StopBulle();
                bulle1.Clear();
                bulle2.StopBulle();
                bulle2.Clear();
            }
            else
            {
                gm.res = GetResult();
                if (bulle1.GetInputAsked() == ' ') //réinitialisation quand la bulle1 n'est pas affichée
                {
                    changedKey1 = true;
                    changedKey2 = true;
                    isPlayer1Started = false;
                    isPlayer2Started = false;
                }
                else
                {
                    if (!isPlayer1Started) //lance le player 
                    {
                        player1.StartPlayer(screenTime);
                        isPlayer1Started = true;
                    }
                    if (!isPlayer2Started) //lance le player 2
                    {
                        player2.StartPlayer(screenTime);
                        isPlayer2Started = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                {
                    player1.StopPlayer(); //stop le coroutine pour ne pas réinitialiser le mult
                    if (Input.GetKeyDown(bulle1.GetInputAsked().ToString()) && changedKey1)
                    {
                        player1.AddScore(points, 1);
                    }
                    else
                    {
                        player1.AddScore(0, 0);
                    }
                    changedKey1 = false;
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    player2.StopPlayer(); //stop le coroutine pour ne pas réinitialiser le mult
                    if (Input.GetKeyDown(bulle2.GetInputAsked()) && changedKey2)
                    {
                        player2.AddScore(points, 1);
                    }
                    else
                    {
                        player2.AddScore(0, 0);
                    }
                    changedKey2 = false;
                }
            }
        }
    }
    private int GetResult()
    {
        if (player1.score.score > player2.score.score)
        {
            return 0;
        }
        else if (player1.score.score < player2.score.score)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public void OnStart(bool newLvl)
    {
        if (!newLvl)//cas retry
        {
            bulle1.StopBulle();
            bulle2.StopBulle();
            player1.StopPlayer();
            player2.StopPlayer();
            gm.StopGameMaster();
        }
        if (gm.res == 0 && newLvl)// si win
        {
            lvl++;
        }
        player1.Clear();
        player2.Clear();
        bulle1.Clear();
        bulle2.Clear();
        gm.Clear();
        gm.StartGameMaster(gm.timeGame);
        bulle1.StartBulle(gm.timeIntro,waitTime,screenTime);
        bulle2.StartBulle(gm.timeIntro,waitTime,screenTime);
    }
}

