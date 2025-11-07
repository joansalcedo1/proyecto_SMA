using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReaderController : MonoBehaviour
{
    [SerializeField]
    private GameObject panelEleccion, buttonsPanel, panelCargando;
    [SerializeField]
    private Button capFinal_btn;
    [SerializeField]
    private List<ScriptableCapituloData> capitulosData;
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
        //panelEleccion.SetActive(true);
        //titulo.text = capitulosData[idCapituloActual].nombreCapitulo;
        ElegirCapitulo(idCapituloActual);
    }

    public void ElegirCapitulo(int idCapitulo) //Este método está sujeto a cambios futuros
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

        // Mostrar panel de carga
        panelCargando.SetActive(true);

        yield return null; // Esperar un frame para que se muestre el panel

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

            // Desactivar todos los interactivos previos
            interactivosInstanciados.ForEach(obj => obj.SetActive(false));

            // Invocar interactivos
            for (int i = 0; i < 4; i++)
            {
                InstanciadorInteractivos(idCapitulo, i + 1, GetEscenarioPorIndice(i));
                yield return null; // Espera un frame para dar tiempo a que se renderice
            }

            StartCoroutine(WaitReading());

            Debug.Log($"Cambiado al capítulo {idCapitulo}: {capitulosData[idCapitulo].nombreCapitulo}");
        }
        else
        {
            Debug.LogWarning($"ID de capítulo {idCapitulo} fuera de rango");
        }

        // Ocultar panel de carga
        panelCargando.SetActive(false);
    }

    List<GameObject> interactivosInstanciados = new();
    void InstanciadorInteractivos(int idCapitulo, int idEscenario, GameObject escenario)
    {
        string nombreObjeto = capitulosData[idCapitulo].nombreCapitulo + idEscenario;

        // Buscar si ya existe como hijo de escenario
        Transform objetoExistente = escenario.transform.Find(nombreObjeto);

        if (objetoExistente != null)
        {
            // Si ya existe, solo activarlo
            objetoExistente.gameObject.SetActive(true);
        }
        else
        {
            // Si no existe, instanciar el prefab
            GameObject prefab = capitulosData[idCapitulo].interactbleObjects[idEscenario - 1];
            if (prefab == null)
            {
                Debug.LogWarning($"No hay prefab asignado para {nombreObjeto}");
                return;
            }

            GameObject instancia = Instantiate(prefab, escenario.transform);
            instancia.name = nombreObjeto; // Asignar el nombre
            interactivosInstanciados.Add(instancia);
        }
    }

    IEnumerator WaitReading()
    {
        yield return new WaitForSeconds(5f);
        buttonsPanel.SetActive(true);
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
        }
        else
        {
            capFinal_btn.gameObject.SetActive(false);
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
