using UnityEngine;

public class OneShootAudioManager : MonoBehaviour
{
    [SerializeField] AudioClip clip;

    public void PlayClip()
    {
        GameManager.Instance.OneShootAudio(clip);
    }
}
