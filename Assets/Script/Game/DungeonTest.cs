using UnityEngine;
using System.Collections;

public class DungeonTest : MonoBehaviour {

    public GameObject MainRoleModel;
    public GameObject DungeonMgrPrefab;

    SInput mInput;

    // Use this for initialization
    void Start () {
        mInput = this.gameObject.AddComponent<SInput>();
        InputReceiver inputRecObj = MainRoleModel.AddComponent<InputReceiver>();
        SMovableObject moveObj = MainRoleModel.AddComponent<SMovableObject>();
        inputRecObj.MovableObject = moveObj;
        mInput.InputReceiver = inputRecObj;

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
