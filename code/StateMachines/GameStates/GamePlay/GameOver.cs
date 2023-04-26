using Godot;

namespace code.StateMachines.GameStates.GamePlay
{
    public class GameOver : GamePlay
    {
        public GameOver(Master masterNode) : base(masterNode)
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
            GameManager gameManager = masterNode.GameManager;
            gameManager.GetTree().Paused = true;
            PackedScene packed = GD.Load<PackedScene>(Master.PATH_TO_MENUS + "game_over_menu" + ".tscn");
            masterNode.AddChild(packed.Instance());
        }

        protected override void ExitState()
        {
            masterNode.GetNode<CanvasLayer>(Master.NODE_PATH_TO_GAME_OVER_MENU).QueueFree();
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
