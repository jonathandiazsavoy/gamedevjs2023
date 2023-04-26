using Godot;

public class PauseMenu : CanvasLayer
{
    [Signal]
    public delegate void GoToTitleScreen();

    public bool resume = false;

    public override void _Ready()
    {
        resume = false;
    }

    public void OnResume()
    {
        resume= true;
    }
    public void OnRestart()
    {
        this.GetTree().ReloadCurrentScene();
        this.GetTree().Paused = false;
    }
    public void OnBackToTitle()
    {
        EmitSignal("GoToTitleScreen");
    }
}
