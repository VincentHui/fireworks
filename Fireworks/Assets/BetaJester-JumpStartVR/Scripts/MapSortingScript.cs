using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MapSortingScript : MonoBehaviour
{
    public Transform PlayerStartingBuilding;

    public int MaxJumpDistance;

    public int MinClosness;

    public int MinNumberOfPaths;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SortObjects(List<GameObject> ObjectsToSort)
    {


        ObjectsToSort.Sort(ByDistance);

        //StartCoroutine("MoveObjs", ObjectsToSort);

        List<GameObject> tempList;

        for (int i = 0; i < ObjectsToSort.Count; i++)
        {
            ObjectsToSort.Sort(ByDistance);

            SetObj = ObjectsToSort[i].transform;

            tempList = new List<GameObject>(ObjectsToSort);

            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j].transform.position.z < SetObj.transform.position.z)
                {
                    tempList.RemoveAt(j);
                }
            }

            tempList.Sort(ByDistanceToSetObj);

            for (int j = 0; j < MinNumberOfPaths && j < tempList.Count; j++)
            {
                float Dist = Vector3.Distance(SetObj.position, tempList[j].transform.position);

                if ((Dist > MaxJumpDistance || Dist < MinClosness) && tempList[j].transform != SetObj.transform)
                {
                    tempList[j].transform.position = SetObj.position + (Vector3.Normalize(tempList[j].transform.position - SetObj.position) * Random.Range(MinClosness, MaxJumpDistance));
                    //                 yield return new WaitForSeconds(.1f);
                }
            }
        }

        for (int i = 0; i < ObjectsToSort.Count; i++)
        {
            ObjectsToSort = ObjectsToSort.OrderBy(x => x.transform.position.z).ToList();

            SetObj = ObjectsToSort[i].transform;

            tempList = new List<GameObject>(ObjectsToSort);

            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j].transform.position.z < SetObj.transform.position.z)
                {
                    tempList.RemoveAt(j);
                }
            }

            tempList.Sort(ByDistanceToSetObj);

            for (int j = 0; j < tempList.Count; j++)
            {
                float Dist = Vector3.Distance(SetObj.position, tempList[j].transform.position);

                if (Dist < MinClosness && tempList[j].transform != SetObj.transform)
                {
                    tempList[j].transform.position = SetObj.position + (Vector3.Normalize(tempList[j].transform.position - SetObj.position) * MinClosness);
                    ///                    yield return new WaitForSeconds(.1f);

                }
            }
        }


        for (int i = 0; i < ObjectsToSort.Count; i++)
        {
            ObjectsToSort.Sort(ByDistance);

            SetObj = ObjectsToSort[i].transform;

            tempList = new List<GameObject>(ObjectsToSort);

            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j].transform.position.z < SetObj.transform.position.z)
                {
                    tempList.RemoveAt(j);
                }
            }

            tempList.Sort(ByDistanceToSetObj);

            int forwardConnections = 0;

            for (int j = 0; j < MinNumberOfPaths && j < tempList.Count; j++)
            {
                float Dist = Vector3.Distance(SetObj.position, tempList[j].transform.position);

                if ((Dist <= MaxJumpDistance || Dist >= MinClosness) && tempList[j].transform != SetObj.transform)
                {
                    forwardConnections++;
                    // tempList[j].transform.position = SetObj.position + (Vector3.Normalize(tempList[j].transform.position - SetObj.position) * Random.Range(MinClosness, MaxJumpDistance));
                    //                 yield return new WaitForSeconds(.1f);
                }
            }

            if(forwardConnections == 0)
            {
                int sdfg = 4;
            }
        }


        //for (int i = 0; i < ObjectsToSort.Count; i++)
        //{
        //    for (int j = 0; j < ObjectsToSort.Count; j++)
        //    {
        //        float Dist = Vector3.Distance(ObjectsToSort[i].transform.position, ObjectsToSort[j].transform.position);

        //        if (Dist < MinClosness - 1 && ObjectsToSort[j].transform != ObjectsToSort[i].transform)
        //        {
        //            int sadfgv = 3456;
        //        }
        //    }
        //}

        //yield return new WaitForSeconds(.1f);



    }


    IEnumerator MoveObjs(List<GameObject> ObjectsToSort)
    {
        List<GameObject> tempList;

        for (int i = 0; i < ObjectsToSort.Count; i++)
        {
            ObjectsToSort.Sort(ByDistance);

            SetObj = ObjectsToSort[i].transform;

            tempList = new List<GameObject>(ObjectsToSort);

            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j].transform.position.z < SetObj.transform.position.z)
                {
                    tempList.RemoveAt(j);
                }
            }

            tempList.Sort(ByDistanceToSetObj);

            for (int j = 0; j < MinNumberOfPaths && j < tempList.Count; j++)
            {
                float Dist = Vector3.Distance(SetObj.position, tempList[j].transform.position);

                if ((Dist > MaxJumpDistance || Dist < MinClosness) && tempList[j].transform != SetObj.transform)
                {
                    tempList[j].transform.position = SetObj.position + (Vector3.Normalize(tempList[j].transform.position - SetObj.position) * Random.Range(MinClosness, MaxJumpDistance));
                    yield return new WaitForSeconds(.1f);
                }
            }
        }

        for (int i = 0; i < ObjectsToSort.Count; i++)
        {
            ObjectsToSort = ObjectsToSort.OrderBy(x => x.transform.position.z).ToList();

            SetObj = ObjectsToSort[i].transform;

            tempList = new List<GameObject>(ObjectsToSort);

            for (int j = 0; j < tempList.Count; j++)
            {
                if (tempList[j].transform.position.z < SetObj.transform.position.z)
                {
                    tempList.RemoveAt(j);
                }
            }

            tempList.Sort(ByDistanceToSetObj);

            for (int j = 0; j < tempList.Count; j++)
            {
                float Dist = Vector3.Distance(SetObj.position, tempList[j].transform.position);

                if (Dist < MinClosness && tempList[j].transform != SetObj.transform)
                {
                    tempList[j].transform.position = SetObj.position + (Vector3.Normalize(tempList[j].transform.position - SetObj.position) * MinClosness);
                    yield return new WaitForSeconds(.1f);

                }
            }
        }

        //for (int i = 0; i < ObjectsToSort.Count; i++)
        //{
        //    for (int j = 0; j < ObjectsToSort.Count; j++)
        //    {
        //        float Dist = Vector3.Distance(ObjectsToSort[i].transform.position, ObjectsToSort[j].transform.position);

        //        if (Dist < MinClosness - 1 && ObjectsToSort[j].transform != ObjectsToSort[i].transform)
        //        {
        //            int sadfgv = 3456;
        //        }
        //    }
        //}

        //yield return new WaitForSeconds(.1f);



    }

    int ByDistance(GameObject a, GameObject b)
    {
        float dstToA = Vector3.Distance(PlayerStartingBuilding.position, a.transform.position);
        float dstToB = Vector3.Distance(PlayerStartingBuilding.position, b.transform.position);
        return dstToA.CompareTo(dstToB);
    }

    Transform SetObj;

    int ByDistanceToSetObj(GameObject a, GameObject b)
    {
        float dstToA = Vector3.Distance(SetObj.position, a.transform.position);
        float dstToB = Vector3.Distance(SetObj.position, b.transform.position);
        return dstToA.CompareTo(dstToB);
    }
}
