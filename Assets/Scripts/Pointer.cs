using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float distance = 10.0f;
    public LineRenderer lineRenderer = null;
    public LayerMask everythingMask = 0;
    public LayerMask interactableMask = 0;

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
    }

    private Vector3 UpdateLine()
    {
        // Create ray
        //RaycastHit hit = CreateRaycast(everythingMask);

        // Default end
        Vector3 endPosition;

        // Check hit

        // Set position

        return Vector3.zero;
    }

    private void UpdateOrigin(OVRInput.Controller controller, GameObject contollerObject)
    {

    }

    /*private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        return hit;
    }*/

    private void SetLineColor()
    {

    }

    private void ProcessTouchpadDown()
    {

    }
}
