using Godot;

public class CameraManager : Camera2D
{
    private Node2D cameraTarget;

    private GameManager gameManager;
    private Player player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }
    public override void _Process(float delta)
    {
        if (cameraTarget == null) return;
        this.GlobalPosition = cameraTarget.GlobalPosition;
    }

    public void PlayZoomOutOnMapAnimation()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        this.Zoom = new Vector2(2, 2);
        cameraTarget = gameManager;
    }
    public void FollowPlayer() 
    {
        player = this.GetNode<Player>(Master.NODE_PATH_TO_PLAYER);
        this.Zoom = new Vector2(.5f, .5f);
        cameraTarget = player;
    }
}
