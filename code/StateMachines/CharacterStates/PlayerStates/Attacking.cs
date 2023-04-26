namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class Attacking : PlayerState
    {
        private readonly BaseFSMState previousState;
        private float orientation;

        public Attacking(Player player, BaseFSMState previousState, float orientation) : base(player)
        {
            this.previousState = previousState;
            this.orientation = NormalizeRotationDegrees(orientation);
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
            // Handle setting of correct sprite animation according to orientation
            float degrees = orientation;
            if (degrees >= 225 && degrees < 315)
            {
                // Up
                player.AnimationPlayer.Play("melee_attack_up");
            }
            else if (degrees >= 135 && degrees < 225)
            {
                // Left
                player.AnimationPlayer.Play("melee_attack_left");

            }
            else if (degrees >= 45 && degrees < 135)
            {
                // Down
                player.AnimationPlayer.Play("melee_attack_down");
            }
            else
            {
                // Right
                player.AnimationPlayer.Play("melee_attack_right");
            }

            player.SoundPlayer.Play("melee_attack");
        }
    }
}
