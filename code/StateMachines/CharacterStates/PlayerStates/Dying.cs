namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class Dying : PlayerState
    {
        public Dying(Player player) : base(player)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            player.AnimationPlayer.Play("death");
            player.SoundPlayer.Play("death");
        }
    }
}
