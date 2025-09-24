using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineta : MonoBehaviour
{
    [SerializeField] private List<inObject> Interacobjects = new List<inObject>();
    [SerializeField] private inObject puerta;
    public AudioSource sonido;
    GameObject objetoMostrar;
    // Start is called before the first frame update
    private void Awake()
    {
        objetoMostrar = GameObject.Find("Content");
        objetoMostrar.SetActive(false);
    }
    void Start()
    {
        Interacobjects.Add(puerta);
        
    }

    public void realizarInteraccion()
    {
        puerta.vibrar();
        Debug.Log(objetoMostrar.name);
        puerta.mostrarContenido(objetoMostrar);

    }
}
