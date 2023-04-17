using System;

namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class Sleeping : EnemyState
    {
        public Sleeping(Character character) : base(character)
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
