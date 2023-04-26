using System;

namespace code.StateMachines.GameStates
{
    public class OnTitleScreen : GameState
    {
        public OnTitleScreen(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            throw new NotImplementedException();
        }

        public override BaseFSMState Update(float delta)
        {
            throw new NotImplementedException();
        }

        protected override void EnterState()
        {
            // nothing
        }
        protected override void ExitState()
        {
            // unpause game and remove screen
        }
    }
}
