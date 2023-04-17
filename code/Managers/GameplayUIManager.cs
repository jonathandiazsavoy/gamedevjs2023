using Godot;
using System;

public class GameplayUIManager : Control
{
    private GameManager gameManager;
    protected RichTextLabel TotalRunTime { get; set; }
    protected RichTextLabel LoopTimeLeft { get; set; }
    protected RichTextLabel EnemiesLeft { get; set; }
    protected AnalogueClock WaveClock { get; set; }

    private string initialTotalRunTimeText;
    private string initialLoopTimeLeftText;
    private string initialEnemiesLeftText;

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>("/root/GameManager");

        TotalRunTime = this.GetNode<RichTextLabel>("TotalRunTime");
        LoopTimeLeft = this.GetNode<RichTextLabel>("LoopTimeLeft");
        EnemiesLeft = this.GetNode<RichTextLabel>("EnemiesLeft");
        WaveClock = this.GetNode<AnalogueClock>("AnalogueClock");

        initialTotalRunTimeText = TotalRunTime.Text;
        initialLoopTimeLeftText= LoopTimeLeft.Text;
        initialEnemiesLeftText= EnemiesLeft.Text;
    }
    public override void _Process(float delta)
    {
        TotalRunTime.Text = initialTotalRunTimeText + Math.Floor(gameManager.TotalRunTime).ToString();
        LoopTimeLeft.Text = initialLoopTimeLeftText + Math.Ceiling(gameManager.WaveCountdown.TimeLeft).ToString();
        EnemiesLeft.Text = initialEnemiesLeftText + gameManager.EnemyCount;

        WaveClock.TimeUnit = gameManager.WaveClockTotal * 60;
    }
}
