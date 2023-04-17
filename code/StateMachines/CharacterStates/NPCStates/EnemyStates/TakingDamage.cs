namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class TakingDamage : EnemyState
    {
        public TakingDamage(Character character) : base(character)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (!character.AnimationPlayer.IsPlaying()) return this.SwitchState(new Idle(character));
            return this;
        }

        protected override void EnterState()
        {
            //
        }
    }
}
