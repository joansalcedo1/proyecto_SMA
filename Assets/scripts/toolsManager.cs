using UnityEngine;

public class toolsManager : MonoBehaviour
{
    public void QuitApp()
    {
        // Cierra la aplicaci�n en un dispositivo m�vil
        Application.Quit();

        // Importante: en el editor de Unity no funciona, as� que esto es solo
        // para depurar durante el desarrollo
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
