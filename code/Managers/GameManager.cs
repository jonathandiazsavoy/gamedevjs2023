using code.Helpers;
using code.Items.TimeItems;
using Godot;

public class GameManager : Node2D
{
    const float ALARM_COUNTDOWN_MAX_START = 60;

    public const string PATH_TO_SOUNDS = "res://assets/audio/music/";

    public Timer AlarmCountdown { get; private set; }
    public WaveManager WaveManager { get; private set; }
    public AudioStreamPlayer AudioStreamPlayer { get; private set; }
    public Node2D Level { get; private set; }
    
    public MusicPlayer MusicPlayer;

    public float TotalRunTime { get; private set; }
    public float CenterClockTotal
    {
        get 
        {
            return Mathf.Abs(AlarmCountdown.TimeLeft - ALARM_COUNTDOWN_MAX_START);
        }
    }
    public int EnemyCount { get; private set; }

    private float initialMusicPitchScale;
    public bool AlarmTriggered;

    public override void _Ready()
    {
        AlarmCountdown = this.GetNode<Timer>("AlarmCountdown");
        WaveManager = this.GetNode<WaveManager>("Level/Objects/WaveManager");
        AudioStreamPlayer = this.GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        Level = this.GetNode<Node2D>("Level");
        MusicPlayer = new MusicPlayer(AudioStreamPlayer, PATH_TO_SOUNDS);


        StartNewRun();
        StartNewLoop();
        EnemyCount = WaveManager.EnemyCount;
        initialMusicPitchScale = AudioStreamPlayer.PitchScale;
    }
    public override void _Process(float delta)
    {
        TotalRunTime += delta;

        // increease speed as timer ticks
        if (!AlarmTriggered) 
        { 
            if (Mathf.Clamp(CenterClockTotal - 45, 0, 20) != 0)
            {

                AudioStreamPlayer.PitchScale = initialMusicPitchScale + (Mathf.Clamp(CenterClockTotal - 45, 0, 20) / 25) * (Mathf.Clamp(CenterClockTotal - 45, 0, 20) / 25);
            }
            else
            {
                AudioStreamPlayer.PitchScale = initialMusicPitchScale;
            }
        }

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            //AdjustAlarmCountdown(5);
            Level.GetTree().Paused= !Level.GetTree().Paused;
        }
        if (Input.IsActionJustPressed("test2"))
        {
            AdjustAlarmCountdown(-5);
            GetTree().Paused = false;
        }
    }

    private void StartNewRun()
    {
        TotalRunTime = 0;
    }
    private void StartNewLoop()
    {
        AlarmTriggered = false;
        AlarmCountdown.Start(ALARM_COUNTDOWN_MAX_START);
    }

    private void AdjustAlarmCountdown(float adjustAmount)
    {
        // this is bugged if you get a negative adjustment when the timer is near 0 - it goes back up to 3
        if (!AlarmTriggered) AlarmCountdown.Start(Mathf.Clamp(AlarmCountdown.TimeLeft + adjustAmount, 0, ALARM_COUNTDOWN_MAX_START));
    }

    // **************************************************
    // Signal listeners
    // **************************************************
    public void OnCountDownModifierItemUsed(CountDownModifier countdownModifier)
    {
        AdjustAlarmCountdown(countdownModifier.CountDownChange);
    }
    public void OnEnemyDied(Enemy enemy)
    {
        this.EnemyCount--;
    }
    public void OnAlarmCountdownTimeout()
    {
        AlarmTriggered = true;
        MusicPlayer.Play("alert_phase");
    }
}
