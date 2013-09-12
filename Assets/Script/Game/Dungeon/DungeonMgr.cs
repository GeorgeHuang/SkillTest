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
    bool mRoleCurSceneEnable = false;

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
        StartCoroutine(nextRoomCoroutine(cb));
    }

    IEnumerator nextRoomCoroutine(CollisionBehaviour cb)
    {
        SMovableObject mainRole = MainGameData.MainRole;
        mainRole.enabled = false;
        mainRole.SetKinematic(true);
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
            CollisionBehaviour nextCb = nextRoom.AlignmentOtherRoom(cb, mCurRoom.CenterPos);
            mCurRoom.JointRoom(cb, nextRoom);
            nextRoom.JointRoom(nextCb, mCurRoom);
        }
        StartCoroutine(mainRoleCutScene(nextRoom.CenterPos));
        yield return StartCoroutine(SCameraMgr.Instance.moveToPos(nextRoom.CenterPos));
        while (mRoleCurSceneEnable == true)
        {
            yield return null;
        }
        mCurRoom = nextRoom;
        registerEvent();
        mCurRoom.OnEnterRoom();
        mainRole.SetKinematic(false);
        mainRole.enabled = true;
    }

    IEnumerator mainRoleCutScene(Vector3 pos)
    {
        mRoleCurSceneEnable = true;
        SMovableObject mainRole = MainGameData.MainRole;
        while( (mainRole.CurPos - pos).sqrMagnitude > 0.01f )
        {
            mainRole.MoveTo(Vector3.Lerp(mainRole.CurPos, pos, 0.1f));
            yield return null;
        }
        mainRole.MoveTo(pos);
        mRoleCurSceneEnable = false;
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
