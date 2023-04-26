namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class TakingDamage : EnemyState
    {
        public TakingDamage(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (!enemy.AnimationPlayer.IsPlaying()) return this.SwitchState(new ChasingTarget(enemy));
            enemy.MoveAndSlide(enemy.incomingAttackForce);
            return this;
        }

        protected override void EnterState()
        {
            enemy.AnimationPlayer.Play("take_damage");
            enemy.SoundPlayer.Play("take_damage");
        }
        protected override void ExitState()
        {
            enemy.AnimationPlayer.Play("RESET");
        }
    }
}
