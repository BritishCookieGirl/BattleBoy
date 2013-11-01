using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour
{
    public float dampTime = 0.15f;
    public Transform target;
    public float yOffset = 0f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 cameraOffset;

	// Use this for initialization
	void Start () {
        cameraOffset = new Vector3(0.0f, yOffset, 0.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target)
        {
            Vector3 point = camera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination + cameraOffset, ref velocity, dampTime);
        }
        this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, -14f, 4.5f), Mathf.Clamp(transform.position.y, 2.5f, 15.0f), 0);
	}
}
