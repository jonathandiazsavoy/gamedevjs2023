namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class TakingDamage : PlayerState
    {
        public TakingDamage(Player player) : base(player)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            if (!player.AnimationPlayer.IsPlaying()) return this.SwitchState(new Idle(player));
            player.MoveAndSlide(player.incomingAttackForce);
            return this;
        }

        protected override void EnterState()
        {
            player.AnimationPlayer.Play("take_damage");
            player.SoundPlayer.Play("take_damage");
        }
    }
}
