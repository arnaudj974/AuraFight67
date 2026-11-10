using UnityEngine;
using UnityEngine.Events;


public class Game : MonoBehaviour
{
    [Header("Entities")]

    [SerializeField] private Player player;
    [SerializeField] private Player player2;
    [SerializeField] private Bot bot;
    [SerializeField] private Bulle bulle;
    [SerializeField] private Background back;

    [Header("Parameters")]

    [SerializeField] private float gameTime = 10f;
    [SerializeField] private float screenTime = 0.7f;
    [SerializeField] private float waitTime = 0.2f;
    [SerializeField] private int points = 10;
    int lvl = 1;
    float _gameTime = 0f;
    bool changedKey1 = true;
    bool changedKey2 = true;
    bool pvp;
    public UnityEvent OnFinStart;

    void Awake()
    {
        pvp = GameManager.Instance.mode == GameManager.Mode.PvP;
        enabled = false;
        OnStart(true);
    }

    public void StartGame()
    { 
        enabled = true; 
    }

    void Update()
    {
        if (gameTime <= _gameTime && !bulle.coroutStarted) //la coroutine de la bulle doit être finie
        {
            OnFinStart?.Invoke();
            enabled = false;
        }
        else
        {
            _gameTime += Time.deltaTime;

            if (!bulle.coroutStarted) //Start la bulle
            {
                bulle.StartBulle(waitTime, screenTime);
            }
            if (bulle.GetInputAsked() == "")
            {
                changedKey1 = true;
                changedKey2 = true;
            }
            else
            {
                if (!player.coroutStarted) player.StartPlayer(screenTime);
                if (pvp) { if (!player2.coroutStarted) player2.StartPlayer(screenTime); }
                else { if (!bot.coroutStarted) bot.StartBot(screenTime, points); }
                //if()
            }
        }
    }

    public int GetResult()
    {
        if (GameManager.Instance.mode != GameManager.Mode.PvP)
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
        else
        {
            if (player.score.score > player2.score.score)
            {
                return 0;
            }
            else if (player.score.score < player2.score.score)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }

    public void OnStart(bool newLvl)
    {
        if (!newLvl)//cas retry
        {
            bulle.StopBulle();
            player.StopPlayer();
            player2?.StopPlayer();
            bot?.StopBot();
        }
        if (GetResult() == 0 && newLvl)// si win
        {
            lvl++;
        }
        back.ChangeBackground(lvl); //change le backround 
        player.Clear();
        player2?.Clear();
        bot?.NewBot(lvl, false, screenTime);
        bulle.Clear();
        _gameTime = 0f;
    }
}
