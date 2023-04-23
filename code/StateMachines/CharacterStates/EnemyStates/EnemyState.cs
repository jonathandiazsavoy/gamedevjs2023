using Godot;

namespace code.StateMachines.CharacterStates.EnemyStates
{
    public abstract class EnemyState : BaseFSMState
    {
        protected Enemy enemy;

        protected EnemyState(Enemy enemy)
        {
            this.enemy = enemy;
        }
    }
}
