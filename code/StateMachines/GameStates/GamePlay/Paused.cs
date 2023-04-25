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
            PackedScene packed = GD.Load<PackedScene>(Master.PATH_TO_MENUS + "pause_menu" + ".tscn");
            masterNode.AddChild(packed.Instance());
        }

        protected override void ExitState()
        {
            masterNode.GetNode<CanvasLayer>(Master.NODE_PATH_TO_PAUSE_MENU).QueueFree();
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
