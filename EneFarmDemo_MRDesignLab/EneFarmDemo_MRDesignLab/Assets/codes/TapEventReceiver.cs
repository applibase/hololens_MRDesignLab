using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using HUX.Focus;
using HUX.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapEventReceiver : MonoBehaviour {

    public GameObject toolbarObj;
    public GameObject obj;

    // Use this for initialization
    void Start () {

        InteractionManager.OnTapped += TappedCallBack;
    }
	
	// Update is called once per frame
	void Update () {

        //GameObject cursor = FocusManager.Instance.GazeFocuser.Cursor.gameObject;
        //getType(cursor);
    }

    private void TappedCallBack(GameObject go, InteractionManager.InteractionEventArgs e)
    {

        if (go != null && go == obj)
        {
            modeEvent();
            return;
        }

        setCube(go);
               
    }

    private void modeEvent()
    {
        var toolbar = toolbarObj.GetComponent<ToolBar>();

        switch (toolbar.state)
        {
            case ToolBar.ToolBarState.Adjust:
                return;
            case ToolBar.ToolBarState.Change:
                return;
            case ToolBar.ToolBarState.Gaze:
                obj.layer = LayerMask.NameToLayer("Ignore Raycast");
                obj.GetComponent<GazeMove>().isAllowMoved = true;
                return;
            case ToolBar.ToolBarState.Hide:
                toolbarObj.SetActive(true);
                toolbar.state = ToolBar.ToolBarState.None;
                return;

        }
    }

    private String getType(GameObject o)
    {

        Vector3 rayPos = o.transform.position;
        Vector3 rayVec = o.transform.forward * 10.0f;

        IntPtr raycastResultPtr = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticRaycastResultPtr();
        SpatialUnderstandingDll.Imports.PlayspaceRaycast(
            rayPos.x,
            rayPos.y,
            rayPos.z,
            rayVec.x,
            rayVec.y,
            rayVec.z,
            raycastResultPtr);

        SpatialUnderstandingDll.Imports.RaycastResult rayCastResult = SpatialUnderstanding.Instance.UnderstandingDLL.GetStaticRaycastResult();
        return rayCastResult.SurfaceType.ToString();

    }
    

    private void setCube(GameObject hitObject)
    {
        String type = getType(hitObject);
        String type2 = getType(FocusManager.Instance.GazeFocuser.FocusHitInfo.gameObject);

        Debug.Log(type);
        Debug.Log(type2);

        if (type != null && (type.Equals("Floor") || type.Equals("FloorLike")))
        {
            obj.layer = LayerMask.NameToLayer("Default");
            obj.GetComponent<GazeMove>().isAllowMoved = false;
            return;
        }


        var hitInfo = FocusManager.Instance.GazeFocuser.FocusHitInfo;
        
        if (hitInfo != null)
        {
            GameObject surface = hitInfo.transform.gameObject;

            if (surface == null)
            {
                return;
            }

            SurfacePlane plane = surface.GetComponent<SurfacePlane>();

            if (plane == null)
            {
                return;
            }

            if (plane.PlaneType == PlaneTypes.Floor)
            {
                obj.layer = LayerMask.NameToLayer("Default");
                obj.GetComponent<GazeMove>().isAllowMoved = false;
                return;
            }

        }
    }
}
