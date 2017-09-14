using HUX.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneReceiver : MonoBehaviour
{
    private bool isVisiblePlane = false;

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
        Debug.Log("Plane is tapped!");

        if (isVisiblePlane)
        {
            GetComponent<CompoundButtonText>().Text = "Plane On";
            isVisiblePlane = false;

            return;
        }

        GetComponent<CompoundButtonText>().Text = "Plane Off";
        isVisiblePlane = true;
    }
}
