using UnityEngine;
using System.Collections;

public class SkillTest : MonoBehaviour {

    public GameObject MainRoleModel;
    
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
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
