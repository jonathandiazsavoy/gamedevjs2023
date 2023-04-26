namespace code.StateMachines.CharacterStates.EnemyStates.FlyingEnemyStates
{
    public class Dying : FlyingEnemyState
    {
        public Dying(FlyingEnemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            flyingEnemy.MoveAndSlide(enemy.incomingAttackForce);
            return this;
        }

        protected override void EnterState()
        {
            flyingEnemy.AnimationPlayer.Play("death");
            flyingEnemy.SoundPlayer.Play("enemy_death");
        }
    }
}
