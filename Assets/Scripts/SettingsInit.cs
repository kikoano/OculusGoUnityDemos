using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInit : MonoBehaviour
{
    private void Start()
    {
        UnityEngine.XR.XRSettings.eyeTextureResolutionScale = 2.0f;
        if (OVRManager.tiledMultiResSupported)
        {
            //OVRManager.tiledMultiResLevel = OVRManager.TiledMultiResLevel.LMSMedium;
        }

        float displayFreq = OVRManager.display.displayFrequency;
        foreach (float freq in OVRManager.display.displayFrequenciesAvailable)
        {
            if (freq > displayFreq)
            {
                displayFreq = freq;
            }
        }
        OVRManager.display.displayFrequency = 72.0f;
    }
}
