using Client.Models;
using Pulse.Blazor;

namespace Client.Services;

internal sealed class PlayerState : StateContainer
{
    public Episode? Current { get; private set => Set(ref field, value); }

    public double CurrentTime { get; set => Set(ref field, value); }
    public double CurrentDuration { get; set => Set(ref field, value); }
    public bool IsPlaying { get; set => Set(ref field, value); }

    public string? Source => Current switch
    {
        null => null,
        _ when !string.IsNullOrEmpty(Current.EnclosureUrl) => Current.EnclosureUrl,
        _ => Current.Link
    };

    public string? Cover =>
        Current?.Image ?? Current?.FeedImage;

    public double EffectiveDuration =>
        CurrentDuration > 0
            ? CurrentDuration
            : (Current?.Duration ?? 0);

    public double Progress =>
        EffectiveDuration <= 0
            ? 0
            : (CurrentTime / EffectiveDuration) * 100.0;

    public void SetCurrent(Episode episode)
    {
        Current = episode;
        IsPlaying = true;
    }

    public void Clear()
    {
        Current = null;
        IsPlaying = false;

        CurrentTime = 0;
        CurrentDuration = 0;
    }
}
