using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DRoom : MonoBehaviour {

    public Vector3 mSize;

    public Vector3 CenterPos { get; set; }

    public Dictionary<CollisionBehaviour, DRoom> JointsDict { get { return this.mJointsDict; } }

    Dictionary<CollisionBehaviour, DRoom> mJointsDict = new Dictionary<CollisionBehaviour, DRoom>();

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    }

    #region call by Mgr
    public bool Init()
    {
        Transform jointRootTrans = transform.Find("Joints");
        if (jointRootTrans == null || jointRootTrans.childCount == 0)
        {
            Debug.LogError("!!!!!! This Room is no Joint !!!!!!");
            return false;
        }

        int size = jointRootTrans.childCount;
        for (int i = 0; i < size; ++i)
        {
            Transform childTrans = jointRootTrans.GetChild(i);
            CollisionBehaviour cb = childTrans.gameObject.AddComponent<CollisionBehaviour>();
            mJointsDict.Add(cb, null);
        }
        return true;
    }

    public void OnEnterRoom()
    {
        //put camera at center
        gameObject.SetActive(true);
        SCameraMgr.Instance.SetCameraRootPos(CenterPos);
    }

    public void OnExitRoom()
    {
        //gameObject.SetActive(false);
    }
    #endregion

    #region public method
    public DRoom GetJointRoom(CollisionBehaviour cb)
    {
        DRoom room = null;
        mJointsDict.TryGetValue(cb, out room);
        return room;
    }

    public void AlignmentOtherRoom(CollisionBehaviour otherCb)
    {
        int jointSize = mJointsDict.Count;
        CollisionBehaviour myAligCb = new List<CollisionBehaviour>(mJointsDict.Keys)[Random.Range(0, jointSize)];
        print(CenterPos + "-" + myAligCb.transform.position + " + " + otherCb.transform.position);
        CenterPos = CenterPos - myAligCb.transform.position + otherCb.transform.position;
        transform.position = CenterPos;
    }

    public void JointRoom(CollisionBehaviour jointCb, DRoom jointRoom)
    {
        if (mJointsDict.ContainsKey(jointCb) == true)
        {
            mJointsDict[jointCb] = jointRoom;
        }
    }
    #endregion
}
