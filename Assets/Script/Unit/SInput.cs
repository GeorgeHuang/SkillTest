using UnityEngine;
using System.Collections;

public class SInput : MonoSingleton<SInput>{

    float x, y;
    SMovableObject mMoveObj;

    #region GetSet
    public SMovableObject MoveObj { get { return this.mMoveObj; } set { mMoveObj = value; } }
    #endregion

    #region MonoBehaviour
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        if (mMoveObj != null)
        {
            mMoveObj.Move(x, y);
        }
    }
    #endregion
}
