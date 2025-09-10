using Shardy.Signals;

/// <summary>
/// Demo interface
/// </summary>
public interface IDemo : ISubscriber {
    void OnSignalAction(string data);
    void OnSignalAction2(int value);
}
