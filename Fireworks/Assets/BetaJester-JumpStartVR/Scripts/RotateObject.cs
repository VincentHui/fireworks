using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
    public float rotationSpeed = 10;
    public bool yAxis;
    public bool xAxis;
    public bool zAxis;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (yAxis)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + rotationSpeed * Time.deltaTime, transform.localRotation.eulerAngles.z);

        if (xAxis)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x + rotationSpeed * Time.deltaTime, transform.localRotation.eulerAngles.y , transform.localRotation.eulerAngles.z);

        if (zAxis)
            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y + rotationSpeed * Time.deltaTime, transform.localRotation.eulerAngles.z + rotationSpeed * Time.deltaTime);
    }
}
