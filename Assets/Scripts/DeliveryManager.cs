using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeList;
    
    private List<RecipeSO> _waitingRecipes;
    private float _recipeTimer = 0f;
    private readonly float _recipeTimerMax = 4f;
    private int _maxWaitingRecipes = 4;
    private int successfulRecipesAmount;

    private void Awake()
    {
        _waitingRecipes = new List<RecipeSO>();
        Instance = this;
    }

    private void Update()
    {
        _recipeTimer += Time.deltaTime;
        
        if (!(_recipeTimer >= _recipeTimerMax)) return;
        _recipeTimer = 0;
        
        if (_waitingRecipes.Count >= _maxWaitingRecipes) return;
        var randRecipe = recipeList.RecipeLists[Random.Range(0, recipeList.RecipeLists.Count)];
        _waitingRecipes.Add(randRecipe);
        
        OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (var waitingRecipe in _waitingRecipes)
        {
            if (waitingRecipe.kitchenObjectSOs.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                var plateContentsMatchesRecipe = true;
                foreach (var waitingRecipeKitchenObjectSO in waitingRecipe.kitchenObjectSOs)
                {
                    var ingredientFound = false;
                    foreach (var plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO != waitingRecipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe)
                {
                    _waitingRecipes.Remove(waitingRecipe);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    successfulRecipesAmount++;
                    return;
                }
            }
        }
        // Player failed to deliver a correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipes()
    {
        return _waitingRecipes;
    }

    public int GetSuccessfulRecipesAmount()
    {
        return successfulRecipesAmount;
    }
}
