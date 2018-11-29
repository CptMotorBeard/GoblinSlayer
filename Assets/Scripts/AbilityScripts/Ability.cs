using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Ability", menuName = "New Ability")]

public class Ability : ScriptableObject {

    public Sprite Icon;                      // The icon for the UI
    public AbilityObject UsedAbility;           // The object created by this ability
    public float Damage;                     // Damage of the ability, percentage
    public float Cooldown;                   // Cooldown of ability in seconds
    public float EnergyUsage;                // How much energy it uses
    public float EnergyRecovery;             // How much energy the ability recovers
    public float RootedTime;                 // Does this ability root you in place (> 0 seconds)
    public bool  ChannelAbility;             // Is this ability a channel ability (RootedTime acts as a boolean for a channel)
    public bool  MultipartAbility;           // Does this ability have children ability objects?
    public bool  DroppedAbility;             // Does this ability follow the player, or is it dropped in place
    public bool  AtMouse;                    // Is this ability dropped at the mouse location?
    [Tooltip("String for a player animation trigger, leave blank for no animation")]
    public string PlayerAnimationTrigger;    // The string for the player animation trigger to play when using this ability
    [TextArea(2, 4)]
    [Tooltip("{0} inserts damage number, {1} inserts cooldown, {2} inserts energy use, {3} inserts energy recovery, {4} inserts rooted time")]
    public string Tooltip;              // The tooltip given by the UI

    /// <summary>
    /// Formats the tooltip to insert the ability values into it
    /// </summary>
    /// <returns>The formatted tooltip</returns>
    public string GetFormattedToolTip()
    {
        return String.Format(Tooltip, Damage, Cooldown, EnergyUsage, EnergyRecovery, RootedTime);
    }
}
