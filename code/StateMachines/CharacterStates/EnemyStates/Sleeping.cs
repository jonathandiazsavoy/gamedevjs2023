using System;

namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class Sleeping : EnemyState
    {
        public Sleeping(Enemy enemy) : base(enemy)
        {
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
