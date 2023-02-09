using System;
using System.Collections;
using System.Collections.Generic;
using Counters;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipes;
    [SerializeField] private BurningRecipeSO[] burningRecipes;

    private State state;
    private float _fryingTimer;
    private float _burningTimer;
    private FryingRecipeSO _fryingRecipe;
    private BurningRecipeSO _burningRecipe;


    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {

        switch (state)
        {
            case State.Idle:
                break;
            case State.Frying:
                _fryingTimer += Time.deltaTime;
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = _fryingTimer / _fryingRecipe.FryingTimerMax
                });
                if (_fryingTimer > _fryingRecipe.FryingTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_fryingRecipe.output, this);
                    state = State.Fried;
                    _burningTimer = 0;
                    _burningRecipe = GetBurningRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _fryingTimer / _fryingRecipe.FryingTimerMax
                    });
                }
                break;
            case State.Fried:
                _burningTimer += Time.deltaTime;
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = _burningTimer / _burningRecipe.BurningTimerMax
                });
                if (_burningTimer > _burningRecipe.BurningTimerMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_burningRecipe.output, this);
                    state = State.Burned;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                }

                break;
            case State.Burned:
                break;
            default:
                break;
        }
        
        
        if (HasKitchenObject())
        {
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _fryingRecipe = GetFryingRecipeWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                    _fryingTimer = 0;
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _fryingTimer / _fryingRecipe.FryingTimerMax
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

                state = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
                
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0
                });
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObject)
    {
        var fryingRecipe = GetFryingRecipeWithInput(inputKitchenObject);
        return fryingRecipe != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
    {
        var fryingRecipe = GetFryingRecipeWithInput(inputKitchenObject);
        if (fryingRecipe != null)
        {
            return fryingRecipe.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectSO inputKitchenObject)
    {
        foreach (var recipe in fryingRecipes)
        {
            if (recipe.input == inputKitchenObject)
            {
                return recipe;
            }
        }

        return null;
    }
    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectSO inputKitchenObject)
    {
        foreach (var recipe in burningRecipes)
        {
            if (recipe.input == inputKitchenObject)
            {
                return recipe;
            }
        }

        return null;
    }
}
