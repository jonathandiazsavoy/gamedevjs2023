namespace code.StateMachines.GameStates.GamePlay
{
    public class GameCompleted : GamePlay
    {
        public GameCompleted(Master masterNode) : base(masterNode)
        {
        }

        public override BaseFSMState HandleInput(float delta)
        {
            return this;
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
