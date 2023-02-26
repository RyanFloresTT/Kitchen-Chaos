using System;
using UnityEngine;

namespace Counters
{
    public class BaseCounter : MonoBehaviour, IKitchenObjectParent
    {
        public static event EventHandler OnObjectPlaced;
        public static void ResetStaticData()
        {
            OnObjectPlaced = null;
        }
        [SerializeField] private Transform counterTopPoint;
    
        private KitchenObject _kitchenObject;
        public virtual void Interact(Player player)
        {
            Debug.LogError("BaseCounter.Interact();");
        }    
        public virtual void AlternateInteract(Player player)
        {
        }    
        public Transform GetKitchenObjectFollowTransform()
        {
            return counterTopPoint;
        }

        public void SetKitchenObject(KitchenObject kitchenObject)
        {
            _kitchenObject = kitchenObject;
            if (kitchenObject == null) return;
            OnObjectPlaced?.Invoke(this, EventArgs.Empty);
        }
        
        public KitchenObject GetKitchenObject()
        {
            return _kitchenObject;
        }

        public void ClearKitchenObject()
        {
            _kitchenObject = null;
        }

        public bool HasKitchenObject()
        {
            return _kitchenObject != null;
        }
    }
}