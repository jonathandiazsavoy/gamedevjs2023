using code.Items.TimeItems;
using Godot;

public class GameManager : Node2D
{
    const float ALARM_COUNTDOWN_MAX_START = 60;

    public Timer AlarmCountdown { get; private set; }
    public WaveManager WaveManager { get; private set; }

    public float TotalRunTime { get; private set; }
    public float CenterClockTotal
    {
        get 
        {
            return Mathf.Abs(AlarmCountdown.TimeLeft - ALARM_COUNTDOWN_MAX_START);
        }
    }
    public int EnemyCount { get; private set; }

    public override void _Ready()
    {
        AlarmCountdown = this.GetNode<Timer>("AlarmCountdown");
        WaveManager = this.GetNode<WaveManager>("Level/Objects/WaveManager");
        
        StartNewRun();
        StartNewLoop();
        EnemyCount = WaveManager.EnemyCount;
    }
    public override void _Process(float delta)
    {
        TotalRunTime += delta;

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            
        }
        if (Input.IsActionJustPressed("test2"))
        {
            
        }
    }

    private void StartNewRun()
    {
        TotalRunTime = 0;
    }
    private void StartNewLoop()
    {
        AlarmCountdown.Start(ALARM_COUNTDOWN_MAX_START);
    }

    private void AdjustAlarmCountdown(float adjustAmount)
    {
        AlarmCountdown.Start(Mathf.Clamp(AlarmCountdown.TimeLeft + adjustAmount, 0, ALARM_COUNTDOWN_MAX_START));
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
    }
}
