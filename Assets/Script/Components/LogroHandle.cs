using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogroHandle : MonoBehaviour
{
    [Header("Configuración de imagen")]
    [SerializeField] private Image imagenObjetivo;     // Imagen UI interna
    [SerializeField] private Sprite[] sprites;         // Sprites posibles
    public int indiceSprite = 0;                       // Índice del sprite a usar

    [Header("Comportamiento")]
    public float duracionVisible = 3f;                 // Tiempo antes de ocultarse
    public float duracionAnimacion = 0.3f;             // Duración de la animación de aparición

    private Vector3 escalaOriginal;

    private void OnEnable()
    {
        // Validar datos
        if (imagenObjetivo == null || sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning($"{name}: Falta asignar imagen o sprites en LogroHandle");
            return;
        }

        // Evitar índices fuera de rango
        indiceSprite = Mathf.Clamp(indiceSprite, 0, sprites.Length - 1);

        // Asignar sprite
        imagenObjetivo.sprite = sprites[indiceSprite];

        // Guardar la escala original cada vez que se activa (importante si estaba desactivado)
        escalaOriginal = transform.localScale == Vector3.zero ? Vector3.one : transform.localScale;

        // Empezar desde escala cero para la animación
        transform.localScale = Vector3.zero;

        StopAllCoroutines();
        StartCoroutine(AparecerConAnimacion());
    }

    private IEnumerator AparecerConAnimacion()
    {
        float tiempo = 0f;
        while (tiempo < duracionAnimacion)
        {
            tiempo += Time.deltaTime;
            float t = Mathf.Clamp01(tiempo / duracionAnimacion);
            transform.localScale = Vector3.Lerp(Vector3.zero, escalaOriginal, t);
            yield return null;
        }

        transform.localScale = escalaOriginal;
        StartCoroutine(DesactivarTrasTiempo());
    }

    private IEnumerator DesactivarTrasTiempo()
    {
        yield return new WaitForSeconds(duracionVisible);
        gameObject.SetActive(false);
    }
}
