using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorInteractable : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Handheld.Vibrate();
        Debug.Log("Interacted with Collector");
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1f);
    }

}
