using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HUX.Interaction;

public class ToolBar : MonoBehaviour
{

    public GameObject adjust;
    public GameObject change;
    public GameObject gaze;
    public GameObject hide;

    public enum ToolBarState
    {
        Adjust, Change, Gaze, Hide, None
    }

    public ToolBarState state { get; set; } 

    void Start()
    {
        state = ToolBarState.Adjust;
    }

    public void changed()
    {
        switch (state)
        {
            case ToolBarState.Adjust:
                var boundingBox = ManipulationManager.Instance.ActiveBoundingBox;
                boundingBox.AcceptInput = false;

                if (GameObject.Find("Drag") != null)
                {
                    GameObject.Find("Drag").layer = LayerMask.NameToLayer("Ignore Raycast");
                }
               
                return;

            case ToolBarState.Gaze:
                var objj = GameObject.Find("Cube");
                objj.layer = LayerMask.NameToLayer("Default");
                objj.GetComponent<GazeMove>().isAllowMoved = false;

                return;
        }
    }
    

}
