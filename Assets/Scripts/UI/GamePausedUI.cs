using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePausedUI : MonoBehaviour
{
    
    
    
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
