using Godot;

public class AnalogueClock : Node2D
{
    const float DEGREES_IN_CIRCLE = 360;
    const float SECONDS_IN_MINUTE = 60;
    const float MINUTES_IN_HOUR = 60;
    const float HOURS_IN_CLOCK = 12;

    // Node members
    private Position2D secondHand;
    private Position2D minuteHand;
    private Position2D hourHand;

    public float TimeUnit;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        secondHand = this.GetNode<Position2D>("ClockFace/HandAnchor/SecondHand");
        minuteHand = this.GetNode<Position2D>("ClockFace//HandAnchor/MinuteHand");
        hourHand = this.GetNode<Position2D>("ClockFace//HandAnchor/HourHand");
    }

    public override void _Process(float delta)
    {
        secondHand.RotationDegrees = TimeUnit * (DEGREES_IN_CIRCLE / SECONDS_IN_MINUTE);
        minuteHand.RotationDegrees = TimeUnit * ((DEGREES_IN_CIRCLE / SECONDS_IN_MINUTE) / MINUTES_IN_HOUR);
        hourHand.RotationDegrees = TimeUnit * (((DEGREES_IN_CIRCLE / SECONDS_IN_MINUTE) / MINUTES_IN_HOUR) / HOURS_IN_CLOCK);
    }
}
