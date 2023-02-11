using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedArgs> OnIngredientAdded;

    public class OnIngredientAddedArgs : EventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validKitchenObjects;
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!validKitchenObjects.Contains(kitchenObjectSO))
        {
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }
        kitchenObjectSOList.Add(kitchenObjectSO);
        
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedArgs
        {
            KitchenObjectSO = kitchenObjectSO
        });
        
        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
} 
