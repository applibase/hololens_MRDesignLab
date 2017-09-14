using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using HUX.Focus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeMove : MonoBehaviour
{

    private RaycastHit hitInfo;
    public bool isAllowMoved { get; set; }

    // Use this for initialization
    void Start()
    {
        isAllowMoved = false;
    }

    // Update is called once per frame
    void Update()
    {

        var toolbarObj = GameObject.Find("ToolBar");

        if (toolbarObj == null)
        {
            return;
        }

        var toolbar = toolbarObj.GetComponent<ToolBar>();

        if (toolbar.state != ToolBar.ToolBarState.Gaze || !isAllowMoved)
        {
            return;
        }

        trackingCube();
    }

    private void trackingCube()
    {

        var hitInfo = FocusManager.Instance.GazeFocuser.FocusHitInfo;

        if (hitInfo != null)
        {
            GameObject cursor = FocusManager.Instance.GazeFocuser.Cursor.gameObject;

            Vector3 postion = cursor.transform.position;
            postion.y = postion.y + (gameObject.transform.localScale.y / 2.0f);

            gameObject.transform.position = postion;

        }


    }
}
