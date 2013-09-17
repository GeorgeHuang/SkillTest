using UnityEngine;
using System.Collections;

public class InputReceiver : MonoBehaviour {
    public Vector2 MoveAxis = Vector2.zero;
    public bool Attack = false;
    public bool Block = false;

    SMovableObject m_movableObject;

    public SMovableObject MovableObject { get { return this.m_movableObject; } set { m_movableObject = value; } }

    void FixedUpdate()
    {
        if (m_movableObject != null)
        {
            m_movableObject.Move(MoveAxis.x, MoveAxis.y);
        }
    }
}
