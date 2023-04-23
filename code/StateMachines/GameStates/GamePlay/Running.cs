using Godot;

namespace code.StateMachines.GameStates.GamePlay
{
    public class Running : GamePlay
    {
        public Running(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            if (Input.IsActionJustPressed("pause"))
            {
                return this.SwitchState(new Paused(masterNode));
            }
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
