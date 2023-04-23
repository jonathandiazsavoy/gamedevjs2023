using Godot;
using System;

public class GameplayUIManager : Control
{
    const string NODE_PATH_TO_PLAYER = "/root/Master/GameManager/Level/Objects/Player";

    private GameManager gameManager;
    private Player player;
    protected RichTextLabel TotalRunTime { get; set; }
    protected RichTextLabel LoopTimeLeft { get; set; }
    protected RichTextLabel EnemiesLeft { get; set; }
    protected RichTextLabel Score { get; set; }
    protected RichTextLabel Money { get; set; }
    protected AnalogueClock WaveClock { get; set; }
    protected AppendableRichTextLabel Hp { get; set; }
    protected AppendableRichTextLabel Mp { get; set; }

    private string initialTotalRunTimeText;
    private string initialLoopTimeLeftText;
    private string initialEnemiesLeftText;
    private string initialScoreLeftText;
    private string initialMoneyLeftText;

    public override void _Ready()
    {
        gameManager = this.GetNode<GameManager>(Master.NODE_PATH_TO_GAME_MANAGER);
        player = this.GetNode<Player>(NODE_PATH_TO_PLAYER);

        EnemiesLeft = this.GetNode<RichTextLabel>("EnemiesLeft");
        Score = this.GetNode<RichTextLabel>("Score");
        Money = this.GetNode<RichTextLabel>("Money");
        WaveClock = this.GetNode<AnalogueClock>("AnalogueClock");
        initialEnemiesLeftText= EnemiesLeft.Text;
        initialScoreLeftText= Score.Text;
        initialMoneyLeftText= Money.Text;

        // TODO temp stuff
        TotalRunTime = this.GetNode<RichTextLabel>("TotalRunTime");
        LoopTimeLeft = this.GetNode<RichTextLabel>("LoopTimeLeft");
        initialTotalRunTimeText = TotalRunTime.Text;
        initialLoopTimeLeftText = LoopTimeLeft.Text;
        Hp = this.GetNode<AppendableRichTextLabel>("Hp");
        Mp = this.GetNode<AppendableRichTextLabel>("Mp");
    }
    public override void _Process(float delta)
    {
        EnemiesLeft.Text = initialEnemiesLeftText + gameManager.EnemyCount;
        Score.Text= initialScoreLeftText + gameManager.Score;
        Money.Text = initialMoneyLeftText + gameManager.Money;

        WaveClock.TimeUnit = gameManager.CenterClockTotal * AnalogueClock.SECONDS_IN_MINUTE;

        // TODO temp stuff
        TotalRunTime.Text = initialTotalRunTimeText + Math.Floor(gameManager.TotalRunTime).ToString();
        LoopTimeLeft.Text = initialLoopTimeLeftText + Math.Ceiling(gameManager.AlarmCountdown.TimeLeft).ToString();
        Hp.AppendToInitialText(player.CurrentStats.Hp + " / " + player.CurrentStats.MaxHp);
        Mp.AppendToInitialText(player.CurrentStats.Mp + " / " + player.CurrentStats.MaxMp);
    }
}
