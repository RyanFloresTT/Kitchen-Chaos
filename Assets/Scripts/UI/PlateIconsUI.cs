using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;
    private List<KitchenObjectSO> kitchenObjectSOs;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObjectOnOnIngredientAdded;
        kitchenObjectSOs = plateKitchenObject.GetKitchenObjectSOList();
    }

    private void PlateKitchenObjectOnOnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (var kitchenObjectSO in kitchenObjectSOs)
        {
            var iconGameObject = Instantiate(iconTemplate, transform);
            iconGameObject.gameObject.SetActive(true);
            iconGameObject.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
    }
}
