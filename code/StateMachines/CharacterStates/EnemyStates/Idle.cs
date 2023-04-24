namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class Idle : EnemyState
    {
        public Idle(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (enemy.Alerted) return this.SwitchState(new ChasingTarget(enemy));
            return this;
        }

        protected override void EnterState()
        {
            // TODO play idle animation
        }
    }
}
