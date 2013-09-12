using UnityEngine;
using System.Collections;

public class SMovableObject : MonoBehaviour {

    public float mMoveRatio = 0.1f;

    Transform mTrans;
    Vector3 mInputValue = Vector3.zero;
    Vector3 mCurTempPos = Vector3.zero;

    public Vector3 CurPos { get { return mTrans.position; } }

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

    public void SetKinematic(bool val)
    {
        rigidbody.isKinematic = val;

        if (collider != null)
        {
            collider.enabled = !val;
        }

        Collider[] array = gameObject.GetComponentsInChildren<Collider>();
        foreach ( Collider cd in array )
        {
            cd.enabled = !val;
        }

    }
    #endregion

    #region private method
    void ApplyInput ()
    {
        if (mInputValue.magnitude < 0.1f)
            return;
        mCurTempPos = mTrans.position;

        mCurTempPos.x += mInputValue.x * mMoveRatio;
        mCurTempPos.z += mInputValue.y * mMoveRatio;

        mTrans.position = mCurTempPos;
    }
    #endregion
}