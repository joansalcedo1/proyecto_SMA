using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public AudioSource audioSource;

    public bool isFirstTime { get; private set; } = false;
    public bool isContinuingGame { get; private set; } = false;

    private const string FIRST_TIME_KEY = "FirstTimePlayed";
    private const string LECTURA_KEY = "CapitulosLeidos";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        VerificarEstadoJuego();
    }

    private void VerificarEstadoJuego()
    {
        if (!PlayerPrefs.HasKey(FIRST_TIME_KEY))
        {
            isFirstTime = true;
            isContinuingGame = false;

            PlayerPrefs.SetInt(FIRST_TIME_KEY, 1);
            PlayerPrefs.Save();

            Debug.Log("🎮 Primer ingreso detectado");
        }
        else
        {
            isFirstTime = false;
            isContinuingGame = true;

            Debug.Log("▶ Continuando partida existente");
        }
    }

    public void OneShootAudio(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// Guarda el estado de lectura de los capítulos en PlayerPrefs usando JSON
    /// </summary>
    public void GuardarProgreso(bool[] capitulosLeidos)
    {
        string json = JsonUtility.ToJson(new BoolArrayWrapper { array = capitulosLeidos });
        PlayerPrefs.SetString(LECTURA_KEY, json);
        PlayerPrefs.Save();
        Debug.Log("💾 Progreso guardado en PlayerPrefs");
    }

    /// <summary>
    /// Recupera el estado de lectura de PlayerPrefs
    /// </summary>
    public bool[] CargarProgreso(int cantidadCapitulos)
    {
        if (PlayerPrefs.HasKey(LECTURA_KEY))
        {
            string json = PlayerPrefs.GetString(LECTURA_KEY);
            var wrapper = JsonUtility.FromJson<BoolArrayWrapper>(json);
            return wrapper.array;
        }

        // Si no hay datos, devuelve todos false
        return new bool[cantidadCapitulos];
    }

    /// <summary>
    /// Resetear progreso completo (primera vez + capítulos)
    /// </summary>
    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey(FIRST_TIME_KEY);
        PlayerPrefs.DeleteKey(LECTURA_KEY);
        PlayerPrefs.Save();
        Debug.Log("🧹 Progreso reiniciado");
    }

    [Serializable]
    private class BoolArrayWrapper
    {
        public bool[] array;
    }
}