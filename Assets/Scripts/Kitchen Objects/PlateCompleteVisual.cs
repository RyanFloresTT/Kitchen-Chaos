using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjects;
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        
        
        foreach (var kitchenObjectSoGameObject in kitchenObjectSOGameObjects)
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs e)
    {
        foreach (var kitchenObjectSoGameObject in kitchenObjectSOGameObjects)
        {
            if (kitchenObjectSoGameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSoGameObject.gameObject.SetActive(true);
            }
        }
    }
}
