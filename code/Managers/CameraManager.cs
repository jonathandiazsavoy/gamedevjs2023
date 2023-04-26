using Godot;

public class CameraManager : Camera2D
{
    private Node2D cameraTarget;

    private GameManager gameManager;
    private Player player;

    public override void _Ready()
    {
        FollowPlayer();
    }
    public override void _Process(float delta)
    {
        if (cameraTarget == null) return;
        this.GlobalPosition = cameraTarget.GlobalPosition;
    }

    public void ZoomOutOnMap()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        this.Zoom = new Vector2(2, 2);
        cameraTarget = gameManager.MapCenter;
    }
    public void FollowPlayer() 
    {
        player = this.GetNode<Player>(Master.NODE_PATH_TO_PLAYER);
        cameraTarget = player;
        this.GetNode<AnimationPlayer>("AnimationPlayer").Play("zoom_in");
    }
}
