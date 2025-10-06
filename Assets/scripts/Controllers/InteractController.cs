using UnityEngine;
using UnityEngine.EventSystems;


public class InteractController : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Cámara usada para el ScreenPointToRay. Si está vacío, usa Camera.main")]
    public Camera targetCamera;


    [Tooltip("Layers que serán interactuables")]
    public LayerMask layerMask = ~0; // por defecto todas las layers


    [Tooltip("Distancia máxima del raycast")]
    public float maxDistance = 100f;


    [Tooltip("Hacer debug draw de los rayos")]
    public bool debugRays = false;
    public static InteractController Instance { get; private set; }

    void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            Debug.LogWarning("InteractController: Ninguna camara fue agregada así que se utilizó la main.");
        }

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // ← Sobrevive al cambio de escena
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Mobile touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                // Ignorar si el toque inició sobre UI
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;


                HandleTap(touch.position);
            }
        }
        else
        {
            // Fallback para testing en editor con mouse
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                // Ignorar si el click inició sobre UI
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                    return;


                HandleTap(Input.mousePosition);
            }
#endif
        }
    }

    private void HandleTap(Vector2 screenPosition)
    {
        if (targetCamera == null)
            return;


        Ray ray = targetCamera.ScreenPointToRay(screenPosition);


        if (debugRays)
            Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.green, 2f);


        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
        {
            // Busca componente que implemente IInteractable en el collider o sus padres
            var interactable = hit.collider.GetComponentInParent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();


                // Si quieres enviar contexto, por ejemplo la posición del impacto
                // interactable.OnInteract(hit.point, hit.normal);
            }
            else
            {
                // opcional: log para debug
                if (debugRays)
                    Debug.Log($"TouchRaycaster: golpeó {hit.collider.name} pero no implementa IInteractable.");
            }
        }
    }

}

