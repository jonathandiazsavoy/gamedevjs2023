using Godot;

namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class Moving : PlayerState
    {
        public Moving(Player player) : base(player)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            // TODO move this to input handler
            float xMove = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            float yMove = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            player.currentMoveDirection = new Vector2(xMove, yMove).Normalized();

            if (Input.IsActionJustPressed("attack"))
            {
                return this.SwitchState(new Attacking(player, this));
            }
            if (player.currentMoveDirection.Equals(Vector2.Zero))
            {
                return this.SwitchState(new Idle(player));
            }
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            player.MoveAndCollide(player.currentMoveDirection * player.CurrentStats.Speed);
            player.SetCharacterOrientation(player.currentMoveDirection);

            // Handle setting of correct sprite animation according to orientation
            float degrees = NormalizeRotationDegrees(player.RotationDegrees);
            if (Mathf.Abs(previousRotationDegrees - degrees) > 45)
            {
                if(degrees >= 225 && degrees < 315)
                {
                    // Up
                    player.AnimationPlayer.Play("move_up");
                    previousRotationDegrees = degrees;
                }
                else if (degrees >= 135 && degrees < 225)
                {
                    // Left
                    player.AnimationPlayer.Play("move_left");
                    previousRotationDegrees = degrees;
                    
                }
                else if (degrees >= 45 && degrees < 135)
                {
                    // Down
                    player.AnimationPlayer.Play("move_down");
                    previousRotationDegrees = degrees;
                }
                else
                {
                    // Right
                    player.AnimationPlayer.Play("move_right");
                    previousRotationDegrees = degrees;
                }

            }

            return this;
        }

        protected override void EnterState()
        {
            //player.AnimationPlayer.Play("Run"); // TODO rename
        }
    }
}
