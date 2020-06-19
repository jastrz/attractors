using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float mouseSpeed = 5f;
    [SerializeField] private float keyboardSpeed = 0.5f;
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private Renderer target;
    [SerializeField] private Solver solver;
    private float distance;
    private Vector3 move;
    private Camera cam;

    Vector3 verticalMovement => Vector3.forward * keyboardSpeed * Time.deltaTime;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (solver.IsStepSolverRunning)
            return;

        move = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            move += verticalMovement;
        if (Input.GetKey(KeyCode.S))
            move -= verticalMovement;

        // Zoom
        move += Input.mouseScrollDelta.y * Vector3.forward * scrollSpeed * Time.deltaTime;
        transform.Translate(move);
        distance = Vector3.Distance(transform.position, target.bounds.center);

        if (Input.GetMouseButton(1))
        {
            transform.Translate(-cam.transform.up * Input.GetAxisRaw("Mouse Y") * mouseSpeed * Time.deltaTime, Space.World);
            transform.Translate(-cam.transform.right * Input.GetAxisRaw("Mouse X") * mouseSpeed * Time.deltaTime, Space.World);
        }

        transform.LookAt(target.bounds.center);
        float newDistance = Vector3.Distance(transform.position, target.bounds.center);
        transform.position += (transform.position - target.bounds.center).normalized * (distance - newDistance);
    }
}
