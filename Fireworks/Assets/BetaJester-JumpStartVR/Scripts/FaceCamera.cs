using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour
{
    Transform target;
    Transform localTransform;

    public float x;
    public float y;
    public float z;

	void Start () 
    {
        localTransform = this.transform;
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
	void Update ()
    {
        localTransform.LookAt(target);
        localTransform.Rotate(x, y, z);
	}
}
