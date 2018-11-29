using UnityEngine;

public class AbilityPanel : MonoBehaviour {

    public AbilityIcon[] Icons;

    public void ChangeAbility(int ability, Sprite newIcon, float newCooldown)
    {
        Icons[ability].ChangeIcon(newIcon);
        Icons[ability].SetCooldown(newCooldown);
    }

    public void SetOnCooldown(int ability)
    {
        Icons[ability].StartCooldown();
    }

    public bool AbilityOnCooldown(int ability)
    {
        return Icons[ability].onCooldown;
    }
}
