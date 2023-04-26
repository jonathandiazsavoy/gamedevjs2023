using Godot;

namespace code.StateMachines.GameStates.GamePlay
{
    public class StartingNewWave : InCutscene
    {
        const float transitionTime = 5f;
        private float timeWaited;

        public StartingNewWave(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            if (Input.IsActionJustPressed("ui_accept"))
            {
                // Can skip
                //return this.SwitchState(new Running(masterNode));
            }
            return this;
        }

        public override BaseFSMState Update(float delta)
        {
            // Wait until transition time
            timeWaited += delta;
            if (timeWaited > transitionTime) return this.SwitchState(new Running(masterNode));
            return this;
        }

        protected override void EnterState()
        {
            masterNode.SoundPlayer.Play("transition");
            GameManager gameManager = masterNode.GameManager;
            gameManager.StartNewWave();
            gameManager.GetTree().Paused = true;
            masterNode.Camera2D.ZoomOutOnMap();
        }

        protected override void ExitState()
        {
            masterNode.Camera2D.FollowPlayer();
            masterNode.GameManager.GetTree().Paused = false;
            masterNode.GameManager.MusicPlayer.Play("calm_phase");
        }
    }
}
