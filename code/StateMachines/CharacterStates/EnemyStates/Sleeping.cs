namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class Sleeping : EnemyState
    {
        public Sleeping(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            if (enemy.Alerted) return this.SwitchState(new ChasingTarget(enemy));
            return this;
        }

        protected override void EnterState()
        {
            enemy.AnimationPlayer.Play("sleeping");
        }
    }
}
