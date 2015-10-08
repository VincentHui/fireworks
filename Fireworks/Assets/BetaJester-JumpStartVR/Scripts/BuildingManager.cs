using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour
{

    //public float MinDistance;
    public float MaxDistance;

    public int MaxNumOfConnections;

    public GameObject[] staticBuildings;

    GameObject player;
    public bool forceSelf = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (forceSelf)
            PassClosestToBuildings(new List<GameObject>());
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void PassClosestToBuildings(List<GameObject> SpawnedBuildings)
    {
        foreach(GameObject b in staticBuildings)
        SpawnedBuildings.Add(b);


        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player");

        foreach (GameObject i in SpawnedBuildings)
        {
            i.GetComponent<BuildingIDScript>().PushToTrigger(player, "OnBuildingTriggered", i);
        }


        List<GameObject> temp = new List<GameObject>(SpawnedBuildings);

        for (int i = 0; i < SpawnedBuildings.Count; i++)
        {
            SetObj = SpawnedBuildings[i].transform;
            temp.Sort(ByDistanceToSetObj);

            int ConnectionsMade = 0;

            List<GameObject> toPush = new List<GameObject>();

            for (int j = 0; j < temp.Count && ConnectionsMade < MaxNumOfConnections; j++)
            {
                if (SpawnedBuildings[i] != temp[j])
                {
                    float Dist = Vector3.Distance(temp[j].transform.position, SetObj.position);
                    //if (Dist >= MinDistance - 1 && Dist < MaxDistance + 1)
                    if (Dist < MaxDistance)
                    {
                        toPush.Add(temp[j]);
                        ConnectionsMade++;
                    }
                }
            }

            SpawnedBuildings[i].GetComponent<BuildingIDScript>().SetClosestBuildingScripts(toPush);
        }
    }

    Transform SetObj;

    int ByDistanceToSetObj(GameObject a, GameObject b)
    {
        float dstToA = Vector3.Distance(SetObj.position, a.transform.position);
        float dstToB = Vector3.Distance(SetObj.position, b.transform.position);
        return dstToA.CompareTo(dstToB);
    }

}
