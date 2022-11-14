using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChooser : MonoBehaviour
{
    private int currentWeapon;
    [SerializeField] private Sprite[] weaponsSprites;
    [SerializeField] private GameObject goToLevelBTN;

    private void Start()
    {
        StartCoroutine(DiceRoller());
    }
    IEnumerator DiceRoller()
    {
        while (true)
        {
            currentWeapon++;
            if (currentWeapon >= weaponsSprites.Length)
            {
                currentWeapon = 0;
            }
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponsSprites[currentWeapon];
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        SoundController.sc.PlaySFX(SoundController.sc.hitDiceSFX);
        DATA.d.currenteWeapon = currentWeapon;
        goToLevelBTN.SetActive(true);
    }
}
