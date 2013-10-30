using UnityEngine;
using System.Collections;

public class SmoothCameraFollow : MonoBehaviour
{
    //@script AddComponentMenu("Camera-Control/Smooth Look At");

    public Transform target;

    public float damping = 6.0f;
    public bool enableCameraPivot = true;
    public bool smoothPivot = true;

    //Movement speed in units/sec
    public float moveSpeed = 1.0f;

    //Transforms to act as start and end markers for the journey
    private Vector3 startPoint;
    private Vector3 endPoint;

    //Time when the movement started
    private float startTime;
    private float elapsedTime;

    //Total distance between the markers
    private float journeyLength;

    private Vector3 cameraOffset;

	// Use this for initialization
	void Start ()
    {
        cameraOffset = new Vector3(0f, 1f, 0f);

        // Make the rigid body not change rotation
        if (rigidbody)
            rigidbody.freezeRotation = true;

        //Keep a note of the time the movement started.
        startTime = Time.time;
        //elapsedTime = 0;

        //startPoint = transform.position;
        endPoint = new Vector3(target.position.x, target.position.y, -target.position.z) + cameraOffset;

        //Calculates the journey length;
        journeyLength = Vector3.Distance(startPoint, endPoint);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Follows the target position like with a spring

        if ((transform.position == target.position + cameraOffset) && Time.time - startTime > 1000)
        {
            startPoint = transform.position;
            startTime = Time.time;
        }

        endPoint = new Vector3(target.position.x, target.position.y, -target.position.z) + cameraOffset;
        journeyLength = Vector3.Distance(startPoint, endPoint);

        //Distance moved = time * speed
        float distCovered = (Time.time - startTime) * moveSpeed;

        //Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        //Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startPoint, endPoint, fracJourney);
	}

    void LateUpdate()
    {
        if (enableCameraPivot)
        {
            if (smoothPivot)
            {
                // Look at and dampen the rotation
                Quaternion rotation = Quaternion.LookRotation(target.position + cameraOffset - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
            else
            {
                // Just lookat
                transform.LookAt(target.position + cameraOffset);
            }
        }
    }
}