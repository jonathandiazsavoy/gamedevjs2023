using System;

namespace code.StateMachines.GameStates.GamePlay
{
    public class InCutscene : GamePlay
    {
        public InCutscene(Master masterNode) : base(masterNode)
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
            throw new NotImplementedException();
        }
    }
}
