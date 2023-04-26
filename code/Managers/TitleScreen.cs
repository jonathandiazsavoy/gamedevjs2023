using Godot;

public class TitleScreen : CanvasLayer
{
    private float timeWaited;

    public override void _Process(float delta)
    {
        timeWaited += delta;
        if (Input.IsActionJustPressed("ui_accept"))
        {
            if (timeWaited > 1)
            {
                this.GetNode<MarginContainer>("Menu/Help").Visible = false;
                this.GetNode<MarginContainer>("Menu/Credits").Visible = false;
            }
        }
    }

    public void OnStart()
    {
        this.GetTree().ChangeScene("res://scenes/levels/DebugLevel.tscn");
    }
    public void OnHelp()
    {
        this.GetNode<MarginContainer>("Menu/Help").Visible= true;
        this.GetNode<MarginContainer>("Menu/Credits").Visible = false;
        timeWaited = 0;
}
    public void OnCredits()
    {
        this.GetNode<MarginContainer>("Menu/Help").Visible = false;
        this.GetNode<MarginContainer>("Menu/Credits").Visible = true;
        timeWaited = 0;
    }
}
