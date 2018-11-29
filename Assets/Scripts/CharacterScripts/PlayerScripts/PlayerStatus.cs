using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{    
    // [ Defensive Options ]
    float maximumHealth;
    float currentHealth;
    float damageReduction;
    float damageResistance;
    float lifePerKill;
    float lifePerSecond;
    float crowdControlReduction;
    float maxCrowdControlReduction;

    // [ Offensive Options ]
    float baseDamage;
    float critDamage;
    float critChance;

    // [ Magic Options ]
    float maximumEnergy;
    float currentEnergy;
    float energyPerSecond;
    float cooldownReduction;
    float maximumCooldownReduction;
    float skillCostReduction;
    float maximumSkillCostReduction;

    // [ Misc  Options ]
    public float moveSpeed { get; private set; }
    float damageReflection;
    float currentExperience;
    float level;
    float maximumLevel;

    // Parameters    
    public ResourceGlobe HealthGlobe;
    public ResourceGlobe EnergyGlobe;

    #region Singleton
    public static PlayerStatus instance;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(gameObject); }
    }
    #endregion

    public void SetupPlayer(PlayerClass SelectedClass)
    {
        // [ Defensive Options ]
         maximumHealth = SelectedClass.MaximumHealth;
         currentHealth = maximumHealth;
         damageReduction = SelectedClass.DamageReduction;
         damageResistance = SelectedClass.DamageResistance;
         lifePerKill = SelectedClass.LifePerKill;
         lifePerSecond = SelectedClass.LifePerSecond;
         crowdControlReduction = SelectedClass.CrowdControlReduction;
         maxCrowdControlReduction = SelectedClass.MaxCrowdControlReduction;

        // [ Offensive Options ]
         baseDamage = SelectedClass.BaseDamage;
         critDamage = SelectedClass.CritDamage;
         critChance = SelectedClass.CritChance;

        // [ Magic Options ]
         maximumEnergy = SelectedClass.MaximumEnergy;
         currentEnergy = maximumEnergy;
         energyPerSecond = SelectedClass.EnergyPerSecond;
         cooldownReduction = SelectedClass.CooldownReduction;
         maximumCooldownReduction = SelectedClass.MaximumCooldownReduction;
         skillCostReduction = SelectedClass.SkillCostReduction;
         maximumSkillCostReduction = SelectedClass.MaximumSkillCostReduction;

        // [ Misc  Options ]
         moveSpeed = SelectedClass.MoveSpeed;
         damageReflection = SelectedClass.DamageReflection;
         currentExperience = SelectedClass.CurrentExperience;
         level = SelectedClass.Level;
         maximumLevel = SelectedClass.MaximumLevel;

        // Setup everything else
    }

    private void Update()
    {
        // Player regeneration
        if (lifePerSecond > 0)
        {
            RecoverResource(lifePerSecond * Time.deltaTime);
        }

        if (energyPerSecond > 0)
        {
            RecoverResource((energyPerSecond * Time.deltaTime), false);
        }
    }

    /// <summary>
    /// Restarts the level and brings up the end of level screens
    /// </summary>
    void Die()
    {
        GameController.instance.PlayerDeath();
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Cause the player to take damage, the damage will be run through player defenses before being applied to health.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns>Amount of damage reflected back</returns>
    public float TakeDamage(float damage)
    {
        // Calculate damage resistance
        float damageResistancePercentage = 1.0f - (damageResistance / (damageResistance + 3500));

        // Damage resistance is applied first
        damage *= damageResistancePercentage;

        // Damage reduction is applied second, 0 or less damage does nothing so we return
        damage -= damageReduction;
        if (damage <= 0) { return damageReflection; }

        // Than our health takes that damage
        currentHealth -= damage;
        if (currentHealth <= 0) { Die(); }

        HealthGlobe.SetGlobeValue(currentHealth / maximumHealth);

        return damageReflection;
    }

    /// <summary>
    /// Use a specified amount of energy, reduction calculations are factored in. Returns if there is enough energy to use
    /// </summary>
    /// <param name="energy"></param>
    /// <returns>Is it possible to use this energy?</returns>
    public bool UseEnergy(float energy)
    {
        // Apply cost reduction, return if less than or equal to 0
        energy *= (1.0f - (skillCostReduction / 100.0f));
        if (energy <= 0) { return true; }

        if (energy > currentEnergy) { return false; }

        currentEnergy -= energy;
        EnergyGlobe.SetGlobeValue(currentEnergy / maximumEnergy);

        return true;
    }

    /// <summary>
    /// Recover either health or energy by an amount
    /// </summary>
    /// <param name="amount"></param>
    /// <param name="health">Set to false to recover energy instead of health</param>
    public void RecoverResource(float amount, bool health = true)
    {
        if (amount <= 0) { return; }

        if (health)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maximumHealth);
            HealthGlobe.SetGlobeValue(currentHealth / maximumHealth);
        }
        else
        {
            currentEnergy = Mathf.Clamp(currentEnergy + amount, 0, maximumEnergy);
            EnergyGlobe.SetGlobeValue(currentEnergy / maximumEnergy);
        }
    }

    /// <summary>
    /// Calculates base damage from crit chance and crit damage
    /// </summary>
    /// <returns>Base damage</returns>
    public bool CalculateBaseDamage(out float baseDamage)
    {
        bool crit = Random.Range(0, 100) < critChance;
        baseDamage = crit ? this.baseDamage * (critDamage / 100) : this.baseDamage;

        return crit;
    }

    /// <summary>
    /// Actions to perform when an enemy is killed
    /// </summary>
    public void EnemyKilled()
    {
        RecoverResource(lifePerKill);
    }
}
