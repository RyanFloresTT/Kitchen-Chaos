using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{

    [SerializeField] private KitchenObjectScriptableObject kitchenObjectScriptableObject;

    public KitchenObjectScriptableObject GetKitchenObjectScriptableObject()
    {
        return kitchenObjectScriptableObject;
    }

}
