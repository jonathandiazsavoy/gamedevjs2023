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
            return this;
        }

        protected override void EnterState()
        {
            player.AnimationPlayer.Play("Run"); // TODO rename
        }

        protected override void ExitState()
        {
            //throw new NotImplementedException();
        }
    }
}
