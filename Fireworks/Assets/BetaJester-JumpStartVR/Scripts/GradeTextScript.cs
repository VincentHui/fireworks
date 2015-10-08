using UnityEngine;
using System.Collections;

public class GradeTextScript : MonoBehaviour
{

    TypogenicText typoGenic;
    string statGrade;
    public string thisLevel = "";
    // Use this for initialization
    void Start()
    {
        typoGenic = this.GetComponent<TypogenicText>();
        LoadLevelStats();
        typoGenic.Text = statGrade;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LoadLevelStats()
    {
        statGrade = SaveData.GetStringData(thisLevel + "Rank", "3rd");
       
    }
}