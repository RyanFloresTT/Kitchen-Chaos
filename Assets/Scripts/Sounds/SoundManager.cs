using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private AudioClipRefsSO audioClipRefsSo;
    private DeliveryCounter _deliveryCounter;
    
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManagerOnOnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManagerOnOnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounterOnOnAnyCut;
        Player.Instance.OnItemPickup += PlayerOnItemPickup;
        PlayerSounds.OnPlayerStep += PlayerSoundsOnOnPlayerStep;
        BaseCounter.OnObjectPlaced += BaseCounterOnOnObjectPlaced;
        TrashCounter.OnItemTrashed += TrashCounterOnOnItemTrashed;
        _deliveryCounter = DeliveryCounter.Instance;
    }

    private void PlayerSoundsOnOnPlayerStep(object sender, EventArgs e)
    {
        PlayerSounds playerSounds = sender as PlayerSounds;
        PlaySound(audioClipRefsSo.Footstep, playerSounds.transform.position);
    }

    private void TrashCounterOnOnItemTrashed(object sender, EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSo.Trash, trashCounter.transform.position);
    }

    private void BaseCounterOnOnObjectPlaced(object sender, EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSo.ObjectDrop, baseCounter.transform.position);
    }

    private void PlayerOnItemPickup(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSo.ObjectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounterOnOnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSo.Chop, cuttingCounter.transform.position);
    }

    private void DeliveryManagerOnOnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSo.DeliverySuccess, _deliveryCounter.transform.position);
    }

    private void DeliveryManagerOnOnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipRefsSo.DeliveryFail, _deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
