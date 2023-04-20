using Godot;

public class NextWavePortal : Area2D
{
    [Signal]
    public delegate void GoToNextWave(Player player);

    public void OnPortalEntered(Node body)
    {
        if (body is Player player) EmitSignal(nameof(GoToNextWave), player);
    }
}
