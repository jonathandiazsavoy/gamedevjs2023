using Godot;

public class NextWavePortal : Area2D
{
    private GameManager gameManager;

    [Signal]
    public delegate void GoToNextWave(Player player);

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        this.Connect("GoToNextWave", gameManager, "OnGoToNextWave");
    }

    public void OnPortalEntered(Node body)
    {
        if (body is Player player) EmitSignal(nameof(GoToNextWave), player);
    }
}
