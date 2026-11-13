using UnityEngine;

public enum Mode { Campagne, Infini, PvP }


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Mode mode;
    public int lvl=1;
    public const int points = 10;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameManager>();
            }
            return _instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


}
