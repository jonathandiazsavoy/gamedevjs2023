namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class Sleeping : EnemyState
    {
        public Sleeping(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            // TODO play sleeping animation
        }
    }
}
