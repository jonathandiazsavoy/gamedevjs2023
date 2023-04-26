using Godot;

namespace code.Helpers
{
    /// <summary>
    /// Hack to lock Sprite node global rotation
    /// </summary>
    public class LockSpriteRotation : Sprite
    {
        [Export]
        public bool Unlock = false;

        public override void _Process(float delta)
        {
            if (Unlock) return;
            this.GlobalRotationDegrees= 0;
        }
    }
}
