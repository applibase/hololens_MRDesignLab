using HUX.Buttons;
using HUX.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustReceiver : MonoBehaviour
{

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

        var toolBar = transform.parent.gameObject.GetComponent<ToolBar>();
        toolBar.changed();
        toolBar.state = ToolBar.ToolBarState.Adjust;

        Debug.Log("Adjust is tapped!");

        if (GameObject.Find("Drag") != null)
        {
            GameObject.Find("Drag").layer = LayerMask.NameToLayer("Default");
        }
        

        var boundingBox = ManipulationManager.Instance.ActiveBoundingBox;
        boundingBox.AcceptInput = true;
        GameObject.Find("Cube").GetComponent<BoundingBoxTarget>().Tapped();

        
    }
}
