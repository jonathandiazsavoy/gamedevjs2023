using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.StateMachines.GameStates.GamePlay
{
    public class GameWon : GamePlay
    {
        public GameWon(Master masterNode) : base(masterNode)
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
