using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEngine;
using UnityEngine.UIElements;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeScriptableObject[] cuttingRecipies;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectScriptableObject inputKitchenObject)
    {
        foreach (var recipe in cuttingRecipies)
        {
            if (recipe.input == inputKitchenObject)
            {
                return true;
            }
        }

        return false;
    }
    

    public override void AlternateInteract(Player player)
    {
        if (!HasKitchenObject() || !HasRecipeWithInput(GetKitchenObject().GetKitchenObjectScriptableObject())) return;
        var outputKitchenObject = GetOutputForInput(GetKitchenObject().GetKitchenObjectScriptableObject());
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObject)
    {
        foreach (var recipe in cuttingRecipies)
        {
            if (recipe.input == inputKitchenObject)
            {
                return recipe.output;
            }
        }

        return null;
    }
}
