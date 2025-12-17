using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void OnSceneChange()
    {
        if(0 == SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
