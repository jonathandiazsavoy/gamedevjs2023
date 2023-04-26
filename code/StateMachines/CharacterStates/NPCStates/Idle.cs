namespace code.StateMachines.CharacterStates.NPCStates
{
    public class Idle : NPCState
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
