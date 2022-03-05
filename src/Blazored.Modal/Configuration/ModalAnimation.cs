namespace Blazored.Modal;

public class ModalAnimation
{
    public ModalAnimationType Type { get; set; }
    public double? Duration { get; set; }

    public ModalAnimation(ModalAnimationType type, double duration)
    {
        Type = type;
        Duration = duration;
    }

    public static ModalAnimation FadeIn(double duration) => new(ModalAnimationType.FadeIn, duration);
    public static ModalAnimation FadeOut(double duration) => new(ModalAnimationType.FadeOut, duration);
    public static ModalAnimation FadeInOut(double duration) => new(ModalAnimationType.FadeInOut, duration);
}

public enum ModalAnimationType
{
    None = 0, // Set to 0 so it is definitely the default value
    FadeIn,
    FadeOut,
    FadeInOut
}