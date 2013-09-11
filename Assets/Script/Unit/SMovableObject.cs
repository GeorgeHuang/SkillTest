using UnityEngine;
using System.Collections;

public class SMovableObject : MonoBehaviour {

    public float mMoveRatio = 0.1f;

    Transform mTrans;
    Vector3 mInputValue = Vector3.zero;
    Vector3 mCurPos = Vector3.zero;

    #region MonoBehaviour
    void Awake()
    {
        mTrans = transform;
    }
    void Start()
    {
    }

    void FixedUpdate()
    {
        ApplyInput();
    }
    #endregion

    #region public method
    public void Move(float x, float y)
    {
        mInputValue.x = x;
        mInputValue.y = y;
    }

    public void MoveTo(Vector3 pos)
    {
        mTrans.position = pos;
    }
    #endregion

    #region private method
    void ApplyInput ()
    {
        if (mInputValue.magnitude < 0.1f)
            return;
        mCurPos = mTrans.position;

        mCurPos.x += mInputValue.x * mMoveRatio;
        mCurPos.z += mInputValue.y * mMoveRatio;

        mTrans.position = mCurPos;
    }
    #endregion
}