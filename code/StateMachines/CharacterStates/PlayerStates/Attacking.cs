using Godot;

namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class Attacking : PlayerState
    {
        private readonly BaseFSMState previousState;

        public Attacking(Player player, BaseFSMState previousState) : base(player)
        {
            this.previousState = previousState;
        }

        public override BaseFSMState HandleInput(float delta)
        {
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            if (!player.AnimationPlayer.IsPlaying()) return this.SwitchState(this.previousState);
            return this;
        }

        protected override void EnterState()
        {
            GD.Print("current orientation: " + player.RotationDegrees);
            player.AnimationPlayer.Play("Attack"); //TODO rename
            player.SoundPlayer.Play("melee_attack");
        }
    }
}
