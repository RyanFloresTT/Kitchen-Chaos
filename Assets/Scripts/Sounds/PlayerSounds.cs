using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;
    public static event EventHandler OnPlayerStep;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer += Time.deltaTime;
        if (!(footstepTimer >= footstepTimerMax)) return;
        footstepTimer = 0;

        if (!_player.IsWalking()) return;
        OnPlayerStep?.Invoke(this, EventArgs.Empty);
    }
}
