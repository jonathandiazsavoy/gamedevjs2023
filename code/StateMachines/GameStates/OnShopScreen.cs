using Godot;

namespace code.StateMachines.GameStates
{
    public class OnShopScreen : GameState
    {
        private readonly BaseFSMState previousState;

        public OnShopScreen(Master masterNode, BaseFSMState previousState) : base(masterNode)
        {
            this.previousState = previousState;
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
            // move shop instance stuff here
            masterNode.GameManager.GetTree().Paused = true;
            PackedScene packedshop = GD.Load<PackedScene>(Master.PATH_TO_SCREENS + "shop" + ".tscn");
            masterNode.AddChild(packedshop.Instance());
        }
        protected override void ExitState()
        {
            GD.Print("exiting on shop screen state");
            masterNode.GetNode<CanvasLayer>(Master.NODE_PATH_TO_SHOP_SCREEN).Free();
            GD.Print("shop node killed");
            masterNode.GameManager.GetTree().Paused = false;
            // unpause game and remove screen
            // retyurn to previous state
        }
    }
}
