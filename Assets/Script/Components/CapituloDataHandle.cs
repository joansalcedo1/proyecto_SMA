using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CapituloDataHandle : MonoBehaviour
{
    [Header("Datos del capítulo")]
    [SerializeField] ScriptableCapituloData capituloData;
    [Header("Imagen del capítulo (UI)")]
    [SerializeField] Image imagenCapitulo;
     [Header("Eventos opcionales")]
    [SerializeField] UnityEvent onCapituloReaded;
    [SerializeField] UnityEvent onCapituloNotReaded;
    [Header("Colores")]
    [SerializeField] Color colorLeido = Color.white;
    [SerializeField] Color colorNoLeido = Color.gray;

    void Awake()
    {
        if(imagenCapitulo == null)
            imagenCapitulo = GetComponent<Image>();
    }

    void OnEnable()
    {
        if(imagenCapitulo !=null && capituloData != null && capituloData.fueLeido)
        {
            imagenCapitulo.color = colorLeido;
            onCapituloReaded.Invoke();
        }
        else
        {
            imagenCapitulo.color = colorNoLeido;
            onCapituloNotReaded.Invoke();
        }
    }
}
