using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantData : MonoBehaviour {

    int numberOfDeaths;
    int score;
    int money;
    PlayerClass playerClass;
    Ability[] abilities = new Ability[4];

    #region Singleton
    public static PersistantData instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        numberOfDeaths = -1;
        if (instance == null) { instance = this; }
        else if (instance != this) { Destroy(this.gameObject); }
    }
    #endregion

    private void Start()
    {   
        playerClass = GameController.instance.SelectedClass;
        abilities = GameController.instance.Abilities;
    }

    private void OnLevelWasLoaded(int level)
    {
        numberOfDeaths++;
        if (numberOfDeaths <= 0) { return; }
        
        GameController.instance.SetupGame(numberOfDeaths, playerClass, abilities);

        CurrencyController.instance.increaseScore(score);
        CurrencyController.instance.increaseMoney(money);
    }

    public void PrepareToLoad()
    {
        {   // Shallow copy of the players class

            PlayerClass t = new PlayerClass();

            PlayerClass SelectedClass = GameController.instance.SelectedClass;
            // [ Defensive Options ]
            t.MaximumHealth = SelectedClass.MaximumHealth;
            t.DamageReduction = SelectedClass.DamageReduction;
            t.DamageResistance = SelectedClass.DamageResistance;
            t.LifePerKill = SelectedClass.LifePerKill;
            t.LifePerSecond = SelectedClass.LifePerSecond;
            t.CrowdControlReduction = SelectedClass.CrowdControlReduction;
            t.MaxCrowdControlReduction = SelectedClass.MaxCrowdControlReduction;

            // [ Offensive Options ]
            t.BaseDamage = SelectedClass.BaseDamage;
            t.CritDamage = SelectedClass.CritDamage;
            t.CritChance = SelectedClass.CritChance;

            // [ Magic Options ]
            t.MaximumEnergy = SelectedClass.MaximumEnergy;
            t.EnergyPerSecond = SelectedClass.EnergyPerSecond;
            t.CooldownReduction = SelectedClass.CooldownReduction;
            t.MaximumCooldownReduction = SelectedClass.MaximumCooldownReduction;
            t.SkillCostReduction = SelectedClass.SkillCostReduction;
            t.MaximumSkillCostReduction = SelectedClass.MaximumSkillCostReduction;

            // [ Misc  Options ]
            t.MoveSpeed = SelectedClass.MoveSpeed;
            t.DamageReflection = SelectedClass.DamageReflection;
            t.CurrentExperience = SelectedClass.CurrentExperience;
            t.Level = SelectedClass.Level;
            t.MaximumLevel = SelectedClass.MaximumLevel;
            t.name = SelectedClass.name;

            playerClass = t;
        }

        // Shallow copy of all the abilities
        Ability[] Abilities = GameController.instance.Abilities;
        for (int i = 0; i < abilities.Length; i++)
        {
            Ability t = new Ability();

            t.AtMouse = Abilities[i].AtMouse;
            t.ChannelAbility = Abilities[i].ChannelAbility;
            t.Cooldown = Abilities[i].Cooldown;
            t.Damage = Abilities[i].Damage;
            t.DroppedAbility = Abilities[i].DroppedAbility;
            t.EnergyRecovery = Abilities[i].EnergyRecovery;
            t.EnergyUsage = Abilities[i].EnergyUsage;
            t.Icon = Abilities[i].Icon;
            t.MultipartAbility = Abilities[i].MultipartAbility;
            t.PlayerAnimationTrigger = Abilities[i].PlayerAnimationTrigger;
            t.RootedTime = Abilities[i].RootedTime;
            t.Tooltip = Abilities[i].Tooltip;
            t.UsedAbility = Abilities[i].UsedAbility;

            t.name = Abilities[i].name;

            abilities[i] = t;
        }        

        score = CurrencyController.instance.GetScore();
        money = CurrencyController.instance.GetMoney();
    }
}
