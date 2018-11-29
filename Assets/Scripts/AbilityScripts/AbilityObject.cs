using System.Collections.Generic;
using UnityEngine;

public class AbilityObject : MonoBehaviour {

    EnemyStatus e;
    Ability ability;

    int abilityKey;
    bool channeling = false;

    PlayerStatus player;
    Animator animator;
    List<EnemyStatus> enemiesInArea;

    private void Start()
    {
        enemiesInArea   = new List<EnemyStatus>();
        player = PlayerStatus.instance;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            e = collision.transform.parent.GetComponent<EnemyStatus>();
            if (!enemiesInArea.Contains(e)) { enemiesInArea.Add(e); }
            if (!ability.ChannelAbility)
            {
                UseAbility();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            e = collision.transform.parent.GetComponent<EnemyStatus>();
            if (enemiesInArea.Contains(e)) { enemiesInArea.Remove(e); }
        }
    }

    private void Update()
    {
        bool[] Inputs = { Input.GetButton("Ability1"), Input.GetButton("Ability2"), Input.GetButton("Ability3"), Input.GetButton("Ability4") };
        if (ability.ChannelAbility)
        {
            channeling = Inputs[abilityKey];
            if (channeling) { channeling = player.UseEnergy(ability.EnergyUsage * Time.deltaTime); }
            animator.SetBool("Channeling", channeling);

            foreach (EnemyStatus e in enemiesInArea) { UseChannel(e); }
        }
    }

    public virtual void SetupAbility(Ability ability, int abilityKey, bool parent = true)
    {
        if (ability.MultipartAbility && parent) { foreach (AbilityObject childAbility in GetComponentsInChildren<AbilityObject>()) {
                childAbility.SetupAbility(ability, abilityKey, false);
            } }

        this.ability = ability;
        this.abilityKey = abilityKey;

        if (ability.ChannelAbility) {
            animator = GetComponent<Animator>();
        }
    }

    public virtual void UseAbility()
    {
        float baseDamage = 0;
        player.CalculateBaseDamage(out baseDamage);     // TODO Deal with crits

        e.TakeDamage((ability.Damage / 100) * baseDamage);
        player.RecoverResource(ability.EnergyRecovery, false);
    }

    public virtual void UseChannel(EnemyStatus e)
    {
        float baseDamage = 0;
        player.CalculateBaseDamage(out baseDamage);

        e.TakeDamage((ability.Damage / 100) * baseDamage * Time.deltaTime);
        player.RecoverResource(ability.EnergyRecovery * Time.deltaTime, false);
    }
}
