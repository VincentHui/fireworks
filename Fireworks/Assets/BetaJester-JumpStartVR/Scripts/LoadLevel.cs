using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
    public string levelToLoad;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelToLoad(GameObject obj)
    {
        if (null != obj)
            Application.LoadLevel(obj.name);
        else
            Application.LoadLevel(levelToLoad);
    }
}
