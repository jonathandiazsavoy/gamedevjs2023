namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class ChasingTarget : EnemyState
    {
        public ChasingTarget(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            enemy.NavigationAgent.SetTargetLocation(enemy.currentTarget.GlobalPosition);

            enemy.currentMoveDirection = (enemy.NavigationAgent.GetNextLocation() - enemy.GlobalPosition).Normalized();

            enemy.MoveAndCollide(enemy.currentMoveDirection * enemy.CurrentStats.Speed);
            enemy.SetCharacterOrientation(enemy.currentMoveDirection);
            return this;
        }

        protected override void EnterState()
        {
            enemy.NavigationAgent.SetTargetLocation(enemy.currentTarget.GlobalPosition); // TODO fix game crashing here once player dies
            enemy.Hitbox.Monitoring = true;
            // TODO play moving animation
        }
    }
}
