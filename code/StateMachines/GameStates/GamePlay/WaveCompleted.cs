﻿using Godot;

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
            if (masterNode.GetNode<WaveCompletedScreen>("WaveCompleted/Control").continuePressed) return this.SwitchState(new Running(masterNode));
            return this;
        }

        protected override void EnterState()
        {
            GameManager gameManager = masterNode.GameManager;
            gameManager.GetTree().Paused = true;
            masterNode.LoadScreen("wave_completed");
        }
        protected override void ExitState()
        {
            masterNode.GetNode<CanvasLayer>("WaveCompleted").QueueFree();
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
