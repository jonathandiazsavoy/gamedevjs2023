namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    public class BumpedTarget : FlyingEnemyState
    {
        private float timeWaited;

        public BumpedTarget(FlyingEnemy flyingEnemy) : base(flyingEnemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            timeWaited += delta;
            if (timeWaited > flyingEnemy.WaitAfterBumpingPlayer) return this.SwitchState(new ChasingTarget(flyingEnemy));
            return this;
        }

        protected override void EnterState()
        {
            timeWaited = 0;
        }
    }
}
