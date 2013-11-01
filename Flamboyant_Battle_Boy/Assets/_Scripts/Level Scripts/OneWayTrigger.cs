﻿using UnityEngine;
using System.Collections;

public class OneWayTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider collide)
    {
        if (collide.tag == "Player" || collide.tag == "Enemy")
        {
            transform.parent.collider.isTrigger = false;
        }
    }

    void OnTriggerExit(Collider collide)
    {
        if (collide.tag == "Player" || collide.tag == "Enemy")
        {
            transform.parent.collider.isTrigger = true;
        }
    }
}
