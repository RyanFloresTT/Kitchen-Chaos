using System;
using Counters;
using UnityEngine;
using UnityEngine.Serialization;

public class ContainerCounter : BaseCounter
{    
    
    public event EventHandler OnPlayerGrabbedObject; 

    [FormerlySerializedAs("kitchenObjectScriptableObject")] [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject()) return;
        KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
