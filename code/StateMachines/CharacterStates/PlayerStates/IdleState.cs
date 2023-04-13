using Godot;

public class IdleState : PlayerState
{
    public IdleState(Player player) : base(player)
    {
    }

    public override BaseFSMState HandleInput()
    {
        float xMove = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
        float yMove = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
        Vector2 moveDirection = new Vector2(xMove, yMove).Normalized();

        if (Input.IsActionJustPressed("attack"))
        {
            return this.SwitchState(new AttackState(player, this));
        }
        if (!moveDirection.Equals(Vector2.Zero))
        {
            return this.SwitchState(new MoveState(player));
        }
        return this;
    }

    public override BaseFSMState Update(float delta)
    {
        return this;
    }

    protected override void EnterState()
    {
        player.AnimationPlayer.Play("RESET");
    }

    protected override void ExitState()
    {
        //throw new NotImplementedException();
    }
}
