using Shardy.Signals;
using UnityEngine;

public class Demo : MonoBehaviour, IDemo {

    /// <summary>
    /// Log tag
    /// </summary>
    public const string TAG = "DEMO";

    /// <summary>
    /// Destroy method name
    /// </summary>
    const string SEND_METHOD = "Send";

    /// <summary>
    /// Init and subscribe to signals
    /// </summary>
    void Awake() {
        Signals.Subscribe(this);
        InvokeRepeating(SEND_METHOD, 1f, 1f);
    }

    /// <summary>
    /// Unsubscribe from signals
    /// </summary>
    void OnDestroy() {
        Signals.Unsubscribe(this);
    }

    /// <summary>
    /// Sends demo signals
    /// Send string data
    /// Send int data -> current subscriber count
    /// </summary>
    void Send() {
        Signals.Send<IDemo>(a => a.OnSignalAction("subscribers"));
        Signals.Send<IDemo>(a => a.OnSignalAction2(Signals.GetSubscriberCount<IDemo>()));
    }

    /// <summary>
    /// Called when a signal is received
    /// </summary>
    public void OnSignalAction(string data) {
        Debug.Log($"[{TAG}] action: {data}");
    }

    /// <summary>
    /// Called when a signal is received
    /// </summary>
    public void OnSignalAction2(int value) {
        Debug.Log($"[{TAG}] action2: {value}");
    }
}
