namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    public class StartFlying : FlyingEnemyState
    {
        public StartFlying(FlyingEnemy flyingEnemy) : base(flyingEnemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (!flyingEnemy.AnimationPlayer.IsPlaying()) return SwitchState(new ChasingTarget(flyingEnemy));
            return this;
        }

        protected override void EnterState()
        {
            flyingEnemy.AnimationPlayer.Play("start_flying");
        }
    }
}
