namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class Idle : EnemyState
    {
        public Idle(Character character) : base(character)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            //
        }
    }
}
