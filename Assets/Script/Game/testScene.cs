using UnityEngine;
using System.Collections;

public class testScene : MonoBehaviour {

    public GameObject mMainRoleModel;
    public GameObject DungeonMgrPrefab;

    SInput mInput;

    // Use this for initialization
    void Start () {
        mInput = this.gameObject.AddComponent<SInput>();
        SMovableObject moveObj = mMainRoleModel.AddComponent<SMovableObject>();
        mInput.MoveObj = moveObj;

        //Camera init
        gameObject.AddComponent<SCameraMgr>();

        //Dungeon init
        GameObject go = Instantiate(DungeonMgrPrefab) as GameObject;
        Common.changGOParent(go, gameObject);

        moveObj.MoveTo(DungeonMgr.Instance.CurRoomPos());

        //MainGameData init
        MainGameData.MainRole = moveObj;
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
