using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{
    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDown = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion
    #region Anchors
    public GameObject leftAnchor;
    public GameObject rightAnchor;
    public GameObject headAnchor;
    #endregion

    #region Input
    private Dictionary<OVRInput.Controller, GameObject> controllerSets = null;
    private OVRInput.Controller inputSource = OVRInput.Controller.None;
    private OVRInput.Controller controller = OVRInput.Controller.None;
    private bool inputActive = true;
    #endregion

    private void Awake()
    {
        OVRManager.HMDMounted += PlayerFound;
        OVRManager.HMDUnmounted += PlayerLost;

        controllerSets = CreateControllerSets();
    }

    private void OnDestroy()
    {
        OVRManager.HMDMounted -= PlayerFound;
        OVRManager.HMDUnmounted -= PlayerLost;
    }

    private void Update()
    {
        // Check for active input
        if (!inputActive)
            return;
        // Check if controller exists
        CheckForController();
        // Check for input source
        CheckInputSource();
        // Check for actual input
        Input();
    }

    private void CheckForController()
    {
        OVRInput.Controller controllerCheck = controller;

        // Right remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote))
            controllerCheck = OVRInput.Controller.RTrackedRemote;
        // Left remote
        if (OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.LTrackedRemote;
        // If no controllers, headset
        if (!OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote) && !OVRInput.IsControllerConnected(OVRInput.Controller.LTrackedRemote))
            controllerCheck = OVRInput.Controller.Touchpad;

        // Update
        controller = UpdateSource(controllerCheck, controller);
    }

    private void CheckInputSource()
    {
        // Update
        inputSource = UpdateSource(OVRInput.GetActiveController(), inputSource);
    }

    private void Input()
    {
        // GRVR Touchpad down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadDown != null)
                OnTouchpadDown();
        }
        // GRVR Touchpad up
        if (OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad))
        {
            if (OnTouchpadUp != null)
                OnTouchpadUp();
        }
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {
        // If values are the same, return
        if (check == previous)
            return previous;

        // Get contoller object
        GameObject controllerObject = null;
        controllerSets.TryGetValue(check, out controllerObject);

        // If no contoller, set to the head
        if (controllerObject == null)
            controllerObject = headAnchor;

        // Send out event
        if (OnControllerSource != null)
            OnControllerSource(check, controllerObject);

        // Return new Value
        return check;
    }

    private void PlayerFound()
    {
        inputActive = true;
    }

    private void PlayerLost()
    {
        inputActive = false;
    }

    private Dictionary<OVRInput.Controller, GameObject> CreateControllerSets()
    {
        Dictionary<OVRInput.Controller, GameObject> newSets = new Dictionary<OVRInput.Controller, GameObject>()
        {
            {OVRInput.Controller.LTrackedRemote, leftAnchor },
            {OVRInput.Controller.RTrackedRemote, rightAnchor },
            {OVRInput.Controller.Touchpad, headAnchor }
        };
        return newSets;
    }
}
