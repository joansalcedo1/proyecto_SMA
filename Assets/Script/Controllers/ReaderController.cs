using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReaderController : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds5 = new WaitForSeconds(5f);
    [SerializeField]
    private GameObject panelEleccion, buttonsPanel, panelCargando;
    [SerializeField]
    private List<Button> capituloButtons;
    [SerializeField]
    private Button capFinal_btn;
    public List<ScriptableCapituloData> capitulosData;
    [SerializeField]
    public TextMeshProUGUI titulo;
    public Image escenario1, escenario2, escenario3, escenario4;
    public int idCapituloActual = 0;

    [SerializeField]
    private AudioSource musicChannel, ambienceChannel, dialogChannel, sfxChannel;
    // Start is called before the first frame update
    public bool todosLeidos = false;

    [Header("Test")]
    [SerializeField] int capituloTestId = 0;
    

   

    void Start()
    {
        // Inicializar todos en false
        foreach (var capitulo in capitulosData)
            capitulo.fueLeido = false;

        // Recuperar progreso si hay
        if (GameManager.Instance.isContinuingGame)
        {
            bool[] progreso = GameManager.Instance.CargarProgreso(capitulosData.Count);
            for (int i = 0; i < progreso.Length; i++)
                capitulosData[i].fueLeido = progreso[i];

            panelEleccion.SetActive(true);

            // Desactivar botones de capítulos leídos
            for (int i = 0; i < capituloButtons.Count && i < capitulosData.Count; i++)
            {
                if (capitulosData[i].fueLeido)
                {
                    capituloButtons[i].gameObject.SetActive(false); //interactable = false;
                    var colors = capituloButtons[i].colors;
                    colors.normalColor = Color.gray;
                    capituloButtons[i].colors = colors;
                }
            }
        
        }
        else
        {
            ElegirCapitulo(idCapituloActual);
        }
    }

    private void GuardarProgreso()
    {
        bool[] estado = new bool[capitulosData.Count];
        for (int i = 0; i < capitulosData.Count; i++)
            estado[i] = capitulosData[i].fueLeido;

        GameManager.Instance.GuardarProgreso(estado);
    }

    public void DeleteData()
    {
        GameManager.Instance.ResetProgress();
    }

    public void ElegirCapitulo(int idCapitulo)
    {
        StartCoroutine(ElegirCapituloCoroutine(idCapitulo));
    }

    private IEnumerator ElegirCapituloCoroutine(int idCapitulo)
    {
        GameObject GetEscenarioPorIndice(int i)
        {
            return i switch
            {
                0 => escenario1.gameObject,
                1 => escenario2.gameObject,
                2 => escenario3.gameObject,
                3 => escenario4.gameObject,
                _ => null
            };
        }

        panelCargando.SetActive(true);

        yield return null;

        if (idCapitulo >= 0 && idCapitulo < capitulosData.Count)
        {
            buttonsPanel.SetActive(false);

            idCapituloActual = idCapitulo;
            titulo.text = capitulosData[idCapitulo].nombreCapitulo;
            escenario1.sprite = capitulosData[idCapitulo].sprite[0];
            escenario2.sprite = capitulosData[idCapitulo].sprite[1];
            escenario3.sprite = capitulosData[idCapitulo].sprite[2];
            escenario4.sprite = capitulosData[idCapitulo].sprite[3];
            capitulosData[idCapitulo].fueLeido = true;

            if (capitulosData[idCapitulo].musicaDeFondo != null)
            {
                PlayMusic(capitulosData[idCapitulo].musicaDeFondo);
            }
            if (capitulosData[idCapitulo].ambienteSonoro != null)
            {
                PlayAmbience(capitulosData[idCapitulo].ambienteSonoro);
            }

            interactivosInstanciados.ForEach(obj => obj.SetActive(false));

            for (int i = 0; i < 4; i++)
            {
                InstanciadorInteractivos(idCapitulo, i + 1, GetEscenarioPorIndice(i));
                yield return null;
            }

            StartCoroutine(WaitReading());

            Debug.Log($"Cambiado al capítulo {idCapitulo}: {capitulosData[idCapitulo].nombreCapitulo}");
        }
        else
        {
            Debug.LogWarning($"ID de capítulo {idCapitulo} fuera de rango");
        }

        panelCargando.SetActive(false);
    }

    List<GameObject> interactivosInstanciados = new();
    void InstanciadorInteractivos(int idCapitulo, int idEscenario, GameObject escenario)
    {
        string nombreObjeto = capitulosData[idCapitulo].nombreCapitulo + idEscenario;

        Transform objetoExistente = escenario.transform.Find(nombreObjeto);

        if (objetoExistente != null)
        {
            objetoExistente.gameObject.SetActive(true);
        }
        else
        {
            GameObject prefab = capitulosData[idCapitulo].interactbleObjects[idEscenario - 1];
            if (prefab == null)
            {
                Debug.LogWarning($"No hay prefab asignado para {nombreObjeto}");
                return;
            }

            GameObject instancia = Instantiate(prefab, escenario.transform);
            instancia.name = nombreObjeto;
            interactivosInstanciados.Add(instancia);
        }
    }

    IEnumerator WaitReading()
    {
        yield return _waitForSeconds5;
        GuardarProgreso();
        buttonsPanel.SetActive(true);
        VerificarProgreso();
    }

    void VerificarProgreso()
    {
        int totalLeidos = 0;

        for (int i = 0; i < idCapituloActual; i++)
        {
            if (capitulosData[i].fueLeido)
            {
                totalLeidos++;
                
            }
        }

        

        Debug.Log($"Capítulos leídos antes del actual: {totalLeidos} de {capitulosData.Count - 1}");

        // Penultimo
        if (totalLeidos.ToString() == (capitulosData.Count - 2).ToString())
        {
            Debug.Log("Activando el ultimo capitulo");
            capFinal_btn.gameObject.SetActive(true);
        }
        else if (totalLeidos == capitulosData.Count - 1)
        {
            capFinal_btn.gameObject.SetActive(false);
            SceneManager.LoadScene("2_Creditos");
        }
    }

    #region Audios
    public void PlayMusic(AudioClip clip)
    {
        musicChannel.clip = clip;
        musicChannel.Play();
    }
    public void PlayAmbience(AudioClip clip)
    {
        ambienceChannel.clip = clip;
        ambienceChannel.Play();
    }
    public void PlayDialog(AudioClip clip)
    {
        dialogChannel.clip = clip;
        dialogChannel.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxChannel.clip = clip;
        sfxChannel.Play();
    }
    #endregion

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

    #region Editor Test Methods

    [ContextMenu("Cargar Sprites de Capítulo en Modo Edición")]
    private void TestCargarSpritesEditor()
    {
        if (capitulosData.Count == 0) return;

        int idCapitulo = capituloTestId; // Cambia para probar otros capítulos
        titulo.text = capitulosData[idCapitulo].nombreCapitulo;

        if (escenario1 != null) escenario1.sprite = capitulosData[idCapitulo].sprite[0];
        if (escenario2 != null) escenario2.sprite = capitulosData[idCapitulo].sprite[1];
        if (escenario3 != null) escenario3.sprite = capitulosData[idCapitulo].sprite[2];
        if (escenario4 != null) escenario4.sprite = capitulosData[idCapitulo].sprite[3];

        Debug.Log("Sprites del capítulo cargados en modo edición");
    }

    [ContextMenu("Descargar Sprites de Escenarios")]
    private void TestDescargarSpritesEditor()
    {
        if (escenario1 != null) escenario1.sprite = null;
        if (escenario2 != null) escenario2.sprite = null;
        if (escenario3 != null) escenario3.sprite = null;
        if (escenario4 != null) escenario4.sprite = null;

        titulo.text = "";

        Debug.Log("Sprites descargados de los escenarios");
    }

    #endregion

}
