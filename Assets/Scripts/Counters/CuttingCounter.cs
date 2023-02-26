using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEngine;
using UnityEngine.UIElements;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnAnyCut;

    new public static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;

    [SerializeField] private CuttingRecipeSO[] cuttingRecipies;
    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    
                    var cuttingRecipe = GetCuttingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
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
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf(); 
                    }
                }
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    

    public override void AlternateInteract(Player player)
    {
        var currentKitchenObject = GetKitchenObject().GetKitchenObjectSO();
        
        if (!HasKitchenObject() || !HasRecipeWithInput(currentKitchenObject)) return;
        cuttingProgress++;
        OnCut?.Invoke(this, EventArgs.Empty);
        OnAnyCut?.Invoke(this, EventArgs.Empty);
        var cuttingRecipe = GetCuttingRecipeWithInput(currentKitchenObject);
        
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress / cuttingRecipe.CuttingProgressMax
        });
        
        if (cuttingProgress < cuttingRecipe.CuttingProgressMax) return;
        var outputKitchenObject = GetOutputForInput(currentKitchenObject);
        GetKitchenObject().DestroySelf();
        KitchenObject.SpawnKitchenObject(outputKitchenObject, this);
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
    {
        var cuttingRecipe = GetCuttingRecipeWithInput(inputKitchenObject);
        return cuttingRecipe != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
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

    private CuttingRecipeSO GetCuttingRecipeWithInput(KitchenObjectSO inputKitchenObject)
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
