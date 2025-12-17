using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.SocialPlatforms;
using Unity.Services.Authentication;
using Unity.Services.Core;
using NUnit.Framework.Interfaces;


#if UNITY_ANDROID || UNITY_EDITOR
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;

#endif

public class GPGS// : MonoBehaviour
{

#if UNITY_ANDROID //|| UNITY_EDITOR
    
    //public static GPGSManager Instance { get; private set; }
    // private async void Awake()
    // {
    //     if (Instance != null && Instance != this)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //         //DontDestroyOnLoad(transform.root.gameObject);

    //         //

    //         // for debug
    //         PlayGamesPlatform.DebugLogEnabled = true;
    //         PlayGamesPlatform.Activate();


    //         LoginGooglePlayGames();

            
    //     }
    

    // }

    

    
    // if wants to use GPGSManager without MonoBehaviour
    private static GPGS instance = new GPGS();
    public static GPGS Instance => instance;
    

    PlayGamesPlatform PGP => PlayGamesPlatform.Instance;
    //ISavedGameClient SavedGameClient => PlayGamesPlatform.Instance.SavedGame;
    //IEventsClient Events => PlayGamesPlatform.Instance.Events;

    private string googlePlayGamesToken;
    DataSource dataSource = DataSource.ReadCacheOrNetwork;
    ConflictResolutionStrategy conflictResolutionStrategy = ConflictResolutionStrategy.UseLastKnownGood;

    
    #region Google Login
    public async void InitiateGPGS()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        
        LoginGooglePlayGames();
    }


    private void LoginGooglePlayGames()
    {
        PGP.Authenticate((status) =>
        {
            if (status == SignInStatus.Success)
            {
                Debug.Log("Login with GooglePlayGames Successful");

                PGP.RequestServerSideAccess(forceRefreshToken: true, code =>
                {
                    Debug.Log("Authorization code: " + code);
                    googlePlayGamesToken = code;

                });


            }
            else
            {
                Debug.Log($"GooglePlayGames login failed, : {status}");

            }
        });
    }

    // when login google play games with intention
    public void StartSignInWithGooglePlayGames()
    {
        if (!PGP.IsAuthenticated())
        {
            Debug.LogWarning("Not yet authenticated with GooglePlayGames / Attempting Login Again...");
            LoginGooglePlayGames();
            return;
        }

        SignInOrLinkWithGooglePlayGames();
    }

    private async void SignInOrLinkWithGooglePlayGames()
    {
        if (string.IsNullOrEmpty(googlePlayGamesToken))
        {
            Debug.LogWarning("Authorization code is null or empty");
            return;
        }

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            // new player
            await SignInWithGooglePlayGamesAsync(googlePlayGamesToken);
        }
        else
        {
            // existing player
            await LinkWithGooglePlayGamesAsync(googlePlayGamesToken);
        }
    }

    // new player
    private async Task SignInWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithGooglePlayGamesAsync(authCode);
            Debug.Log("Signin Success");
        }
        catch (AuthenticationException e)
        {

            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }

    // existing player
    private async Task LinkWithGooglePlayGamesAsync(string authCode)
    {
        try
        {
            await AuthenticationService.Instance.LinkWithGooglePlayGamesAsync(authCode);
            Debug.Log("Link Success");
        }
        catch (AuthenticationException e) when (e.ErrorCode == AuthenticationErrorCodes.AccountAlreadyLinked)
        {
            Debug.LogWarning("This user is already linked with another account. Log in Instead");
        }
        catch (AuthenticationException e)
        {
            Debug.LogException(e);
        }
        catch (RequestFailedException e)
        {
            Debug.LogException(e);
        }
    }
    #endregion


    #region Achievements
    public void ShowAchievements()
    {
        
        PGP.ShowAchievementsUI((status) =>
        {
             //UIStatus.Valid
        });

        // Social is obsolete
        //Social.ShowAchievementsUI();
    }

    public void UnlockAchievement(string gpgsid)
    {
        PGP.UnlockAchievement(gpgsid, (bool success) => { });
        //CheckAchievement(gpgsid);

    }

    public void CheckScoreAchievement(int currentScore)
    {
        if(10 == currentScore)
        {
            CheckAchievement(GPGSIds.achievement_reach_10);    
        }
        else if(20 == currentScore)
        {
            CheckAchievement(GPGSIds.achievement_reach_20);    
        }
        /*
        else if(30 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_30);    
        }
        else if(40 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_40);    
        }
        else if(50 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_50);    
        }
        else if(100 == currentScore)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_reach_100);    
        }
        */
    }
    public void CheckDeathAchievement(int numDeath)
    {
        if(10 == numDeath)
        {
            CheckAchievement(GPGSIds.achievement_death_10);    
        }
        else if(20 == numDeath)
        {
            CheckAchievement(GPGSIds.achievement_death_20);    
        }
        /*
        else if(50 == numDeath)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_gameover_50);    
        }
        else if(100 == numDeath)
        {
            GPGSManager.Instance.CheckAchievement(GPGSIds.achievement_gameover_100);    
        }
        */
    }

    private void CheckAchievement(string gpgsid)
    {
        PGP.LoadAchievements(achievements =>
        {
            foreach (var achieve in achievements)
            {
                if(gpgsid == achieve.id)
                {
                    if(achieve.completed)
                    {
                        return;
                    }
                    else
                    {
                        PGP.UnlockAchievement(gpgsid, (bool success) => { });
                    }
                }
            }
        });
    }


    public void IncrementAchievement(string gpgsid, int increaseAmount)
    {
        PGP.IncrementAchievement(gpgsid, increaseAmount, (bool success) => { });
    }

    /*
 According to the expected behavior of Social.ReportProgress,
a value of 0.0f means the achievement is revealed
and a progress of 100.0f means the achievement is unlocked.

  To reveal an achievement that was previously hidden without unlocking it,
call Social.ReportProgress with a value of 0.0f.

 */
    public void RevealAchievement(string gpgsid)
    {
        PGP.ReportProgress(gpgsid, 0f, (bool success) => { });
    }

    #endregion

    #region Leaderboard
    public void ShowLeaderboard()
    {
        PGP.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }

    public void AddHighScoreToLeaderboard(int highScore, string gpgsid = GPGSIds.leaderboard_leaderboard)
    {
        PGP.ReportScore(highScore, gpgsid, (bool success) => { });

    }
    #endregion

    #region Event
    public void IncrementEvent(string gpgsid, uint increaseAmount)
    {
        PGP.Events.IncrementEvent(gpgsid, increaseAmount);
    }
    #endregion

    #region Save & Load
    private void OpenDataFile(string fileName, string saveData, Action<bool> onCloudSaved = null, Action<bool, string> onCloudLoaded = null)
    {
        //Debug.Log($"OpenDataFile, isSaving: {isSaving} | {fileName} , {saveData}");

        //DataSource dataSource = DataSource.ReadCacheOrNetwork;
        //ConflictResolutionStrategy conflictResolutionStrategy = ConflictResolutionStrategy.UseLastKnownGood;

        //SavingDataFile(fileName, saveData, onCloudSaved, dataSource, conflictResolutionStrategy);
        SavingDataFile(fileName, saveData, onCloudSaved);

    }
    private void OpenDataFile(string fileName, Action<bool, string> onCloudLoaded = null)
    {
        //Debug.Log($"OpenDataFile, isSaving: {isSaving} | {fileName} , {saveData}");

        //DataSource dataSource = DataSource.ReadCacheOrNetwork;
        //ConflictResolutionStrategy conflictResolutionStrategy = ConflictResolutionStrategy.UseLastKnownGood;

        //LoadingDataFile(fileName, onCloudLoaded, dataSource, conflictResolutionStrategy);
        LoadingDataFile(fileName, onCloudLoaded);
        
    }

