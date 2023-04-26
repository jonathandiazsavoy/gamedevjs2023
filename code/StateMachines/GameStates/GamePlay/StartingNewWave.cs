using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.StateMachines.GameStates.GamePlay
{
    public class StartingNewWave : InCutscene
    {
        public StartingNewWave(Master masterNode) : base(masterNode)
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
            // do loading
            // unpause
            // play start new wave animation
            GameManager gameManager = masterNode.GameManager;
            masterNode.SoundPlayer.Play("pause");
            gameManager.GetTree().Paused = true;
            PackedScene packed = GD.Load<PackedScene>(Master.PATH_TO_MENUS + "pause_menu" + ".tscn");
            masterNode.AddChild(packed.Instance());
        }

        protected override void ExitState()
        {
            masterNode.GameManager.GetTree().Paused = false;
        }
    }
}
