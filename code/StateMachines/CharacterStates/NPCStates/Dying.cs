public class Dying : CharacterState
{
    public Dying(Character character) : base(character)
    {
    }

    public override BaseFSMState Update(float delta)
    {
        return this;
    }

    protected override void EnterState()
    {
        character.AnimationPlayer.Play("DEATH");
    }
}
