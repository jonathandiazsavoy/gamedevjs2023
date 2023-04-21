using code.Helpers;
using Godot;

// TODO need to reorganize tree hierarchy entirely - the game game manager should be moved up to this master node later on
// need to rethink tree structure is accordance what shoudl be paused when "Game" is paused
public class Master : Node
{
    public const string NODE_PATH_TO_GAME_MANAGER = "/root/Master/GameManager";
    public const string PATH_TO_SOUNDS = "res://assets/audio/sounds/";

    public AudioStreamPlayer2D AudioStreamPlayer { get; private set; }
    public SoundPlayer SoundPlayer;

    private GameManager gameManager;

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(NODE_PATH_TO_GAME_MANAGER);
        AudioStreamPlayer = this.GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
        SoundPlayer = new SoundPlayer(AudioStreamPlayer, PATH_TO_SOUNDS);
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("pause"))
        {
            // TODO fix audio steup
            if (!gameManager.GetTree().Paused) SoundPlayer.Play();
            gameManager.GetTree().Paused= !gameManager.GetTree().Paused;
        }

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            gameManager.AdjustAlarmCountdown(5);
        }
        if (Input.IsActionJustPressed("test2"))
        {
            gameManager.AdjustAlarmCountdown(-5);
        }
        if (Input.IsActionJustPressed("test2"))
        {
            gameManager.AdjustMoney(100);
        }
    }
}
