using Godot;

public class GameManager : Node2D
{
    const float WAVE_COUNTDOWN_MAX_START = 60;

    public Timer WaveCountdown { get; private set; }

    public float TotalRunTime { get; private set; }
    public float WaveClockTotal
    {
        get 
        {
            return Mathf.Abs(WaveCountdown.TimeLeft - WAVE_COUNTDOWN_MAX_START);
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        WaveCountdown = this.GetNode<Timer>("WaveCountdown");
        
        StartNewRun();
        StartNewLoop();
    }
    public override void _Process(float delta)
    {
        TotalRunTime += delta;

        //TODO remove this test stuff
        if (Input.IsActionJustPressed("test1"))
        {
            IncreaseWaveCountdown();
        }
        if (Input.IsActionJustPressed("test2"))
        {
            DecreaseWaveCountdown();
        }
    }

    public void IncreaseWaveCountdown(float increaseAmount = 1)
    {
        WaveCountdown.Start(Mathf.Clamp(WaveCountdown.TimeLeft + increaseAmount, 0, WAVE_COUNTDOWN_MAX_START));
    }
    public void DecreaseWaveCountdown(float decreaseAmount = 1)
    {
        WaveCountdown.Start(Mathf.Clamp(WaveCountdown.TimeLeft - decreaseAmount, 0, WAVE_COUNTDOWN_MAX_START));
    }

    private void StartNewRun()
    {
        TotalRunTime = 0;
    }
    private void StartNewLoop()
    {
        WaveCountdown.Start(WAVE_COUNTDOWN_MAX_START);
    }
}
