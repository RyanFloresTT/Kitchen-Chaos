using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{

    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;
    [SerializeField] private Transform counterTopPoint;
    
    public void Interact()
    {
        Debug.Log("Interact");
        var kitchenObjectTransform = Instantiate(kitchenObjectScriptableObject.prefab, counterTopPoint);
        kitchenObjectTransform.localPosition = Vector3.zero;
        
    }
}
