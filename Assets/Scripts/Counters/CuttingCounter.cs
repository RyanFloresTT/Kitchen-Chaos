using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEngine;
using UnityEngine.UIElements;

public class CuttingCounter : BaseCounter
{
    
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }
    [SerializeField] private CuttingRecipeScriptableObject[] cuttingRecipies;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectScriptableObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    var cuttingRecipe = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectScriptableObject());
                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipe.CuttingProgressMax
                    });
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
        var cuttingRecipe = GetCuttingRecipeWithInput(inputKitchenObject);
        return cuttingRecipe != null;
    }
    

    public override void AlternateInteract(Player player)
    {
        var currentKitchenObject = GetKitchenObject().GetKitchenObjectScriptableObject();
        
        if (!HasKitchenObject() || !HasRecipeWithInput(currentKitchenObject)) return;
        cuttingProgress++;
        OnCut?.Invoke(this, EventArgs.Empty);
        var cuttingRecipe = GetCuttingRecipeWithInput(currentKitchenObject);
        
        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress / cuttingRecipe.CuttingProgressMax
        });
        
        if (cuttingProgress < cuttingRecipe.CuttingProgressMax) return;
        var outputKitchenObject = GetOutputForInput(currentKitchenObject);
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
    }

    private KitchenObjectScriptableObject GetOutputForInput(KitchenObjectScriptableObject inputKitchenObject)
    {
        var cuttingRecipe = GetCuttingRecipeWithInput(inputKitchenObject);
        if (cuttingRecipe != null)
        {
            return cuttingRecipe.output;
        }
        else
        {
            return null;
        }
    }

    private CuttingRecipeScriptableObject GetCuttingRecipeWithInput(KitchenObjectScriptableObject inputKitchenObject)
    {
        foreach (var recipe in cuttingRecipies)
        {
            if (recipe.input == inputKitchenObject)
            {
                return recipe;
            }
        }

        return null;
    }
}
