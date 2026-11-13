using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] int index;
    public event Action<int,Vector2> OnMoveInput;
    PlayerInput playerInput;
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    } 

    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) OnMoveInput?.Invoke(index,ctx.ReadValue<Vector2>());
    }

    public int GetIndex()
    {
        return index;
    }
}