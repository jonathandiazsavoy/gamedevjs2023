namespace code.StateMachines.GameStates
{
    public abstract class GameState : BaseFSMState
    {
        protected Master masterNode;

        public GameState(Master masterNode)
        {
            this.masterNode = masterNode;
        }

        /// <summary>
        ///     Processes player input and updates current state.
        /// </summary>
        /// <param name="delta">Time since previous game loop in seconds.</param>
        /// <returns>State after input is processed.</returns>
        public abstract BaseFSMState HandleInput(float delta);
        /// <summary>
        ///     Processes player input and updates the players state.
        /// </summary>
        /// <param name="delta">Time since previous game loop in seconds.</param>
        /// <returns>State after the update is performed.</returns>
        public BaseFSMState HandleInputAndUpdate(float delta)
        {
            return this.HandleInput(delta).Update(delta);
        }
    }
}
