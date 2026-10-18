using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    
    PlayerInput playerInput;
    Player player;
    GameManager.ControlScheme scheme;
    InputDevice device;
    bool manetteBranchee = Gamepad.all.Count > 0;

    void Awake()
    {
        player = GetComponent<Player>();
        // Récupère le script Player Input
        playerInput = GetComponent<PlayerInput>();
        InputSystem.actions.Disable();
        playerInput.currentActionMap?.Enable();
        if(GameManager.Instance.mode != GameManager.Mode.PvP) playerInput.neverAutoSwitchControlSchemes = false;
        else
        {
            playerInput.neverAutoSwitchControlSchemes = true;
            scheme = player.playerIndex == 1 ? GameManager.Instance.csPlayer1 : GameManager.Instance.csPlayer2;
            device = player.playerIndex == 1 ? GameManager.Instance.devicePlayer1 : GameManager.Instance.devicePlayer1;
            playerInput.SwitchCurrentControlScheme(scheme.ToString(), device);
        }
    }
    void Start()
    {

    }


    void Update()
    {

    }
}
