using Godot;

namespace code.StateMachines.CharacterStates.PlayerStates
{
    public class Idle : PlayerState
    {
        public Idle(Player player) : base(player)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            float xMove = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
            float yMove = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
            Vector2 moveDirection = new Vector2(xMove, yMove).Normalized();

            if (Input.IsActionJustPressed("attack"))
            {
                return this.SwitchState(new Attacking(player, this));
            }
            if (!moveDirection.Equals(Vector2.Zero))
            {
                return this.SwitchState(new Moving(player));
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
    }
}
