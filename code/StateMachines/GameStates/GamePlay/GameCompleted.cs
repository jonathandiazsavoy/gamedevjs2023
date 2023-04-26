using Godot;

namespace code.StateMachines.GameStates.GamePlay
{
    public class GameCompleted : GamePlay
    {
        public GameCompleted(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            if (masterNode.GetNode<WaveCompletedScreen>("/root/Master/GameCompleted/Control").continuePressed) return this.SwitchState(new OnTitleScreen(masterNode));
            return this;
        }

        protected override void EnterState()
        {
            GameManager gameManager = masterNode.GameManager;
            gameManager.GetTree().Paused = true;
            gameManager.MusicPlayer.Stop();
        }
        protected override void ExitState()
        {
            masterNode.GetNode<CanvasLayer>("WaveCompleted").QueueFree();
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
