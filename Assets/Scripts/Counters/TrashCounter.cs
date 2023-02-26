using System;
using Counters;

public class TrashCounter : BaseCounter
{

    public static event EventHandler OnItemTrashed;
    
    new public static void ResetStaticData()
    {
        OnItemTrashed = null;
    }
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            
            OnItemTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
