using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class soundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    //vars to handle the volume of a group mixer
    public bool isMuted = false;
    public float lastAmbientVolume = 0f;
    private Dictionary<string, float> lastVolumes = new Dictionary<string, float>();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Controla através de un slider el volumen de un grupo del AudioMixer.
    /// </summary>
    public void handleAmbientSound(Slider slider)
    {
        mixer.SetFloat(slider.gameObject.name, Mathf.Log10(slider.value));
    }
    /// <summary>
    /// Activa o desactiva mute en un grupo del AudioMixer.
    /// </summary>
    public void toggleSound(string groupName)
    {
        // Si ya está muteado, lo restauramos
        if (lastVolumes.ContainsKey(groupName))
        {
            mixer.SetFloat(groupName, lastVolumes[groupName]);
            lastVolumes.Remove(groupName);
        }
        else
        {
            // Guardamos el volumen actual y lo muteamos
            if (mixer.GetFloat(groupName, out float currentVolume))
            {
                lastVolumes[groupName] = currentVolume;
            }
            mixer.SetFloat(groupName, -80f);
        }
    }
    

}
