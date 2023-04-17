namespace code.StateMachines.CharacterStates.NPCStates
{
    public abstract class NPCState : BaseFSMState
    {
        protected Character character;

        public NPCState(Character character)
        {
            this.character = character;
        }
    }
}
