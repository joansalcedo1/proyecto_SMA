using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReaderController : MonoBehaviour
{
    [SerializeField] private GameObject panelEleccion;
    [SerializeField]
    private List<ScriptableCapituloData> capitulosData;
    [SerializeField]
    public TextMeshProUGUI titulo;

    public Image escenario1, escenario2, escenario3, escenario4;

    public Button cambioCap0_btn, cambioCap1_btn;

    public int idCapituloActual = 0;

    [SerializeField]
    private toolsManager toolsManager;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        panelEleccion.SetActive(true);
        escenario1 = escenario1.GetComponent<Image>();
        escenario2 = escenario2.GetComponent<Image>();
        escenario3 = escenario3.GetComponent<Image>();
        escenario4 = escenario4.GetComponent<Image>();
        cambioCap0_btn = cambioCap0_btn.GetComponent<Button>();
        cambioCap1_btn = cambioCap1_btn.GetComponent<Button>();


        titulo.text = capitulosData[idCapituloActual].nombreCapitulo;
    }
    public void cambiar1Cap()
    {
        int idCapituloAcambiar = capitulosData[idCapituloActual].infoBotones[0].idCapitulo;
        Debug.Log($"Capitulo actual:{idCapituloActual}, capitulo a cambiar{idCapituloAcambiar}");
        Debug.Log($"Se cambi� al capitulo con ID {idCapituloAcambiar} y nombre {capitulosData[idCapituloActual].infoBotones[0].nombreCapitulo}");
        idCapituloActual = idCapituloAcambiar;

    }
    public void cambiar2Cap()
    {
        int idCapituloAcambiar = capitulosData[idCapituloActual].infoBotones[1].idCapitulo;
        Debug.Log($"Se cambi� al capitulo con ID {idCapituloAcambiar} y nombre {capitulosData[idCapituloActual].infoBotones[1].nombreCapitulo}");
        idCapituloActual = idCapituloAcambiar;

    }
    void verificar2OpcionBtn(bool test = false)
    {
        int cantidadOpciones = capitulosData[idCapituloActual].infoBotones.Length;

        if (cantidadOpciones > 1)
        {
            cambioCap1_btn.interactable = true;
        }
        else
        {
            cambioCap1_btn.interactable = false;
        }

        if (test) { Debug.Log($"Cantidad de opciones: {cantidadOpciones}"); }
    }
    void verificarUltimoCap(bool test = false)
    {
        int cantidadOpciones = capitulosData[idCapituloActual].infoBotones.Length;
        if (cantidadOpciones < 1)
        {
            cambioCap0_btn.onClick.RemoveAllListeners();
            cambioCap0_btn.onClick.AddListener(() => toolsManager.cambioEscena(3));
            if (test) { Debug.Log("Ultimo capitulo, se cambia la funcionalidad del boton a cambio de escena"); }

            return;
        }

        if(test) { Debug.Log($"Cantidad de opciones: {cantidadOpciones}"); }

    }
    public void ElegirCapitulo(int idCapitulo) //Este método está sujeto a cambios futuros
{
    if (idCapitulo >= 0 && idCapitulo < capitulosData.Count)
    {
        idCapituloActual = idCapitulo;
        titulo.text = capitulosData[idCapitulo].nombreCapitulo;
        escenario1.sprite = capitulosData[idCapitulo].sprite[0];
        escenario2.sprite = capitulosData[idCapitulo].sprite[1];
        escenario3.sprite = capitulosData[idCapitulo].sprite[2];
        escenario4.sprite = capitulosData[idCapitulo].sprite[3];

        Debug.Log($"Cambiado al capítulo {idCapitulo}: {capitulosData[idCapitulo].nombreCapitulo}");
    }
    else
    {
        Debug.LogWarning($"ID de capítulo {idCapitulo} fuera de rango");
    }
}
    // Update is called once per frame
    void Update()
    {
        titulo.text = capitulosData[idCapituloActual].nombreCapitulo;
        escenario1.sprite = capitulosData[idCapituloActual].sprite[0];
        escenario2.sprite = capitulosData[idCapituloActual].sprite[1];
        escenario3.sprite = capitulosData[idCapituloActual].sprite[2];
        escenario4.sprite = capitulosData[idCapituloActual].sprite[3];
        verificar2OpcionBtn();
        verificarUltimoCap();
    }

    #region TEST
    [ContextMenu("Test Verificar 2 Opcion Btn")]
    void TestVerificar2OpcionBtn()
    {
        verificar2OpcionBtn(true);
    }
    [ContextMenu("Test Verificar Ultimo Cap")]
    void TestVerificarUltimoCap()
    {
        verificarUltimoCap(true);
    }
    #endregion
}
