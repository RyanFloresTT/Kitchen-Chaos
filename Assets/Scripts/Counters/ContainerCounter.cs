using System;
using Counters;
using UnityEngine;

public class ContainerCounter : BaseCounter
{    
    
    public event EventHandler OnPlayerGrabbedObject; 

    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;
        KitchenObject.SpawnKitchenObject(kitchenObjectScriptableObject, player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
