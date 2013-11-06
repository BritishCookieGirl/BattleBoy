using UnityEngine;
using System.Collections;

public class OneWayTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collide)
    {
        Transform platform = transform.parent;
        if(collide.tag == "Player" || collide.tag == "Enemy")
        {
            Physics.IgnoreCollision(collide.GetComponent<CharacterController>(), platform.GetComponent<BoxCollider>());
        }
        //else if(collide.tag == "Enemy")
        //{
        //    Physics.IgnoreCollision(collide.GetComponent<BoxCollider>(), platform.GetComponent<BoxCollider>());
        //}

        //if (collide.tag == "Player" || collide.tag == "Enemy")
        //{
        //    transform.parent.collider.isTrigger = false;
        //}
    }

    void OnTriggerExit(Collider collide)
    {
        // Reset collider's layer to something the platform collides with
        // just in case we wanted to jump through this one
        collide.gameObject.layer = 0;

        // Re-enable collision between jumper and parent platform so we can stand on top again
        Transform platform = transform.parent;

        if(collide.tag == "Player" || collide.tag == "Enemy")
        {
            Physics.IgnoreCollision(collide.GetComponent<CharacterController>(), platform.GetComponent<BoxCollider>(), false);
        }
        //else if(collide.tag == "Enemy")
        //{
        //    Physics.IgnoreCollision(collide.GetComponent<BoxCollider>(), platform.GetComponent<BoxCollider>(), false);
        //}

        //if (collide.tag == "Player" || collide.tag == "Enemy")
        //{
        //    transform.parent.collider.isTrigger = true;
        //}
    }
}