using Shardy.Signals;
using UnityEngine;

/// <summary>
/// Destroyable demo component
/// Show correct signals handling for destroyed objects
/// </summary>
public class Destroyable : MonoBehaviour, IDemo {

    /// <summary>
    /// Log tag
    /// </summary>
    public const string TAG = "DESTROYABLE";

    /// <summary>
    /// Init and subscribe to signals
    /// </summary>
    void Awake() {
        Signals.Subscribe(this);
    }

    /// <summary>
    /// Called when a signal is received
    /// </summary>
    public void OnSignalAction(string data) {
        Debug.Log($"[{TAG}] {gameObject.name} action: {data}");
    }

    /// <summary>
    /// Called when a signal is received
    /// </summary>
    public void OnSignalAction2(int value) {
        Debug.Log($"[{TAG}] {gameObject.name} action2: {value}");
    }
}
