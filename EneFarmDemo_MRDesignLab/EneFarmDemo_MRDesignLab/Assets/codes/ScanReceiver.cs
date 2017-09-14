using HoloToolkit.Unity;
using HUX.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanReceiver : MonoBehaviour
{
    private bool isEnableScan = true;

    private void OnEnable()
    {

        var button = GetComponent<CompoundButton>();
        button.OnButtonReleased += ButtonReleased;
    }

    private void OnDisable()
    {
        var button = GetComponent<CompoundButton>();
        button.OnButtonReleased -= ButtonReleased;
    }

    private void ButtonReleased(GameObject obj)
    {
        Debug.Log("Scan is tapped!");

        if (isEnableScan)
        {
            SpatialUnderstanding.Instance.RequestFinishScan();
            GetComponent<CompoundButtonText>().Text = "Scan Start";       
            isEnableScan = false;

            return;
        }

        SpatialUnderstanding.Instance.RequestBeginScanning();
        GetComponent<CompoundButtonText>().Text = "Scan End";
        isEnableScan = true;
    }
}
