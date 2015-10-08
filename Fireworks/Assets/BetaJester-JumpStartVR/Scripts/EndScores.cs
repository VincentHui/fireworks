using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndScores : MonoBehaviour
{
    
    static int TopBracketOfScores;
    static int MiddleBracketOfScores;
    static int BottomBracketOfScores;
    public int topScoreBracket;
    public int middleScoreBracket;
    public int bottomScoreBracket;

    static string firstClassPass = "1st";
    static string upperSecondClassPass = "2:1";
    static string lowerSecondClassPass = "2:2";
    static string thirdClassPass = "3rd";

    //These will be shown on end game screen
    public static string playerOverallRating;

    //Used for calculations
    static int overallRatingINT;

    public Text finalRatingText;
    public ItemCollectorScript Player;


    public int BestPlat;
    public int BestCollectables;

    void Awake()
    {
        //Find the text fields that are specified as the score texts
        
        //Set local static values to publicly set values
        TopBracketOfScores = topScoreBracket;
        MiddleBracketOfScores = middleScoreBracket;
        BottomBracketOfScores = bottomScoreBracket;
    }

    void Update()
    {
        //if(LevelManagerScript.levelState == LevelManagerScript.LevelState.Ending)
        {
            CalculateOverallRatingINT(JumpTextScript.JumpCount, Player.NumberCollected);
            finalRatingText.text = GetOverallRating();
        }
    }
    

    public static string GetOverallRating()
    {
        return playerOverallRating;
    }
    
    //This is called in CalculateOverallRatingINT. Is not needed elsewhere
    static void CalculateOverallRating()
    {
        if (overallRatingINT >= TopBracketOfScores)
        {
            //Top score
            playerOverallRating = firstClassPass;
        }
        else if (overallRatingINT >= MiddleBracketOfScores && overallRatingINT < TopBracketOfScores)
        {
            //2nd best score
            playerOverallRating = upperSecondClassPass;
        }
        else if (overallRatingINT >= BottomBracketOfScores && overallRatingINT < MiddleBracketOfScores)
        {
            //3rd best score
            playerOverallRating = lowerSecondClassPass;
        }
        else if (overallRatingINT < BottomBracketOfScores)
        {
            //Lowest score
            playerOverallRating = thirdClassPass;
        }
    }

    //Call this method for overall rating to be set
    public void CalculateOverallRatingINT(int platformsHit, int collectablesCollected)
    {
        bool isINT = false;

        overallRatingINT = Mathf.RoundToInt(((((float)BestPlat/ (float)platformsHit)*10) + (((float)collectablesCollected / (float)BestCollectables) * 10))/2);

        if (overallRatingINT / 2 == (int)(overallRatingINT / 2))
        {
            isINT = true;
        }
        else if (overallRatingINT / 2 != (int)(overallRatingINT / 2))
        {
            isINT = false;
        }
        else
        {
            Debug.LogError("End Score isn't dividing by 2 correctly");
        }

        //If the result is a hole number, not decimal set overallRatig value directly
        //If not, cast as int which loses the decimal, but +1 so it rounds up
        if (isINT)
        {
            //Pointless line, just means we can keep the value as is, rather than rounding up
            overallRatingINT = overallRatingINT;
        }
        else if (!isINT)
        {
            overallRatingINT = ((int)overallRatingINT) + 1;
        }

        CalculateOverallRating();
    }
}
