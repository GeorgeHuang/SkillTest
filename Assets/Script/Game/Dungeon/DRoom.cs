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
    }

    public void OnExitRoom()
    {
    }
    #endregion

    #region public method
    public DRoom GetJointRoom(CollisionBehaviour cb)
    {
        DRoom room = null;
        mJointsDict.TryGetValue(cb, out room);
        return room;
    }

    public CollisionBehaviour AlignmentOtherRoom(CollisionBehaviour otherCb, Vector3 otherCenter)
    {
        int jointSize = mJointsDict.Count;
        CollisionBehaviour myAligCb = new List<CollisionBehaviour>(mJointsDict.Keys)[Random.Range(0, jointSize)];
        Vector3 tarDir = -(otherCb.transform.position - otherCenter);
        Vector3 curDir = myAligCb.transform.position - CenterPos;
        float sign = Vector3.Dot(Vector3.up, Vector3.Cross(tarDir,curDir)) < 0 ? 1 : -1;
        float angle = Vector3.Angle(tarDir,curDir);
        transform.Rotate(0, angle * sign, 0);
        CenterPos = CenterPos - myAligCb.transform.position + otherCb.transform.position;
        transform.position = CenterPos;
        return myAligCb;
    }

    public void JointRoom(CollisionBehaviour jointCb, DRoom jointRoom)
    {
        if (mJointsDict.ContainsKey(jointCb) == true)
        {
            mJointsDict[jointCb] = jointRoom;
        }
    }
    #endregion


    void setTriggerEnable(bool enable)
    {
        foreach(CollisionBehaviour cb in mJointsDict.Keys)
        {
            cb.enabled = enable;
        }
    }
}
