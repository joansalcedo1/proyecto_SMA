using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevoCapitulo", menuName = "SistemaLectura/Capitulo")]
public class ScriptableCapituloData : ScriptableObject
{
    public int capituloId;
    public string nombreCapitulo;
    public Sprite[] sprite= new Sprite[4];
    public GameObject[] interactbleObjects = new GameObject[4];
    public bool fueLeido = false;
    public bool esCapituloFinal = false;
    /*public ButtonData[] infoBotones = new ButtonData[2];

    [System.Serializable]
    public struct ButtonData 
    { 
        public string nombreCapitulo;
        public int idCapitulo;
    }*/
}
