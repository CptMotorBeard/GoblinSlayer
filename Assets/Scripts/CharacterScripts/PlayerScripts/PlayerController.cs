using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Vector3 targetPosition;
    PlayerStatus player;

    Animator animator;
    
    bool rooted = false;
    bool rootedChannel = false;
    bool channelingAbility = false;
    int  channelIndex = 0;
    bool canAttack = true;

    Ability[] abilities;

    // Parameters
    public AbilityPanel Panel;    

    public Transform PlayerAbilityTransform;
    public Transform WorldAbilityTransform;

    public void SetupController(Ability[] a)
    {
        abilities = a;

        player = PlayerStatus.instance;
        animator = this.GetComponent<Animator>();
        targetPosition = this.transform.position;

        for (int i = 0; i < 4; i++) { Panel.ChangeAbility(i, abilities[i].Icon, abilities[i].Cooldown); }
    }
	
	// Update is called once per frame
	void Update () {
        if (abilities.Length != 4) { Debug.LogError("There are not enough abilities");  return; }       // We aren't allowed to have less than or more than a list of 4

        // Deal with inputs and channelling
        bool[] Inputs = { Input.GetButton("Ability1"), Input.GetButton("Ability2"), Input.GetButton("Ability3"), Input.GetButton("Ability4") };
        
        if (channelingAbility)
        {
            channelingAbility = Inputs[channelIndex];
            if (rootedChannel) { rootedChannel = rooted = channelingAbility; }
            if (!channelingAbility) { Panel.SetOnCooldown(channelIndex); }
        }

        if ((this.transform.position != targetPosition) && !rooted) { animator.SetBool("Walking", true); }
        else { animator.SetBool("Walking", false); }

        // Aim towards mouse cursor
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.eulerAngles = new Vector3(0, 0, 90 + Mathf.Rad2Deg * Mathf.Atan2(transform.position.y - mousePosition.y, transform.position.x - mousePosition.x));

        for (int i = 0; i < 4; i++) {       // Ignore additional channeling inputs
            if ((channelingAbility && channelIndex != i) || !channelingAbility) { if (Inputs[i]) { UseAbility(i); } }            
        }

        // Move towards right clicks
        if (Input.GetMouseButton(1)) { targetPosition = mousePosition; }
        if (!rooted) { this.transform.position = Vector2.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * player.moveSpeed); }

	}

    /// <summary>
    /// Use the specified ability a, all player calculations are handled, such as rooted
    /// </summary>
    /// <param name="a"></param>
    void UseAbility(int index)
    {
        if (!canAttack) { return; }

        // Check player can cast ability
        if (abilities[index] == null) { return; }       // Check for empty slot

        if (Panel.AbilityOnCooldown(index)) { return; }
        if (!player.UseEnergy(abilities[index].EnergyUsage)) { return; }        

        if (abilities[index].ChannelAbility) { channelingAbility = true; channelIndex = index; }

        if (abilities[index].RootedTime > 0) {
            targetPosition = this.transform.position;
            if (abilities[index].ChannelAbility) { rootedChannel = true; }
            else { StartCoroutine(RootForDuration(abilities[index].RootedTime)); }
        }

        // Check ability location
        Transform parent = abilities[index].DroppedAbility ? WorldAbilityTransform : PlayerAbilityTransform;
        Vector3 position;
        if (abilities[index].AtMouse)
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
        }
        else { position = this.transform.position; }

        // Spawn the ability and set its base damage
        AbilityObject ability = Instantiate(abilities[index].UsedAbility, position, transform.rotation, parent);
        ability.transform.localScale = new Vector3(1, 1, 1);
        
        ability.SetupAbility(abilities[index], index);

        // Perform other ability actions
        if (abilities[index].Cooldown > 0 && !abilities[index].ChannelAbility) { Panel.SetOnCooldown(index); }

        // Trigger the player animation
        if (abilities[index].PlayerAnimationTrigger != "") { animator.SetTrigger(abilities[index].PlayerAnimationTrigger); }     
    }

    IEnumerator RootForDuration(float duration)
    {
        rooted = true;
        yield return new WaitForSeconds(duration);
        rooted = false;
    }
}