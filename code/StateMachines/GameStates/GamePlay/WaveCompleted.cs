using Godot;

namespace code.StateMachines.GameStates.GamePlay
{
    public class WaveCompleted : GamePlay
    {
        public WaveCompleted(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            //if (masterNode.GetNodeOrNull<WaveCompletedScreen>("WaveCompleted/Control") == null) return this;
            if (masterNode.GetNode<WaveCompletedScreen>("/root/Master/WaveCompleted/Control").continuePressed) return this.SwitchState(new StartingNewWave(masterNode));
            return this;
        }

        protected override void EnterState()
        {
            GameManager gameManager = masterNode.GameManager;
            gameManager.GetTree().Paused = true;
            masterNode.LoadScreen("wave_completed");
            gameManager.MusicPlayer.Stop();
        }
        protected override void ExitState()
        {
            masterNode.GetNode<CanvasLayer>("WaveCompleted").QueueFree();
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
