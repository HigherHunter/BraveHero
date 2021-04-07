using UnityEngine;

//get trigger from animation
public class AnimationEventReceiver : MonoBehaviour {

    public CharacterCombat combat;

    //if triggered sends info to combat
	public void AttackHitEvent()
    {
        combat.AttackHitAnimation();
    }
}
