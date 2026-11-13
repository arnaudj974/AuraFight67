using UnityEngine;
using UnityEngine.Events;


public class Game : MonoBehaviour
{
    [Header("Entities")]

    Player player;
    Player player2;
    Bot bot;
    [SerializeField] private Bulle bulle;
    [SerializeField] private Background back;

    [Header("Parameters")]

    [SerializeField] private float gameTime = 10f;
    [SerializeField] private float screenTime = 0.7f;
    [SerializeField] private float waitTime = 0.2f;
    public float baseScreenTime = 0.95f;
    public float baseWaitTime = 0.27f;
    public float minScreenTime = 0.15f;
    public float minWaitTime = 0.05f;
    public float difficultyRate = 0.05f;
    float _gameTime = 0f;
    bool pvp;
    public UnityEvent OnFinStart;

    void Start()
    {
        pvp = GameManager.Instance.mode == Mode.PvP;
        player = FindAnyObjectByType<Player>();
        if (!pvp)
        {
            bot = FindAnyObjectByType<Bot>();
            if (GameManager.Instance.mode == Mode.Infini)
            {
                UpdateDifficulty(GameManager.Instance.lvl);
                bot.NewBot(5, screenTime);
            }
            else bot.NewBot(GameManager.Instance.lvl, screenTime);
        }
        else player2 = FindObjectsByType<Player>()[1];
        Debug.Log($"ScreenTime: {screenTime}, WaitTime: {waitTime}");
        this.enabled = false;
    }

    public void StartGame()
    {
        this.enabled = true;
    }

    void Update()
    {
        if (gameTime <= _gameTime && !bulle.coroutStarted) //la coroutine de la bulle doit être finie
        {
            OnFinStart?.Invoke();
            this.enabled = false;
        }
        else
        {
            _gameTime += Time.deltaTime;

            if (!bulle.coroutStarted) //Start la bulle
            {
                bulle.StartBulle(waitTime, screenTime);
            }
        }
    }

    public int GetResult()
    {
        if (!pvp)
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
                return GameManager.Instance.lvl == 4 ? 3 : 1;
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
    public void UpdateDifficulty(int lvl)
    {
        screenTime = minScreenTime + (baseScreenTime - minScreenTime) * Mathf.Exp(-difficultyRate * lvl);
        waitTime = minWaitTime + (baseWaitTime - minWaitTime) * Mathf.Exp(-difficultyRate * lvl);
    }
}
