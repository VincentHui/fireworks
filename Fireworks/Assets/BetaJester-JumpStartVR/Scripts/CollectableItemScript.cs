using UnityEngine;
using System.Collections;

public class CollectableItemScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

      
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<ItemCollectorScript>().ItemCollected(this.tag);

            CollectableManager.OnCollected();

            Destroy(this.gameObject);
        }
    }
}
