namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class TakingDamage : EnemyState
    {
        public TakingDamage(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (!character.AnimationPlayer.IsPlaying()) return this.SwitchState(new Idle(enemy));
            return this;
        }

        protected override void EnterState()
        {
            enemy.SoundPlayer.Play("take damage");
        }
    }
}
