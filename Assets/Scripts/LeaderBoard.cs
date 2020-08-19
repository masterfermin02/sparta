using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public void OnShowLeaderBoard()
    {
        //        Social.ShowLeaderboardUI (); // Show all leaderboard
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_score); // Show current (Active) leaderboard
    }
}
