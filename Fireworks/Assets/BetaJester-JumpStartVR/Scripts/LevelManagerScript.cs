using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(AudioSource))]
public class LevelManagerScript : MonoBehaviour
{

    public enum LevelState
    {
        Starting,
        Playing,
        Paused,
        Ending,
        Restarting,
        Finished
    }


    public static LevelState levelState = LevelState.Starting;
    
    public GameObject Player;
    public string ReturnLevelTitle;
    public string NextLevelTitle;


    public AudioSource mainAudio;
    AudioSource audSource;
    bool EndSoundPlayed = false;

    // Use this for initialization
    void Start()
    {

        audSource = this.GetComponent<AudioSource>();

        levelState = LevelState.Starting;

        //CountdownAmount = levelStartTime / CountdownSize;
        //audioSource = this.GetComponent<AudioSource>();
        //audioSource.loop = false;
        //CurrentAudioClip = CountdownSize;
        //audioSource.clip = levelStartNumberClips[CurrentAudioClip - 1];
        //audioSource.Play();

        //if (showStartTimer)
        //    levelText.text = "" + CurrentAudioClip;
        //else
        //    levelText.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        switch (levelState)
        {
            case LevelState.Starting:
                #region Starting
                {
                    levelState = LevelState.Playing;


                }
                #endregion
                break;

            case LevelState.Playing:
                #region Playing

                #endregion
                break;

            case LevelState.Paused:
                #region Paused
                #endregion
                break;

            case LevelState.Ending:
                #region Ending

                if (!EndSoundPlayed)
                {
                    mainAudio.Stop();
                    audSource.Play();
                    EndSoundPlayed = true;
                }

                
                #endregion
                break;

            case LevelState.Restarting:
                #region Restarting
                Reset();
                #endregion
                break;

            case LevelState.Finished:
                #region Finished

                SaveLevelStats();
                Reset();
                Application.LoadLevel(NextLevelTitle);
                #endregion
                break;
        }
    }

    private void Reset()
    {
        JumpTextScript.Reset();
    }

    private void SaveLevelStats()
    {
        int Collected = Player.GetComponent<ItemCollectorScript>().NumberCollected;
        string Rank = EndScores.playerOverallRating;

        SaveData.AddToIntDictionary(Application.loadedLevelName + "Collected", Collected);
        SaveData.AddToStringDictionary(Application.loadedLevelName + "Rank", Rank);
    }

    public void ForceEndLevel()
    {
        //PauseScript.ResumeGame();
       // Score = Vector2.zero;
        Application.LoadLevel(ReturnLevelTitle);
    }

    public static void EndGame()
    {
        levelState = LevelState.Ending;
    }

    public void FinishedLevel(GameObject nextLevel)
    {
        NextLevelTitle = nextLevel.name; 

        levelState = LevelState.Finished;
    }
}
