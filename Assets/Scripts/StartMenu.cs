using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    #region DEFAULT_UNITY_CALLBACKS
    void Start()
    {
        // Create client configuration
        PlayGamesClientConfiguration config = new
            PlayGamesClientConfiguration.Builder()
            .Build();

        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        // Initialize and activate the platform
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
    }
    #endregion

    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }

    /// <summary>
    /// Shows All Available Leaderborad
    /// </summary>
    public void OnShowLeaderBoard()
    {
        //        Social.ShowLeaderboardUI (); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_score); // Show current (Active) leaderboard
    }
    /// <summary>
    /// Adds Score To leader board
    /// </summary>
    public void OnAddScoreToLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, GPGSIds.leaderboard_score, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });
        }
    }
    /// <summary>
    /// On Logout of your Google+ Account
    /// </summary>
    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
}
