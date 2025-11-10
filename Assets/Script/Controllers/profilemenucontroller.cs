using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ProfileMenuController : MonoBehaviour
{
    [Serializable]
    public struct ProfileMenuItem
    {
        public Button boton;
        public GameObject menuObject;
        public UnityEvent OnClickEvent;
        public UnityEvent OnFocusEvent;
        public UnityEvent OnUnfocusEvent;
    }

    public ProfileMenuItem[] profileMenuItems;

    [Header("Perfil Info")]
    [SerializeField] ReaderController readerController;
    public TextMeshProUGUI nombreUsuario;
    public TextMeshProUGUI cantidadLeidos;

    private int currentSelectedIndex = 0; // Índice del item seleccionado


    private void OnEnable()
    {
        if (readerController != null)
        {
            ActualizarInformacionPerfil();
        }
        else
        {
            cantidadLeidos.transform.parent.gameObject.SetActive(false);
        }
    }

    private void ActualizarInformacionPerfil()
    {
        // Contar capítulos leídos
        int capLeidos = 0;
        for (int i = 0; i < currentSelectedIndex; i++)
        {
            if (readerController.capitulosData[i].fueLeido)
                capLeidos++;
        }

        // Mostrar nombre de usuario (nombre del dispositivo)
        nombreUsuario.text = SystemInfo.deviceName;

        // Mostrar progreso
        cantidadLeidos.text = $"{capLeidos}/{readerController.capitulosData.Count - 1}";
    }

    void Start()
    {
        // Asignar eventos
        for (int i = 0; i < profileMenuItems.Length; i++)
        {
            int index = i;

            // Configurar click
            profileMenuItems[i].boton.onClick.AddListener(() => OnMenuItemClicked(index));

            // Añadir manejador de hover
            AddHoverHandler(profileMenuItems[i].boton, index);
        }

        // Inicializar todos desactivados
        for (int i = 0; i < profileMenuItems.Length; i++)
        {
            if (profileMenuItems[i].menuObject != null)
                profileMenuItems[i].menuObject.SetActive(false);
        }

        // Activar el primero por defecto
        if (profileMenuItems.Length > 0)
        {
            currentSelectedIndex = 0;
            if (profileMenuItems[0].menuObject != null)
                profileMenuItems[0].menuObject.SetActive(true);

            // Disparar evento de focus inicial
            profileMenuItems[0].OnFocusEvent?.Invoke();
        }
    }

    void OnMenuItemClicked(int index)
    {
        if (index == currentSelectedIndex)
            return; // No hacer nada si ya está seleccionado

        // Unfocus del anterior
        profileMenuItems[currentSelectedIndex].OnUnfocusEvent?.Invoke();

        // Activar solo el nuevo menú
        for (int i = 0; i < profileMenuItems.Length; i++)
        {
            bool active = (i == index);
            if (profileMenuItems[i].menuObject != null)
                profileMenuItems[i].menuObject.SetActive(active);
        }

        // Nuevo focus
        currentSelectedIndex = index;
        profileMenuItems[index].OnFocusEvent?.Invoke();
        profileMenuItems[index].OnClickEvent?.Invoke();
    }

    void AddHoverHandler(Button button, int index)
    {
        var hoverHandler = button.gameObject.AddComponent<ButtonHoverHandler>();

        hoverHandler.onEnter = () =>
        {
            // Solo si no es el seleccionado actual
            if (index != currentSelectedIndex)
                profileMenuItems[index].OnFocusEvent?.Invoke();
        };

        hoverHandler.onExit = () =>
        {
            // Solo si no es el seleccionado actual
            if (index != currentSelectedIndex)
                profileMenuItems[index].OnUnfocusEvent?.Invoke();
        };
    }

    // Componente auxiliar interno
    private class ButtonHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Action onEnter;
        public Action onExit;

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onExit?.Invoke();
        }
    }
}