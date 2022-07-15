using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    void Start()
    {
        GameController.gc.inLoading = true;
        SceneManager.LoadSceneAsync(SaveSystem.LoadInt("SceneToLoad"));
        SaveSystem.EraseFile("SceneToLoad");
    }
}