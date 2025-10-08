using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vineta : MonoBehaviour
{
    [SerializeField] private List<InObject> Interacobjects = new List<InObject>();
    [SerializeField] private InObject puerta;
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
