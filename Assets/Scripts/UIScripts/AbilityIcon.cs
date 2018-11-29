using UnityEngine.UI;
using UnityEngine;

public class AbilityIcon : MonoBehaviour {

    float cooldownTimer;
    float currentCooldown;

    public bool onCooldown { get; private set; }

    // Parameters
    public Image CooldownIndicator;
    public Image Icon;

    /// <summary>
    /// Change the displayed icon of this ability
    /// </summary>
    /// <param name="newIcon"></param>
    public void ChangeIcon(Sprite newIcon)
    {
        Icon.sprite = newIcon;
    }

    /// <summary>
    /// Set the cooldown of the ability
    /// </summary>
    /// <param name="newCooldown"></param>
    public void SetCooldown(float newCooldown)
    {
        cooldownTimer = newCooldown;
    }

    /// <summary>
    /// Start the ability on cooldown
    /// </summary>
    public void StartCooldown()
    {
        currentCooldown = 0;
        CooldownIndicator.fillAmount = 1;
        onCooldown = true;
    }

    private void Update()
    {
        if (onCooldown)
        {
            currentCooldown += Time.deltaTime;
            CooldownIndicator.fillAmount = 1 - (currentCooldown / cooldownTimer);
            if (currentCooldown >= cooldownTimer) { onCooldown = false; }
        }
    }
}
