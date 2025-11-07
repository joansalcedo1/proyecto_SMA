using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeAudioChannel : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixer audioMixer;    
    public string exposedParam = "MasterVolume";  

    [Header("UI")]
    public Slider volumeSlider;  

    [Header("Volume Range")]
    public float minVolume = -80f; // en dB
    public float maxVolume = 10f;  // en dB

    void Start()
    {
        if (volumeSlider != null)
        {
            // Cargar el valor guardado en PlayerPrefs si existe
            float savedVolume = PlayerPrefs.GetFloat(exposedParam, 0f); // por defecto 0 dB
            audioMixer.SetFloat(exposedParam, savedVolume);

            // Configuramos el slider con el valor actual
            volumeSlider.value = Mathf.InverseLerp(minVolume, maxVolume, savedVolume);

            // Añadimos listener
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float sliderValue)
    {
        // Convertimos el valor del slider al rango dB
        float dB = Mathf.Lerp(minVolume, maxVolume, sliderValue);
        audioMixer.SetFloat(exposedParam, dB);

        // Guardar en PlayerPrefs
        PlayerPrefs.SetFloat(exposedParam, dB);
        PlayerPrefs.Save();
    }
}