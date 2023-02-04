using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;

    private ClearCounter _clearCounter;

    public KitchenObjectScriptableObject GetKitchenObjectScriptableObject()
    {
        return kitchenObjectScriptableObject;
    }

    public void SetClearCounter(ClearCounter clearCounter)
    {
        if (_clearCounter != null)
        {
            _clearCounter.ClearKitchenObject();
        }
        _clearCounter = clearCounter;

        if (clearCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a Kitchen Object.");
        }
        clearCounter.SetKitchenObject(this);
        
        transform.parent = clearCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }
}
