using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.SpatialMapping;
using HUX.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpatialProcess : MonoBehaviour {

    private bool isCreated = false;
    // Use this for initialization
    void Start()
    {
        print("start");

        //	壁とか作り終わったら教えて
        SurfaceMeshesToPlanes.Instance.MakePlanesComplete += SurfaceMeshesToPlanes_MakePlanesComplete;
        InteractionManager.OnTapped += OnInputClicked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInputClicked(GameObject go, InteractionManager.InteractionEventArgs e)
    {
        if (!isCreated) {
            print("click");
            //	エアタップされたらメッシュ作るの止めて、壁とか作る
            SpatialMappingManager.Instance.StopObserver();
            SurfaceMeshesToPlanes.Instance.MakePlanes();
            isCreated = true;
        }
    }

    //	壁とか出来たら呼ばれる	
    private void SurfaceMeshesToPlanes_MakePlanesComplete(object source, System.EventArgs args)
    {
        print("end");
        //	平面に内包されてる頂点を削除して軽くするみたい
        RemoveSurfaceVertices.Instance.RemoveSurfaceVerticesWithinBounds(SurfaceMeshesToPlanes.Instance.ActivePlanes);

        //	メッシュが邪魔なので消しちゃう
        SpatialMappingManager.Instance.DrawVisualMeshes = false;

    }
}
