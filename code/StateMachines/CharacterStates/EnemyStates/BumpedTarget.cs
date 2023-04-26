namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class BumpedTarget : EnemyState
    {
        private float timeWaited;

        public BumpedTarget(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            timeWaited += delta;
            if(timeWaited > enemy.WaitAfterBumpingPlayer) return this.SwitchState(new ChasingTarget(enemy));
            return this;
        }

        protected override void EnterState()
        {
            timeWaited = 0;
        }
    }
}
