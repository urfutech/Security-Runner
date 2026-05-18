using UnityEngine;

public class JumpBoost : Boost
{
    [SerializeField] float multiplier;

    public override void PickUp()
    {
        if (!isBoostActive)
            playerMove.SetJumpForce(playerMove.JumpForce * multiplier);
        base.PickUp();
    }

    public override void Remove()
    {
        if (isBoostActive)
        {
			isBoostActive = false;
			playerMove.RecoverJumpForce();
		}
    }
}
