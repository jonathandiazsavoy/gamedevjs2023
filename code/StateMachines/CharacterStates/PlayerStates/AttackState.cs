public class AttackState : PlayerState
{
    private readonly BaseFSMState previousState;

    public AttackState(Player player, BaseFSMState previousState) : base(player)
    {
        this.previousState = previousState;
    }

    public override BaseFSMState HandleInput()
    {
        return this;
    }

    public override BaseFSMState Update(float delta)
    {
        if(!player.AnimationPlayer.IsPlaying()) return this.SwitchState(this.previousState);
        return this;
    }

    protected override void EnterState()
    {
        player.AnimationPlayer.Play("Attack");
    }

    protected override void ExitState()
    {
        //throw new NotImplementedException();
    }
}
