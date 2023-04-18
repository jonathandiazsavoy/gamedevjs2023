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
            return this;
        }

        protected override void EnterState()
        {
            player.SoundPlayer.Play("take damage");
        }
    }
}
