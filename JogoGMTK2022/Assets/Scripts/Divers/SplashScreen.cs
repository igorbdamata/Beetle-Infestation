using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    void Start() { if (DATA.d.playedSplashScreen) { Destroy(gameObject); } }

    public void OnEndAnimation()
    {
        DATA.d.playedSplashScreen = true;
        Destroy(gameObject);
    }
}