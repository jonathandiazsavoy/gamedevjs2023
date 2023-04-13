public abstract class CharacterState : BaseFSMState
{
    protected Character character;

    public CharacterState(Character character)
    {
        this.character = character;
    }
}
