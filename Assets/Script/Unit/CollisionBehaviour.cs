using UnityEngine;
using System.Collections;

public class CollisionBehaviour : MonoBehaviour {

    public System.Action<CollisionBehaviour> OnContact;

    void OnCollisionEnter(Collision collision)
    {
        if (OnContact != null)
            OnContact(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (OnContact != null)
            OnContact(this);
    }
}
