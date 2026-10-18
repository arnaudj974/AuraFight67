using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public enum Mode {Campagne, Infini, PvP }
    public Mode mode;
    public enum Time { Intro,Jeu,Fin}
    public Time time;
    public enum ControlScheme { KeyboardZQSD, KeyboardARROWS, Gamepad }
    public ControlScheme csPlayer1;
    public ControlScheme csPlayer2;
    public InputDevice devicePlayer1;
    public InputDevice devicePlayer2;
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
    
    public void ChangeMode(Mode m)
    {
        if (m == Mode.PvP)
        {
            if(Gamepad.all.Count > 0)
            {
                csPlayer2 = ControlScheme.Gamepad;
                csPlayer1 = ControlScheme.KeyboardARROWS;
                devicePlayer1 = Keyboard.current;
                devicePlayer2 = Gamepad.current;
            }
            else
            {
                csPlayer1 = ControlScheme.KeyboardZQSD;
                csPlayer2 = ControlScheme.KeyboardARROWS;
                devicePlayer1 = Keyboard.current;
                devicePlayer2 = Keyboard.current;
            }
        }
    }
}
