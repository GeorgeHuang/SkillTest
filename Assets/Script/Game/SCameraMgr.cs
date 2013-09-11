using UnityEngine;
using System.Collections;

public class SCameraMgr : MonoSingleton<SCameraMgr> {

    Camera mMainCamera;
    GameObject mCameraRootGO;

    public Camera MainCamera { get { return this.mMainCamera; } set { mMainCamera = value; } }

    // Use this for initialization
    void Start () {

    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public override void Init ()
    {
        base.Init ();
        mMainCamera = Camera.main;
        mCameraRootGO = new GameObject("CameraRoot");
        mMainCamera.transform.parent = mCameraRootGO.transform;
    }

    public void SetCameraRootPos(Vector3 pos)
    {
        mCameraRootGO.transform.position = pos;
    }
}
