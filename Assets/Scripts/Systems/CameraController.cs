using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControllerEuler : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 90f;

    private float yaw;   // обертання навколо Y
    private float pitch; // нахил камери по X

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // --- Рух ---
        Vector3 move = Vector3.zero;
        Vector3 f = transform.forward;
        Vector3 _forward = new Vector3(f.x, 0, f.z);
        if (Keyboard.current.wKey.isPressed) move += _forward;
        if (Keyboard.current.sKey.isPressed) move -= _forward;
        if (Keyboard.current.aKey.isPressed) move -= transform.right;
        if (Keyboard.current.dKey.isPressed) move += transform.right;
        if (Keyboard.current.rKey.isPressed) move += Vector3.up;
        if (Keyboard.current.fKey.isPressed) move -= Vector3.up;

        transform.position += move * moveSpeed * Time.deltaTime;

        // --- Обертання по Y (Q/E) ---
        if (Keyboard.current.qKey.isPressed) yaw -= rotationSpeed * Time.deltaTime;
        if (Keyboard.current.eKey.isPressed) yaw += rotationSpeed * Time.deltaTime;

        // pitch залишаємо без змін (нахил камери)
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}