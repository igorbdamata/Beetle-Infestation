using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int totalLife;
    public int currentLife { get; private set; }

    [SerializeField] private float invencibleCooldown;
    [SerializeField] private int blinkTimes;
    [SerializeField] private SpriteRenderer[] spritesToBlink;
    [SerializeField] private float deadAnimationTime;
    public bool isInvencible { get; private set; }
    public bool isDead { get; private set; }

    [Header("Camera shake")]
    [SerializeField] private float shakeTime;
    [SerializeField] private AnimationCurve gainCurve;
    [SerializeField] private AnimationCurve frequencyCurve;

    private void Start()
    {
        currentLife = totalLife;
        if (blinkTimes % 2 != 0) { blinkTimes--; }
    }
    public void AddDamage(int damage)
    {
        if (isInvencible || isDead) { return; }
        GetComponent<PlayerAttack>().StopAttack();
        currentLife -= damage;
        UI.ui.UpdateLifeBar(this, totalLife);
        if (currentLife <= 0)
        {
            StartCoroutine(Dead());
            return;
        }
        isInvencible = true;
        GameController.gc.camShake.SetShake(gainCurve, frequencyCurve, shakeTime);
        StartCoroutine(InvencibleCooldown());
    }

    IEnumerator Dead()
    {
        isDead = true;
        yield return new WaitForSeconds(deadAnimationTime);
        UI.ui.SetGameOverScreen();
    }

    public void Cure(int valueToCure)
    {
        currentLife += valueToCure;
    }

    IEnumerator InvencibleCooldown()
    {
        isInvencible = true;
        float timeToWait = invencibleCooldown / blinkTimes;
        float timeWaited = 0;
        while (timeWaited <= invencibleCooldown)
        {
            for (int i = 0; i < spritesToBlink.Length; i++)
            {
                spritesToBlink[i].enabled = !spritesToBlink[i].enabled;
            }
            timeWaited += timeToWait;
            yield return new WaitForSeconds(timeToWait);
        }
        for (int i = 0; i < spritesToBlink.Length; i++)
        {
            spritesToBlink[i].enabled = true;
        }
        isInvencible = false;
    }
}
