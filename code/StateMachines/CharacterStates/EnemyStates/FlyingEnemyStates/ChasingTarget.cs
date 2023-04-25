namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    public class ChasingTarget : FlyingEnemyState
    {
        public ChasingTarget(FlyingEnemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            flyingEnemy.NavigationAgent.SetTargetLocation(flyingEnemy.currentTarget.GlobalPosition);

            flyingEnemy.currentMoveDirection = (flyingEnemy.NavigationAgent.GetNextLocation() - flyingEnemy.GlobalPosition).Normalized();

            flyingEnemy.MoveAndCollide(flyingEnemy.currentMoveDirection * flyingEnemy.CurrentStats.Speed);
            flyingEnemy.SetCharacterOrientation(flyingEnemy.currentMoveDirection);
            return this;
        }

        protected override void EnterState()
        {
            flyingEnemy.NavigationAgent.SetTargetLocation(enemy.currentTarget.GlobalPosition); // TODO fix game crashing here once player dies
            flyingEnemy.Hitbox.Monitoring = true;
            flyingEnemy.AnimationPlayer.Play("moving");
        }
    }
}
