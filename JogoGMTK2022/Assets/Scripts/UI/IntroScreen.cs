using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IntroScreen : MonoBehaviour
{
    [SerializeField] private GameObject splashScreen;
    [SerializeField] private GameObject initialScreen;
    [SerializeField] private Button startButton;

    [SerializeField] private RectTransform offScreenT;
    [SerializeField] private RectTransform onScreenT;

    [SerializeField] private float speed = 500f;
    
    bool isChangingPosition;

    private void Start()
    {
        if (DATA.d.playedIntro)
        {
            transform.position = offScreenT.position;
        }
        else { initialScreen.transform.position = offScreenT.position; }
    }

    private void Update()
    {
        bool willExitIntro = !DATA.d.playedIntro && Input.anyKeyDown && (DATA.d.playedSplashScreen || !splashScreen.activeSelf);
        if (willExitIntro) { ChangeScreen(); }
    }

    public void ChangeScreen() { StartCoroutine(ControlAnimation()); }

    IEnumerator ControlAnimation()
    {
        if (!isChangingPosition)
        {
            isChangingPosition = true;
            DATA.d.playedIntro = !DATA.d.playedIntro;

            yield return Move();

            isChangingPosition = false;
        }
    }

    IEnumerator Move()
    {
        Vector2 introFinalPos = DATA.d.playedIntro ? offScreenT.position : onScreenT.position;
        Vector2 initialScreenFinalPos = DATA.d.playedIntro ? onScreenT.position : offScreenT.position;
        while (true)
        {
            transform.position += (Vector3)GetDirection(transform.position, introFinalPos) * speed * Time.deltaTime;
            initialScreen.transform.position += (Vector3)GetDirection(initialScreen.transform.position, initialScreenFinalPos) * speed * Time.deltaTime;

            bool animationEnded = DATA.d.playedIntro ? transform.position.x <= offScreenT.position.x : transform.position.x >= onScreenT.position.x;
            if (animationEnded)
            {
                transform.position = DATA.d.playedIntro ? offScreenT.position : onScreenT.position;
                initialScreen.transform.position = DATA.d.playedIntro ? onScreenT.position : offScreenT.position;
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    Vector2 GetDirection(Vector2 currentPos, Vector2 endPos)
    {
        if (endPos.x > currentPos.x) { return Vector2.right; }
        return Vector2.left;
    }
}