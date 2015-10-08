using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CollectableManager : MonoBehaviour {

    public GameObject[] CollectableObjects;
    public List<GameObject> BuildingsToAvoid;
    public int NumOfCollectables;

    List<GameObject> SpawnedCollectables = new List<GameObject>();
    private static AudioSource audSource;

    // Use this for initialization
    void Start () {
        if (this.GetComponent<AudioSource>() != null)
            audSource = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetUpCollectables(List<GameObject> Buildings)
    {
        List<int> selectedBuildings = new List<int>();

        for(int i = 0; i < NumOfCollectables; i++)
        {
            int nextB = 0;
            bool newBuildingFound = false;

            do
            {
                nextB = Random.Range(0, Buildings.Count);

                if(BuildingsToAvoid.Contains(Buildings[nextB]))
                    selectedBuildings.Add(nextB);
                else if (!selectedBuildings.Contains(nextB))
                {
                    newBuildingFound = true;
                    selectedBuildings.Add(nextB);
                }
            }
            while (!newBuildingFound);

            GameObject newBook = CollectableObjects[Random.Range(0, CollectableObjects.Length)];
            SpawnedCollectables.Add((GameObject)Instantiate(newBook, Buildings[nextB].GetComponent<BuildingIDScript>().LandingPoint.transform.position, newBook.transform.rotation));
            SpawnedCollectables[SpawnedCollectables.Count - 1].transform.parent = this.transform;
        }
    }

    internal static void OnCollected()
    {
        audSource.Play();
    }
}
