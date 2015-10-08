using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceRandomFillManager : MonoBehaviour
{
    //public PlayerHealth playerHealth;
    public GameObject defaultSpawnObject;
    public GameObject[] spawnObjects;
    public Transform[] spawnSpace;
    public int OddsOfSpawning = 80;

    public bool UseSmallerOdds = true;

    public int SpawnSeed = 0;

    public bool LimitHeight = true;

    List<GameObject> SpawnedObjects = new List<GameObject>();

    public bool SortObjects = true;
    MapSortingScript mapSort;

    public bool DeleteOuterAfterSort = true;

    public bool PushToBuildingManager = true;

    public bool UseCollectables = true;

    void Start()
    {
        Random.seed = SpawnSeed;

        mapSort = this.GetComponent<MapSortingScript>();

        float Width = 0;
        float Height = 0;
        float Depth = 0;

        if (null != defaultSpawnObject.transform.FindChild("Floor"))
        {
            Width = defaultSpawnObject.transform.FindChild("Floor").lossyScale.x;
            Height = defaultSpawnObject.transform.FindChild("Floor").lossyScale.y;
            Depth = defaultSpawnObject.transform.FindChild("Floor").lossyScale.z;
        }
        else
        {
            Width = defaultSpawnObject.transform.localScale.x;
            Height = defaultSpawnObject.transform.localScale.y;
            Depth = defaultSpawnObject.transform.localScale.z;
        }

        //print("Width=" + Width + " Height=" + Height + " Depth=" + Depth);

        float MaxWidth = Mathf.Max(spawnSpace[0].transform.position.x, spawnSpace[1].transform.position.x);
        float MaxHeight = Mathf.Max(spawnSpace[0].transform.position.y, spawnSpace[1].transform.position.y);
        float MaxDepth = Mathf.Max(spawnSpace[0].transform.position.z, spawnSpace[1].transform.position.z);

        //print("MaxWidth=" + MaxWidth + " MaxHeight=" + MaxHeight + " MaxDepth=" + MaxDepth);

        Vector3 StartingVec = Vector3.zero;

        if (spawnSpace[0].transform.position.x < spawnSpace[1].transform.position.x)
            StartingVec = spawnSpace[0].transform.position;
        else
            StartingVec = spawnSpace[1].transform.position;

        List<Vector3> spawnWidthPlaces = GetSpawnLocationOnAxis(new Vector3(Width, 0, 0), new Vector3(MaxWidth, MaxHeight, MaxDepth), StartingVec);

        if (spawnSpace[0].transform.position.y < spawnSpace[1].transform.position.y)
            StartingVec = spawnSpace[0].transform.position;
        else
            StartingVec = spawnSpace[1].transform.position;

        List<Vector3> spawnHeightPlaces = GetSpawnLocationOnAxis(new Vector3(0, Height, 0), new Vector3(MaxWidth, MaxHeight, MaxDepth), StartingVec);




        if (spawnSpace[0].transform.position.z < spawnSpace[1].transform.position.z)
            StartingVec = spawnSpace[0].transform.position;
        else
            StartingVec = spawnSpace[1].transform.position;

        List<Vector3> spawnDepthPlaces = GetSpawnLocationOnAxis(new Vector3(0, 0, Depth), new Vector3(MaxWidth, MaxHeight, MaxDepth), StartingVec);

        Vector3[,,] RoomSpawnVecs = new Vector3[spawnWidthPlaces.Count, spawnHeightPlaces.Count, spawnDepthPlaces.Count];

        for (int i = 0; i < RoomSpawnVecs.GetLength(0); i++)
        {
            RoomSpawnVecs[i, 0, 0] = spawnWidthPlaces[i];
        }
        for (int i = 0; i < RoomSpawnVecs.GetLength(1); i++)
        {
            RoomSpawnVecs[0, i, 0] = spawnHeightPlaces[i];
        }
        for (int i = 0; i < RoomSpawnVecs.GetLength(2); i++)
        {
            RoomSpawnVecs[0, 0, i] = spawnDepthPlaces[i];
        }

        for (int i = 0; i < RoomSpawnVecs.GetLength(0); i++)
        {
            for (int j = 0; j < RoomSpawnVecs.GetLength(1); j++)
            {
                if (i == 0 && j == 0)
                {
                    continue;
                }
                else
                {

                    for (int k = 0; k < RoomSpawnVecs.GetLength(2); k++)
                    {
                        if ((i == 0 || j == 0) && k == 0)
                        {
                            continue;
                        }
                        else
                        {
                            RoomSpawnVecs[i, j, k] = new Vector3(MaxWidth, MaxHeight, MaxDepth);
                            RoomSpawnVecs[i, j, k].x = RoomSpawnVecs[i, 0, 0].x;
                            RoomSpawnVecs[i, j, k].y = RoomSpawnVecs[0, j, 0].y;
                            RoomSpawnVecs[i, j, k].z = RoomSpawnVecs[0, 0, k].z;
                        }
                    }
                }
            }
        }

        foreach (Vector3 vec in RoomSpawnVecs)
        {

            int Odds = Random.Range(0, (UseSmallerOdds ? 1000000 : 100));

            if (Odds > (UseSmallerOdds ? 1000000 : 100) - OddsOfSpawning)
                Spawn(vec);

        }

        if(SortObjects)
        {
            mapSort.SortObjects(SpawnedObjects);
        }

        if(DeleteOuterAfterSort)
        {
            for (int i = 0; i < SpawnedObjects.Count; i++)
            {
                if (SpawnedObjects[i].transform.position.z > Mathf.Max(spawnSpace[0].position.z, spawnSpace[1].position.z) ||
                    SpawnedObjects[i].transform.position.x > Mathf.Max(spawnSpace[0].position.x, spawnSpace[1].position.x) ||
                    SpawnedObjects[i].transform.position.z < Mathf.Min(spawnSpace[0].position.z, spawnSpace[1].position.z) ||
                    SpawnedObjects[i].transform.position.x < Mathf.Min(spawnSpace[0].position.x, spawnSpace[1].position.x) )
                {
                    Destroy(SpawnedObjects[i]);
                    SpawnedObjects.RemoveAt(i);
                    i--;
                }

            }

        }

        if(PushToBuildingManager)
        {
            this.GetComponent<BuildingManager>().PassClosestToBuildings(SpawnedObjects);
        }

        if (UseCollectables)
        {
            this.GetComponent<CollectableManager>().SetUpCollectables(SpawnedObjects);
        }
    }

    private List<Vector3> GetSpawnLocationOnAxis(Vector3 DifBetweenRooms, Vector3 MaxOnAxis, Vector3 StartingVec)
    {
        List<Vector3> spawnVectors = new List<Vector3>();

        spawnVectors.Add(StartingVec);

        Vector3 NextSpawnLocation = StartingVec + DifBetweenRooms;

        do
        {
            spawnVectors.Add(NextSpawnLocation);

            //print("X" + spawnVectors[spawnVectors.Count - 1].x + "Y" + spawnVectors[spawnVectors.Count - 1].y + "Z" + spawnVectors[spawnVectors.Count - 1].z);

            NextSpawnLocation += DifBetweenRooms;

            //print("Next Spawn X = " + NextSpawnLocation.x.ToString() + " Max is: " + MaxOnAxis.ToString());
        }
        while (!(NextSpawnLocation.x > MaxOnAxis.x) && !(NextSpawnLocation.y > MaxOnAxis.y) && !(NextSpawnLocation.z > MaxOnAxis.z));

        return spawnVectors;
    }


    void Spawn(Vector3 Position)
    {
        //print("X" + Position.x + "Y" + Position.y + "Z" + Position.z);

        GameObject spawnObject = defaultSpawnObject;

        if (spawnObjects.Length > 0)
        {
            spawnObject = spawnObjects[Random.Range(0, spawnObjects.Length)];
        }

        if (LimitHeight)
        {
            Position = new Vector3(Position.x, 0, Position.z);
        }

        GameObject obj = (GameObject)Instantiate(spawnObject, Position, spawnObject.transform.rotation);
        obj.transform.parent = this.transform;
        SpawnedObjects.Add(obj);


    }


}
