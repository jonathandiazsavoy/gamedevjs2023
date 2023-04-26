using Godot;

public class AppendableRichTextLabel : RichTextLabel
{
    public string InitialTextAtReady { get; private set; }

    public override void _Ready()
    {
        InitialTextAtReady = this.Text;
    }

    public void AppendToInitialText(string text)
    {
        this.Text = InitialTextAtReady+ text;
    }
}
