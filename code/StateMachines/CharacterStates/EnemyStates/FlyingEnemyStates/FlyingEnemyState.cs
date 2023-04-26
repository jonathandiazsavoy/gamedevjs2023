namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    public abstract class FlyingEnemyState : EnemyState
    {
        protected FlyingEnemy flyingEnemy;

        protected FlyingEnemyState(FlyingEnemy flyingEnemy) : base(flyingEnemy)
        {
            this.flyingEnemy = flyingEnemy;
        }
    }
}
