using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inObject : MonoBehaviour
{
    public string id;

    //hace una accion
    public void vibrar()
    {
        Debug.Log("Voy a vibrar");
        Handheld.Vibrate();
        Debug.Log("Ya vibré");
    }
    public void cambiarVineta(string vinetaACambiar)
    {
        GameObject.Find(vinetaACambiar).SetActive(true);
    }
    public void mostrarContenido(GameObject contenido)
    {
        Debug.Log(contenido.name);
        contenido.SetActive(true);
    }
    public void ocultarContenido(GameObject contenido)
    {
        contenido.SetActive(false);
    }

    public void sonar(AudioSource clip)
    {
        clip.Play();
    }

    
}
