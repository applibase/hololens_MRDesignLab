using HUX.Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeReceiver : MonoBehaviour
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
        toolBar.state = ToolBar.ToolBarState.Change;
       

        Debug.Log("Change is tapped!");
    }
}
