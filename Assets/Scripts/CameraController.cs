using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 5f;
    [SerializeField] float keyboardSpeed = 0.5f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private Renderer target;

    Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            move += Vector3.forward * keyboardSpeed;
        if (Input.GetKey(KeyCode.S))
            move -= Vector3.forward * keyboardSpeed;
        if (Input.GetKey(KeyCode.D))
            move += Vector3.right * keyboardSpeed;
        if (Input.GetKey(KeyCode.A))
            move -= Vector3.right * keyboardSpeed;
        if (Input.GetKey(KeyCode.E))
            move += Vector3.up * keyboardSpeed;
        if (Input.GetKey(KeyCode.Q))
            move -= Vector3.up * keyboardSpeed;

        move += Input.mouseScrollDelta.y * Vector3.forward * scrollSpeed;
        transform.Translate(move);

        if (Input.GetMouseButton(1))
        {
            transform.Translate(-cam.transform.up * Input.GetAxisRaw("Mouse Y") * mouseSpeed, Space.World);
            transform.Translate(-cam.transform.right * Input.GetAxisRaw("Mouse X") * mouseSpeed, Space.World);
        }
        
        transform.LookAt(target.bounds.center);
    }
}
