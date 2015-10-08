using UnityEngine;
using System.Collections;

public class DeleteDelay : MonoBehaviour
{
    public float delay;

	// Use this for initialization
	void Start () 
    {
        StartCoroutine(DeleteAfterDelay(delay));
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    IEnumerator DeleteAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(gameObject);
    }
}
