using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReaderController : MonoBehaviour
{
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
        Debug.Log($"Se cambió al capitulo con ID {idCapituloAcambiar} y nombre {capitulosData[idCapituloActual].infoBotones[0].nombreCapitulo}");
        idCapituloActual = idCapituloAcambiar;

    }
    public void cambiar2Cap()
    {
        int idCapituloAcambiar = capitulosData[idCapituloActual].infoBotones[1].idCapitulo;
        Debug.Log($"Se cambió al capitulo con ID {idCapituloAcambiar} y nombre {capitulosData[idCapituloActual].infoBotones[1].nombreCapitulo}");
        idCapituloActual = idCapituloAcambiar;

    }
    void verificar2OpcionBtn()
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
    }
    void verificarUltimoCap()
    {
        int cantidadOpciones = capitulosData[idCapituloActual].infoBotones.Length;
        if(cantidadOpciones < 1)
        {
            cambioCap0_btn.onClick.RemoveAllListeners();
            cambioCap0_btn.onClick.AddListener(() => toolsManager.cambioEscena(3));

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
}
