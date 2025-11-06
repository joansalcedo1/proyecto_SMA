using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReaderController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelEleccion;
    [SerializeField]
    private Button capFinal_btn;
    [SerializeField]
    private List<ScriptableCapituloData> capitulosData;
    [SerializeField]
    public TextMeshProUGUI titulo;

    public Image escenario1, escenario2, escenario3, escenario4;
    public int idCapituloActual = 0;

    [SerializeField]
    private toolsManager toolsManager;
    // Start is called before the first frame update
    public bool todosLeidos = false;
    private void Awake()
    {
        panelEleccion.SetActive(true);
        escenario1 = escenario1.GetComponent<Image>();
        escenario2 = escenario2.GetComponent<Image>();
        escenario3 = escenario3.GetComponent<Image>();
        escenario4 = escenario4.GetComponent<Image>();

        titulo.text = capitulosData[idCapituloActual].nombreCapitulo;
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
            capitulosData[idCapitulo].fueLeido = true;

            Debug.Log($"Cambiado al capítulo {idCapitulo}: {capitulosData[idCapitulo].nombreCapitulo}");
        }
        else
        {
            Debug.LogWarning($"ID de capítulo {idCapitulo} fuera de rango");
        }
        VerificarProgreso();

    }

    void VerificarProgreso()
    {
        foreach (var capitulo in capitulosData)
        {
            if (capitulo.esCapituloFinal) continue; // Saltar el capítulo final
            if (!capitulo.fueLeido) todosLeidos = false;
        }
        todosLeidos = true;

        if (todosLeidos)
        {
            capFinal_btn.gameObject.SetActive(true);
        } else
        {
            capFinal_btn.gameObject.SetActive(false);
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
        capitulosData[idCapituloActual].fueLeido = true;
        //verificar2OpcionBtn();
        //verificarUltimoCap();
    }

    #region TEST
    [ContextMenu("Test Verificar 2 Opcion Btn")]
    /*void TestVerificar2OpcionBtn()
    {
        verificar2OpcionBtn(true);
    }*/
    [ContextMenu("Test Verificar Ultimo Cap")]
    void TestVerificarUltimoCap()
    {
        // verificarUltimoCap(true);
    }
    #endregion
}
