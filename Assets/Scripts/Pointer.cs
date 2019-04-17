using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pointer : MonoBehaviour
{
    public float distance = 10.0f;
    public LineRenderer lineRenderer = null;
    public LayerMask everythingMask = 0;
    public LayerMask interactableMask = 0;
    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;

    private Transform currentOrigin = null;
    private GameObject currentObject = null;

    private void Awake()
    {
        PlayerEvents.OnControllerSource += UpdateOrigin;
        PlayerEvents.OnTouchpadDown += ProcessTouchpadDown;
    }

    private void Start()
    {
        SetLineColor();
    }

    private void OnDestroy()
    {
        PlayerEvents.OnControllerSource -= UpdateOrigin;
        PlayerEvents.OnTouchpadDown -= ProcessTouchpadDown;
    }

    private void Update()
    {
        Vector3 hitPoint = UpdateLine();

        currentObject = UpdatePointerStatus();

        if (OnPointerUpdate != null)
            OnPointerUpdate(hitPoint, currentObject);
    }

    private Vector3 UpdateLine()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(everythingMask);

        // Default end
        Vector3 endPosition = currentOrigin.position + (currentOrigin.forward * distance);

        // Check hit
        if (hit.collider != null)
            endPosition = hit.point;

        // Set position
        lineRenderer.SetPosition(0, currentOrigin.position);
        lineRenderer.SetPosition(1, endPosition);

        return endPosition;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject contollerObject)
    {
        // Set origin of pointer
        currentOrigin = contollerObject.transform;

        // Is the laser visiable?
        if (controller == OVRInput.Controller.Touchpad)
            lineRenderer.enabled = false;
        else
            lineRenderer.enabled = true;
    }
    private GameObject UpdatePointerStatus()
    {
        // Create ray
        RaycastHit hit = CreateRaycast(interactableMask);

        // Check hit
        if (hit.collider)
            return hit.collider.gameObject;
        // Return
        return null;
    }

    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(currentOrigin.position, currentOrigin.forward);
        Physics.Raycast(ray, out hit, distance, layer);

        return hit;
    }

    private void SetLineColor()
    {
        if (!lineRenderer)
            return;
        Color endColor = Color.white;
        endColor.a = 0.0f;

        lineRenderer.endColor = endColor;
    }

    private void ProcessTouchpadDown()
    {
        if (!currentObject)
            return;

        Interactable interactable = currentObject.GetComponent<Interactable>();
        interactable.Pressed();
    }
}