private void SavingDataFile(string fileName, string saveData, Action<bool> onCloudSaved)
    {
        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, dataSource,
                    conflictResolutionStrategy, (status, game) =>
                    {
                        //Writting Save Files
                        if (status == SavedGameRequestStatus.Success)
                        {
                            Debug.Log("SavedGameRequestStatus.Success");

                            var update = new SavedGameMetadataUpdate.Builder().Build();
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(saveData);
                            PGP.SavedGame.CommitUpdate(game, update, bytes, (status2, game2) =>
                            {
                                onCloudSaved?.Invoke(status2 == SavedGameRequestStatus.Success);
                            });

                            



                        }
                        else
                        {
                            Debug.LogWarning("SavedGameRequestStatus Failed");
                            // handle error
                            onCloudSaved?.Invoke(false);
                        }
                    });
    }
    private void LoadingDataFile(string fileName, Action<bool, string> onCloudLoaded)
    {
        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, dataSource,
                    conflictResolutionStrategy, (status, game) =>
                    {
                        if (status == SavedGameRequestStatus.Success)
                        {
                            PGP.SavedGame.ReadBinaryData(game, (status2, loadedData) =>
                            {
                                string data = System.Text.Encoding.UTF8.GetString(loadedData);
                                onCloudLoaded?.Invoke(true, data);


                            });

                        }
                        else if (status == SavedGameRequestStatus.AuthenticationError)
                        {
                            onCloudLoaded?.Invoke(false, "AuthenticationError");

                        }
                        else if (status == SavedGameRequestStatus.BadInputError)
                        {
                            onCloudLoaded?.Invoke(false, "BadInputError");

                        }
                        else if (status == SavedGameRequestStatus.InternalError)
                        {
                            onCloudLoaded?.Invoke(false, "privateError");

                        }
                        else if (status == SavedGameRequestStatus.TimeoutError)
                        {
                            onCloudLoaded?.Invoke(false, "TimeoutError");

                        }
                    });
    }


    

    public void Save(string fileName, SaveDataCollection saveDataCollection)
    {
        OpenDataFile(fileName, saveDataCollection.ToJson(), (bool success) =>
        {

            try
            {
                Debug.Log("Save succeed its' from try");

            }

            catch (Exception e)
            {
                Debug.LogWarning($"Failed to save with exception {e}");

            }
        });
    }

    //public void Load(string fileName,SaveDataCollection saveDataCollection)
    public void Load(string fileName)
    {
        OpenDataFile(fileName, (success, data) =>
        {
            SaveDataCollection saveDataCollection = new SaveDataCollection();

            try
            {
                Debug.Log("Load succeed its' from try");
                saveDataCollection.LoadFromJson(data);
                SaveLoadManager.Instance.LoadFromSaveData(saveDataCollection);

            }

            catch (Exception e)
            {
                Debug.LogWarning($"Failed to Load with exception {e}");
                saveDataCollection.LoadFromJson(data);
            

            }
        });
    }
    // show Saved Files
    public void ShowSavedFiles()
    {
        Debug.Log("ShowSavedFiles");

        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
    }

    private void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
            Load(game.Filename);            
        }
        else
        {
            // handle cancel or error
        }
    }
#endregion

    #region  Delete
    public void Delete(string fileName)
    {
        //SavedGameClient.OpenWithAutomaticConflictResolution(saveFile, DataSource.ReadCacheOrNetwork,   ConflictResolutionStrategy.UseLastKnownGood, DeleteSavedGame); 
        PGP.SavedGame.OpenWithAutomaticConflictResolution(fileName, DataSource.ReadCacheOrNetwork,
        ConflictResolutionStrategy.UseLastKnownGood, DeleteSavedGame);
    }
    private void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("SavedGameRequestStatus.Success");
            PGP.SavedGame.Delete(game);
            /*
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
            */

        }
        else
        {
            // handle error
            Debug.LogWarning("SavedGameRequestStatus failed");
        }

    }



    #endregion

#endif

}
