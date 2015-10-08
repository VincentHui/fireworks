using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    Vector3 objectVector;

	// Use this for initialization
	void Start ()
    {
        objectVector = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.RotateAround(objectVector, Vector3.up, 20 * Time.deltaTime);
	}
}
