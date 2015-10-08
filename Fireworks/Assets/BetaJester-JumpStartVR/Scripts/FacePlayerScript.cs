using UnityEngine;
using System.Collections;

public class FacePlayerScript : MonoBehaviour {

    Transform target;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;

	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            transform.LookAt(target, Vector3.up);
            transform.Rotate(0, 180,0);
        }
    }
}
