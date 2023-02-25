using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    public event EventHandler OnInteractAction;
    public event EventHandler OnAlternateInteractAction;
    public event EventHandler OnPauseAction;
    private PlayerInputActions _playerInputActions;
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Interact.performed += InteractPerformed;
        _playerInputActions.Player.InteractAlternate.performed += InteractAlternatePerformed;
        _playerInputActions.Player.Pause.performed += PauseOnperformed;

        Instance = this;
    }

    private void PauseOnperformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternatePerformed(InputAction.CallbackContext obj)
    {
        OnAlternateInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
    
        var inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector;
    }
}
