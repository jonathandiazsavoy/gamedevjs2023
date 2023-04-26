namespace code.StateMachines.CharacterStates.EnemyStates
{
    public class Dying : EnemyState
    {
        public Dying(Enemy enemy) : base(enemy)
        {
        }

        public override BaseFSMState Update(float delta)
        {
            enemy.MoveAndSlide(enemy.incomingAttackForce);
            return this;
        }

        protected override void EnterState()
        {
            enemy.AnimationPlayer.Play("death");
            enemy.SoundPlayer.Play("enemy_death");
        }
    }
}
