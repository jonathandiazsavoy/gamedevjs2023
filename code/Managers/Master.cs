using code.Helpers;
using code.StateMachines.GameStates;
using code.StateMachines.GameStates.GamePlay;
using Godot;

// TODO 9 need to reorganize tree hierarchy entirely - the game game manager should be moved up to this master node later on
// need to rethink tree structure is accordance what shoudl be paused when "Game" is paused
public class Master : Node
{
    public const string NODE_PATH_TO_MASTER = "/root/Master";
    public const string NODE_PATH_TO_GAME_MANAGER = "/root/Master/GameManager";
    public const string NODE_PATH_TO_SHOP_SCREEN = "/root/Master/ShopScreen";
    public const string NODE_PATH_TO_PAUSE_MENU = "/root/Master/PauseMenu";
    public const string NODE_PATH_TO_GAME_OVER_MENU = "/root/Master/GameOverMenu";
    public const string NODE_PATH_TO_CAMERA = "/root/Master/Camera2D";
    public const string NODE_PATH_TO_PLAYER = "/root/Master/GameManager/Level/Objects/Player";
    public const string PATH_TO_SCREENS = "res://scenes/screens/";
    public const string PATH_TO_MENUS = "res://scenes/menus/";
    public const string PATH_TO_SOUNDS = "res://assets/audio/sounds/events/";

    public AudioStreamPlayer AudioStreamPlayer { get; private set; }
    public SoundPlayer SoundPlayer;

    public GameManager GameManager;
    public CameraManager Camera2D;
    private GameState currentState;

    public override void _Ready()
    {
        GameManager = this.GetNode<GameManager>(NODE_PATH_TO_GAME_MANAGER);
        AudioStreamPlayer = this.GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        Camera2D = this.GetNode<CameraManager>(NODE_PATH_TO_CAMERA);
        SoundPlayer = new SoundPlayer(AudioStreamPlayer, PATH_TO_SOUNDS);
        currentState = new Running(this);
    }

    public override void _Process(float delta)
    {
        currentState = (GameState)currentState.HandleInput(delta);

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            GameManager.AdjustAlarmCountdown(5);
        }
        if (Input.IsActionJustPressed("test2"))
        {
            GameManager.AdjustAlarmCountdown(-5);
        }
        if (Input.IsActionJustPressed("test3"))
        {
            GameManager.AdjustMoney(100);
        }
        if (Input.IsActionJustPressed("test4"))
        {
            this.GetNode<CameraManager>(NODE_PATH_TO_CAMERA).ZoomOutOnMap();
        }
    }
    public override void _PhysicsProcess(float delta)
    {
        currentState = (GameState)currentState.Update(delta);
    }

    public void LoadScreen(string screenName)
    {
        PackedScene packedshop = GD.Load<PackedScene>(Master.PATH_TO_SCREENS + screenName + ".tscn");
        this.AddChild(packedshop.Instance());
    }
    public void LoadMenu(string menuName)
    {
        PackedScene packedshop = GD.Load<PackedScene>(Master.PATH_TO_MENUS + menuName + ".tscn");
        this.AddChild(packedshop.Instance());
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnShopEntered(Player player)
    {
        SoundPlayer.Play("enter_shop");
        GameManager.Player = player;
        // if game is not paused, then go to shop
        if (currentState is Running) currentState = (GameState)currentState.SwitchState(new OnShopScreen(this, currentState));
    }
    public void OnExitShop(Player player)
    {
        SoundPlayer.Play("exit_teleport");
        // Close shop for current wave after exiting
        GameManager.WaveManager.CloseShop();
        GameManager.Player = player;
        // if game is not paused, then go to shop
        if (currentState is OnShopScreen) currentState = (GameState)currentState.SwitchState(new Running(this));
        // TODO 2 is bugged since exit uses same button as pasue - need a cooldown to prevent pausing
    }
    public void OnGoToWaveCompletedScreen()
    {
        currentState = (GameState)currentState.SwitchState(new WaveCompleted(this));
    }
    public void OnGoToGameCompletedScreen()
    {
        currentState = (GameState)currentState.SwitchState(new GameCompleted(this));
    }
    public void OnGoToGameOverScreen()
    {
        currentState = (GameState)currentState.SwitchState(new GameOver(this));
    }
    public void OnGoToTitleScreen()
    {
        currentState = (GameState)currentState.SwitchState(new OnTitleScreen(this));
    }

    // On restart go the starting new wave state and reset the player object stuff - restore hp and reset coords
    // TODO 9 find a better way to get signals sent through state machines
}
