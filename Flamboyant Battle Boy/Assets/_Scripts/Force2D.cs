using UnityEngine;

public class Force2D : MonoBehaviour
{
    private Transform trans;
    private Rigidbody body;
    //historical x-position
    private float x;

    public void Start()
    {
        //cashe components for good performance
        trans = transform;
        body = rigidbody;

        x = trans.position.x;
    }

    public void FixedUpdate()
    {
        //reset x-position
        Vector3 pos = trans.position;
        pos.x = x;
        trans.position = pos;

        //clear Y and Z rotation
        Vector3 dir = trans.localRotation * Vector3.forward;
        dir.x = 0;
        trans.localRotation = Quaternion.LookRotation(dir, -Vector3.Cross(Vector3.right, dir));

        //clear X-velocity
        Vector3 vel = body.velocity;
        vel.x = 0;
        body.velocity = vel;

        //clear Y and Z angular velocity
        body.angularVelocity = new Vector3(body.angularVelocity.x, 0, 0);
    }
}