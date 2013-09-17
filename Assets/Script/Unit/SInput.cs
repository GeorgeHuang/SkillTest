using UnityEngine;
using System.Collections;

public class SInput : MonoSingleton<SInput>{

    InputReceiver m_inputReceiver;

    #region GetSet
    public InputReceiver InputReceiver { get { return this.m_inputReceiver; } set { m_inputReceiver = value; } }
    #endregion

    #region MonoBehaviour
    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        if (m_inputReceiver != null)
        {
            m_inputReceiver.MoveAxis.x = Input.GetAxis("Horizontal");
            m_inputReceiver.MoveAxis.y = Input.GetAxis("Vertical");
            m_inputReceiver.Attack = Input.GetButtonDown("Fire1");
            m_inputReceiver.Block = Input.GetButtonDown("Fire2");
        }
    }
    #endregion
}
