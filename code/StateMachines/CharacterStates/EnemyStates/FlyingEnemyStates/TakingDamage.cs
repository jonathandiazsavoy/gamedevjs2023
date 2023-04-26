namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    internal class TakingDamage : FlyingEnemyState
    {
        public TakingDamage(FlyingEnemy flyingEnemy) : base(flyingEnemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (!flyingEnemy.AnimationPlayer.IsPlaying()) return this.SwitchState(new ChasingTarget(flyingEnemy));
            flyingEnemy.MoveAndSlide(enemy.incomingAttackForce);
            return this;
        }

        protected override void EnterState()
        {
            flyingEnemy.AnimationPlayer.Play("take_damage");
            flyingEnemy.SoundPlayer.Play("take_damage");
        }
        protected override void ExitState()
        {
            flyingEnemy.AnimationPlayer.Play("RESET");
        }
    }
}
