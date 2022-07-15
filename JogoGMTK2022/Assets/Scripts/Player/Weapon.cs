using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons")]
public class Weapon : ScriptableObject
{
    [SerializeField] private int weaponDamage;
    [SerializeField] private float weaponAttackCooldown;
    [SerializeField] private AnimationCurve weaponAttackAnimationSpeed;
    [SerializeField] private Sprite weaponSprite;

    public int damage { get => weaponDamage; }
    public float attackCooldown { get => weaponAttackCooldown; }
    public AnimationCurve attackAnimationSpeed { get => weaponAttackAnimationSpeed; }
    public Sprite sprite { get => weaponSprite; }
}