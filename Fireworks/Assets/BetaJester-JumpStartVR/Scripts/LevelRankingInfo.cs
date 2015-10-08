using UnityEngine;
using System.Collections;

public class LevelRankingInfo{
   
    public float BestGameTime;
    public float BestShotsTaken;
    public float BestHits;
    public float BestHitsRecieved;
    public float BestNumSyncUps;
    public float BestAccuracy;

    public static void SetUpLevelRanking()
    {
        LevelRankingInfo newLvl = new LevelRankingInfo();
        newLvl.BestGameTime = 60;
        newLvl.BestShotsTaken = 25;
        newLvl.BestHits = 20;
        newLvl.BestHitsRecieved = 5;
        newLvl.BestNumSyncUps = 20;
        newLvl.BestAccuracy = 90;
        SaveStatistics("1vs1 #1 Square", newLvl.ToString());

        newLvl = new LevelRankingInfo();
        newLvl.BestGameTime = 60;
        newLvl.BestShotsTaken = 25;
        newLvl.BestHits = 20;
        newLvl.BestHitsRecieved = 5;
        newLvl.BestNumSyncUps = 20;
        newLvl.BestAccuracy = 90;
        SaveStatistics("1vs1 #2 Pentagon", newLvl.ToString());


        newLvl = new LevelRankingInfo();
        newLvl.BestGameTime = 60;
        newLvl.BestShotsTaken = 25;
        newLvl.BestHits = 20;
        newLvl.BestHitsRecieved = 5;
        newLvl.BestNumSyncUps = 20;
        newLvl.BestAccuracy = 90;
        SaveStatistics("1vs1 #3 Hexagon", newLvl.ToString());


        newLvl = new LevelRankingInfo();
        newLvl.BestGameTime = 60;
        newLvl.BestShotsTaken = 25;
        newLvl.BestHits = 20;
        newLvl.BestHitsRecieved = 5;
        newLvl.BestNumSyncUps = 20;
        newLvl.BestAccuracy = 90;
        SaveStatistics("1vs1 #4 Triangle", newLvl.ToString());
    }

    public override string ToString()
    {
        return "" + BestGameTime + ";" + BestShotsTaken + ";" + BestHits + ";" + BestHitsRecieved + ";" + BestNumSyncUps + ";" + BestAccuracy;
    }

    public static LevelRankingInfo Parse(string LoadString)
    {
        string[] LoadStringArray = LoadString.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);

        if (LoadStringArray.Length != 6)
        {
            return null;
        }
        else
        {
            return new LevelRankingInfo()
            {
                BestGameTime = float.Parse(LoadStringArray[0]),
                BestShotsTaken = float.Parse(LoadStringArray[1]),
                BestHits = float.Parse(LoadStringArray[2]),
                BestHitsRecieved = float.Parse(LoadStringArray[3]),
                BestNumSyncUps = float.Parse(LoadStringArray[4]),
                BestAccuracy = float.Parse(LoadStringArray[5]),
            };
        }
    }

    public static void SaveStatistics(string LevelName, string LvlRankingInfo)
    {
        PlayerPrefs.SetString(LevelName + "RankingInfo", LvlRankingInfo);
    }

    public static LevelRankingInfo LoadStatistics(string LevelName)
    {
        string LoadString = PlayerPrefs.GetString(LevelName + "RankingInfo", "");

        if (LoadString != "")
        {
           return LevelRankingInfo.Parse(LoadString);
        }
        else
        {
            return new LevelRankingInfo();
        }
    }

}
