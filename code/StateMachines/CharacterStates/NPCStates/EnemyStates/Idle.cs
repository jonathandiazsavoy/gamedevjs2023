namespace code.StateMachines.CharacterStates.NPCStates.EnemyStates
{
    public class Idle : EnemyState
    {
        public Idle(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            return this;
        }

        protected override void EnterState()
        {
            //
        }
    }
}
