using Godot;

namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class Idle : PlayerState
    {
        private float orientation;

        public Idle(Player player, float orientation) : base(player)
        {
            this.orientation = NormalizeRotationDegrees(orientation);
        }

        public override BaseFSMState HandleInput(float delta)
        {
            float xMove = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            float yMove = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            Vector2 moveDirection = new Vector2(xMove, yMove).Normalized();

            if (Input.IsActionJustPressed("attack"))
            {
                return this.SwitchState(new Attacking(player, this, player.RotationDegrees));
            }
            if (!moveDirection.Equals(Vector2.Zero))
            {
                return this.SwitchState(new Moving(player, player.RotationDegrees));
            }
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            // Handle setting of correct sprite animation according to orientation
            float degrees = orientation;
            if (degrees >= 225 && degrees < 315)
            {
                // Up
                player.AnimationPlayer.Play("idle_up");
            }
            else if (degrees >= 135 && degrees < 225)
            {
                // Left
                player.AnimationPlayer.Play("idle_left");

            }
            else if (degrees >= 45 && degrees < 135)
            {
                // Down
                player.AnimationPlayer.Play("idle_down");
            }
            else
            {
                // Right
                player.AnimationPlayer.Play("idle_right");
            }
        }
    }
}
