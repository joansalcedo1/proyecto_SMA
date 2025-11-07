using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Eje de rotación (usar Vector3.forward para sprites 2D)")]
    public Vector3 rotationAxis = Vector3.forward; // Por defecto para sprites 2D
    [Header("Velocidad de rotación en grados por segundo")]
    public float rotationSpeed = 90f;

    void Update()
    {
        // Rotar el objeto cada frame
        transform.Rotate(rotationAxis.normalized * rotationSpeed * Time.deltaTime);
    }
}
