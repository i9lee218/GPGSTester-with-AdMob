using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using TMPro;


public class GPGSTester : MonoBehaviour , ISaveable
{
 string log;
string fileName = "SaveData.Json";

[SerializeField] private TextMeshProUGUI text;

    [SerializeField] public int CurrentScore;
    [SerializeField] public string PlayerName = "PN";
    



    void Start()
    {
            
    }
    void OnGUI()
    {
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one * 4);



        if (GUILayout.Button("ClearLog"))
            log = "";

        if (GUILayout.Button("DP Current Score"))
        {
            log = "Current Score: " +CurrentScore.ToString();

            text.text = "Current Score: " +CurrentScore.ToString();
        }
        if (GUILayout.Button("Up Score"))
        {
            CurrentScore++;
            log = "CurrentScore: " + CurrentScore.ToString();
        }

        if (GUILayout.Button("Down Score"))
        {
            CurrentScore--;
            log = "CurrentScore: " + CurrentScore.ToString();
        }
        
        if (GUILayout.Button("Unlock ahcie 1"))
        {
            log = "Unlock ahcie 1";
            GPGS.Instance.UnlockAchievement(GPGSIds.achievement_reach_10);
        }
        if (GUILayout.Button("RevealAchievement 1"))
        {
            log = "RevealAchievement 1";
            GPGS.Instance.RevealAchievement(GPGSIds.achievement_death_10);
        }



        if (GUILayout.Button("ShowAchievementUI"))
        {
            GPGS.Instance.ShowAchievements();
            log = "ShowAchievementUI";
        }
        if (GUILayout.Button("AddHighScoreToLeaderboard"))
        {
            GPGS.Instance.AddHighScoreToLeaderboard(CurrentScore);
            log = "AddHighScoreToLeaderboard";

        }

        //AddHighScoreToLeaderboard
            
        if (GUILayout.Button("ShowAllLeaderboardUI"))
        {
            GPGS.Instance.ShowLeaderboard();
            log = "ShowAllLeaderboardUI";

        }
        if (GUILayout.Button("ShowSaveFile"))
        {
            GPGS.Instance.ShowSavedFiles();
            log = "ShowSaveFile";
        }
        if (GUILayout.Button("Save"))
        {
            log = "Saving";

            SaveLoadManager.Instance.Save();

        
            //GPGSManager.Instance.Save("mysave", "want data", success => log = $"{success}");
        }
            

        if (GUILayout.Button("Load"))
        {
            log = "Loading";

            
            SaveLoadManager.Instance.Load();
            
            //GPGSManager.Instance.Load("mysave", (success, data) => log = $"{success}, {data}");

        

        }
            

        if (GUILayout.Button("Delete"))
        {
            log = "Deleting";
            GPGS.Instance.Delete(fileName);
            //GPGSManager.Instance.Delete("mysave", success => log = $"{success}");
        }
            

            
            /*

        if (GUILayout.Button("Login"))
            GPGSManager.Instance.Login((success, localUser) =>
            log = $"{success}, {localUser.userName}, {localUser.id}, {localUser.state}, {localUser.underage}");

        if (GUILayout.Button("Logout"))
            GPGSManager.Instance.Logout();
            
        if (GUILayout.Button("ShowSaveFile"))
            GPGSManager.Instance.ShowSavedFiles();


        if (GUILayout.Button("SaveCloud"))
            GPGSManager.Instance.SaveCloud("mysave", "want data", success => log = $"{success}");

        if (GUILayout.Button("LoadCloud"))
            GPGSManager.Instance.LoadCloud("mysave", (success, data) => log = $"{success}, {data}");

        if (GUILayout.Button("DeleteCloud"))
            GPGSManager.Instance.DeleteCloud("mysave", success => log = $"{success}");

        if (GUILayout.Button("ShowAchievementUI"))
            GPGSManager.Instance.ShowAchievements();

        if (GUILayout.Button("UnlockAchievement_one"))
            GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_one, success => log = $"{success}");

        if (GUILayout.Button("UnlockAchievement_two"))
            GPGSManager.Instance.UnlockAchievement(GPGSIds.achievement_two, success => log = $"{success}");

        if (GUILayout.Button("IncrementAchievement_three"))
            GPGSManager.Instance.IncrementAchievement(GPGSIds.achievement_three, 1, success => log = $"{success}");

        if (GUILayout.Button("ShowAllLeaderboardUI"))
            GPGSManager.Instance.ShowLeaderboard();
            

        if (GUILayout.Button("ShowTargetLeaderboardUI_num"))
            GPGSManager.Instance.ShowTargetLeaderboardUI(GPGSIds.leaderboard_num);

        if (GUILayout.Button("ReportLeaderboard_num"))
            GPGSManager.Instance.ReportLeaderboard(GPGSIds.leaderboard_num, 1000, success => log = $"{success}");

        if (GUILayout.Button("LoadAllLeaderboardArray_num"))
            GPGSManager.Instance.LoadAllLeaderboardArray(GPGSIds.leaderboard_num, scores =>
            {
                log = "";
                for (int i = 0; i < scores.Length; i++)
                    log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
            });

        if (GUILayout.Button("LoadCustomLeaderboardArray_num"))
            GPGSManager.Instance.LoadCustomLeaderboardArray(GPGSIds.leaderboard_num, 10,
                GooglePlayGames.BasicApi.LeaderboardStart.PlayerCentered, GooglePlayGames.BasicApi.LeaderboardTimeSpan.Daily, (success, scoreData) =>
                {
                    log = $"{success}\n";
                    var scores = scoreData.Scores;
                    for (int i = 0; i < scores.Length; i++)
                        log += $"{i}, {scores[i].rank}, {scores[i].value}, {scores[i].userID}, {scores[i].date}\n";
                });

        if (GUILayout.Button("IncrementEvent_event"))
            GPGSManager.Instance.IncrementEvent(GPGSIds.event_event, 1);

        if (GUILayout.Button("LoadEvent_event"))
            GPGSManager.Instance.LoadEvent(GPGSIds.event_event, (success, iEvent) =>
            {
                log = $"{success}, {iEvent.Name}, {iEvent.CurrentCount}";
            });

        if (GUILayout.Button("LoadAllEvent"))
            GPGSManager.Instance.LoadAllEvent((success, iEvents) =>
            {
                log = $"{success}\n";
                foreach (var iEvent in iEvents)
                    log += $"{iEvent.Name}, {iEvent.CurrentCount}\n";
            });

            */

        GUILayout.Label(log);
    }

    public void PopulateSaveData(SaveDataCollection saveDataCollection)
    {
        saveDataCollection.sampleData._playerName = PlayerName;
        saveDataCollection.sampleData._score = CurrentScore;
        
    }

    public void LoadFromSaveData(SaveDataCollection saveDataCollection)
    {
        
        PlayerName = saveDataCollection.sampleData._playerName;
        CurrentScore = saveDataCollection.sampleData._score;
    }
}
