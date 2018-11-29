using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : AbilityObject {

    public override void SetupAbility(Ability ability, int abilityKey, bool parent = true)
    {
        PlayerStatus.instance.transform.position = this.transform.position;
        Destroy(gameObject);
    }
}
