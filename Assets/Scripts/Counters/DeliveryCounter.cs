using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;
        if (!player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) return;
        
        DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
        player.GetKitchenObject().DestroySelf();
    }
}
