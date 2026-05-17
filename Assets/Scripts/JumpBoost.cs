using UnityEngine;

public class JumpBoost : Boost
{
    [SerializeField] float multiplier;
    float _originalJumpForce;

    public override void PickUp()
    {
        _originalJumpForce = playerMove.JumpForce;
        playerMove.SetJumpForce(playerMove.JumpForce * multiplier);
        base.PickUp();
    }

    public override void Remove()
    {
        playerMove.SetJumpForce(_originalJumpForce);
    }
}
