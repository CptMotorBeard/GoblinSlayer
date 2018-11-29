using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Class", menuName = "New Player Class")]
public class PlayerClass : ScriptableObject {
    /*
     * [ Defensive Options ]
     * Health               (Max, Current)
     * Damage Reduction     (Flat damage reduction)
     * Damage Resistance    (Percentage damage reduced, calculated through [ DR / (DR + 3500)])
     * Life per Kill
     * Life per Second
     * CC Reduction         (Percentage, up to a maximum)
     * Maximum CC Reduction (Useful for multiple classes and/or characters)
     * 
     * [ Offensive Options ]
     * Base Damage          (Modified by abilities to calculate damage dealt)
     * Crit Damage          (Multiplier)
     * Crit Chance          (Percentage)
     * 
     * [ Magic Options ]
     * Energy / Mana
     * Energy / Mana Regen
     * CDR                  (Percentage, up to a maximum)
     * Maximum CDR          (Useful for multiple classes and/or characters)
     * Cost Reduction
     * Max Cost Reduction   (Useful for multiple classes and/or characters
     *
     * [Misc Options]
     * Speed
     * Damage Reflection    (Either a flat number or a percentage of base damage)
     * Current Experience
     * Experience Curve     (Method to figure out amount of experience to level up)
     * Level
     * Maximum Level
     * 
    */

    [Header("Defensive Options")]
    public float MaximumHealth              = 100;
    public float DamageReduction            =   0;
    public float DamageResistance           =   0;
    public float LifePerKill                =   0;
    public float LifePerSecond              =   0;
    public float CrowdControlReduction      =   0;
    public float MaxCrowdControlReduction   =  40;

    [Header("Offensive Options")]
    public float BaseDamage                 =  10;
    public float CritDamage                 = 125;
    public float CritChance                 =   5;

    [Header("Magic Options")]
    public float MaximumEnergy              = 100;
    public float EnergyPerSecond            =   5;
    public float CooldownReduction          =   0;
    public float MaximumCooldownReduction   =  40;
    public float SkillCostReduction         =   0;
    public float MaximumSkillCostReduction  =  40;

    [Header("Misc  Options")]
    public float MoveSpeed                  =  10;
    public float DamageReflection           =   0;
    public float CurrentExperience          =   0;
    public float Level                      =   1;
    public float MaximumLevel               =  99;

    /// <summary>
    /// Return the amount experience required to get to the next level based on given level
    /// </summary>
    /// <param name="level"></param>
    /// <returns>Experience to next level</returns>
    public float ExperienceCurve(float level)
    {
        return 0;
    }
}
