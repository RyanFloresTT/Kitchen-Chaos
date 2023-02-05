using System.Collections;
using System.Collections.Generic;
using Counters;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;
    
    public override void Interact(Player player)
    {
    }
}
