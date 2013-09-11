using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonMgr: MonoSingleton<DungeonMgr> {

    #region prefab
    public GameObject StartRoom;
    public GameObject[] Rooms;
    #endregion

    List<GameObject> mRoomList = new List<GameObject>();

    DRoom mCurRoom;

    #region Mono
    void Start () {
        mCurRoom.OnEnterRoom();
    }
    
    // Update is called once per frame
    void Update () {
    
    }
    #endregion

    #region public method
    public override void Init ()
    {
        base.Init ();

        //init room
        GameObject go = Instantiate(StartRoom) as GameObject;
        mCurRoom = go.GetComponent<DRoom>();
        mCurRoom.CenterPos = Vector3.zero;
        mCurRoom.Init();
        registerEvent();
        mRoomList.Add(go);
    }

    public Vector3 CurRoomPos()
    {
        if (mCurRoom != null)
            return mCurRoom.CenterPos;
        return Vector3.zero;
    }
    #endregion

    #region private method
    void nextRoomTriggerEnter(CollisionBehaviour cb)
    {
        Common.sysPrint(" nextRoomTriggerEnter ");
        unregisterEvent();
        mCurRoom.OnExitRoom();

        //try get next room
        DRoom nextRoom = mCurRoom.GetJointRoom(cb);
        if (nextRoom == null)
        {
            //TODO: Be Random After
            GameObject go = Instantiate(Rooms[0]) as GameObject;
            nextRoom = go.GetComponent<DRoom>();
            nextRoom.Init();
            nextRoom.AlignmentOtherRoom(cb);
            nextRoom.JointRoom(cb, nextRoom);
        }

        mCurRoom = nextRoom;
        registerEvent();
        mCurRoom.OnEnterRoom();
    }

    void registerEvent()
    {
        if (mCurRoom == null)
            return;
        foreach(CollisionBehaviour cb in mCurRoom.JointsDict.Keys)
        {
            cb.OnContact += nextRoomTriggerEnter;
        }
    }

    void unregisterEvent()
    {
        if (mCurRoom == null)
            return;
        foreach(CollisionBehaviour cb in mCurRoom.JointsDict.Keys)
        {
            cb.OnContact -= nextRoomTriggerEnter;
        }
    }
    #endregion
}
