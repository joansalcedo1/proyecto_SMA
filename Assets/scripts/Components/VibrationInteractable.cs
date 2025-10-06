using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationInteractable : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Debug.Log("Voy a vibrar");
        Handheld.Vibrate();
        Debug.Log("Ya vibré");
    }
  
}
