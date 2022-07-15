using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private int totalLife;
    public int currentLife { get; private set; }

    [SerializeField] private float invencibleCooldown;
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
    }
    public void AddDamage(int damage)
    {
        if (isInvencible) { return; }
        currentLife -= damage;
        if(currentLife<=0)
        {
            StartCoroutine(Dead());
            isInvencible = true;
            return;
        }

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
        yield return new WaitForSeconds(invencibleCooldown);
        isInvencible = false;
    }
}
