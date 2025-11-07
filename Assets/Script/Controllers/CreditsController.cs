using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [SerializeField] GameObject mensajeFinal, panelCreditos;

    void Start()
    {
        StartCoroutine(MostrarMensajeAntesDeInciiar());
    }

    IEnumerator MostrarMensajeAntesDeInciiar()
    {
        yield return new WaitForSeconds(5f);
        mensajeFinal.SetActive(false);
        panelCreditos.SetActive(true);
    }
}
