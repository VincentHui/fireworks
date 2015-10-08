using UnityEngine;
using System.Collections;

public class DeleteDelay : MonoBehaviour
{
    public float delay;

	// Use this for initialization
	void Start () 
    {
        Invoke("DeleteAfterDelay", delay);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void DeleteAfterDelay()
    {
        Destroy(gameObject);
    }
}
