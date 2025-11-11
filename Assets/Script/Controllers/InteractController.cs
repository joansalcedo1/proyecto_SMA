using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class InteractController : MonoBehaviour
{
    [Header("Configuración")]
    public Camera targetCamera;
    public LayerMask layerMask = ~0;
    public float maxDistance = 100f;
    public bool debugRays = false;

    [Header("UI Raycasting")]
    public GraphicRaycaster uiRaycaster; // el GraphicRaycaster del canvas principal
    public EventSystem eventSystem;

    public static InteractController Instance { get; private set; }

    void Awake()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (uiRaycaster == null)
            uiRaycaster = FindFirstObjectByType<GraphicRaycaster>();

        if (eventSystem == null)
            eventSystem = FindFirstObjectByType<EventSystem>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            HandleTap(Input.mousePosition);
#endif
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            HandleTap(Input.GetTouch(0).position);
    }

    private void HandleTap(Vector2 screenPosition)
    {
        // 🔹 1️⃣ Primero probamos con UI
        if (uiRaycaster != null && eventSystem != null)
        {
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = screenPosition;

            List<RaycastResult> results = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerData, results);

            foreach (var result in results)
            {
                var interactableUI = result.gameObject.GetComponentInParent<IInteractable>();
                if (interactableUI != null)
                {
                    Debug.Log($"InteractController: golpeó UI {result.gameObject.name}");
                    interactableUI.Interact();
                    return;
                }
            }
        }

        // 🔹 2️⃣ Si no fue UI, probamos con mundo 3D
        Ray ray = targetCamera.ScreenPointToRay(screenPosition);
        if (debugRays)
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 2f);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
        {
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                Debug.Log($"InteractController: golpeó objeto 3D {hit.collider.name}");
                interactable.Interact();
                return;
            }
        }

        Debug.Log("InteractController: ningún objeto interactivo encontrado.");
    }
}
