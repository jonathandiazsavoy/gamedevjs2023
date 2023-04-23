using Godot;

namespace code.StateMachines.GameStates.GamePlay
{
    public class Paused : GamePlay
    {
        public Paused(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            if (Input.IsActionJustPressed("pause"))
            {
                return this.SwitchState(new Running(masterNode));
            }
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            GameManager gameManager = masterNode.GameManager;
            masterNode.SoundPlayer.Play();
            gameManager.GetTree().Paused = true;
        }
    }
}
