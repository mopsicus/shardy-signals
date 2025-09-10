using UnityEngine;

/// <summary>
/// Self-destroying timer
/// </summary>
public class DestroyTimer : MonoBehaviour {

    /// <summary>
    /// Destroy method name
    /// </summary>
    const string DESTROY_METHOD = "Destroy";

    /// <summary>
    /// Timer value
    /// </summary>
    [SerializeField]
    float _timer = 5f;

    /// <summary>
    /// Init timer
    /// </summary>
    void Awake() {
        Invoke(DESTROY_METHOD, _timer);
    }

    /// <summary>
    /// Destroy the game object
    /// </summary>
    void Destroy() {
        Debug.LogWarning($"destroying {gameObject.name}");
        Destroy(gameObject);
    }
}

