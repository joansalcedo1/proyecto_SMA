using UnityEngine;

public class toolsManager : MonoBehaviour
{
    public void QuitApp()
    {
        // Cierra la aplicación en un dispositivo móvil
        Application.Quit();

        // Importante: en el editor de Unity no funciona, así que esto es solo
        // para depurar durante el desarrollo
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
