using UnityEngine;

//control camera view
public class CameraController : MonoBehaviour
{
    public Transform target;

    //offset from target
    public Vector3 offset;
    public float zoomSpeed = 4f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    public float heightOffset = 2f;

    public float cameraRotateSpeed = 100f;

    private float currentZoom = 10f;
    private float cameraRotate = 0f;

    private void Update()
    {
        //zoom in and out with mouse wheel
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
        //rotate with keyboard
        cameraRotate -= Input.GetAxis("Horizontal") * cameraRotateSpeed * Time.deltaTime;
    }

    private void LateUpdate()
    {
        transform.position = target.position - offset * currentZoom;
        transform.LookAt(target.position + Vector3.up * heightOffset);
        transform.RotateAround(target.position, Vector3.up, cameraRotate);
    }
}
