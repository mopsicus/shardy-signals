<a href="./README.md">![Static Badge](https://img.shields.io/badge/english-118027)</a>
<a href="./README.ru.md">![Static Badge](https://img.shields.io/badge/russian-0390fc)</a>
<p align="center">
    <picture>
        <source media="(prefers-color-scheme: dark)" srcset="Media/logo-signals-dark.png">
        <source media="(prefers-color-scheme: light)" srcset="Media/logo-signals.png">
        <img alt="Signals for Shardy" height="256" width="256" src="Media/logo-signals.png">
    </picture>
</p>
<h3 align="center">Signals for Shardy</h3>
<h4 align="center">Framework for online games and apps</h4>
<p align="center">
    <a href="#quick-start">Quick start</a> · <a href="https://github.com/mopsicus/shardy-unity">Unity client</a> · <a href="https://github.com/mopsicus/shardy">Shardy</a> · <a href="https://github.com/mopsicus/shardy-signals/issues">Report Bug</a>
</p>

# 💬 Overview

This package is a simple event manager aka eventbus aka pub-sub implementation. It is a truly focused, tiny, standalone library for reducing connectivy between systems, objects, and scenes in your project. Made for Shardy and more.

> [!NOTE]
> Shardy is a framework for online games and applications for Node.js. It provides the basic functionality for building microservices solutions: mobile, social, web, multiplayer games, realtime applications, chats, middleware services, etc.
> 
> [Read about Shardy](https://github.com/mopsicus/shardy) 💪

# ✨ Features

- Simple API: subscribe, send, handle
- Interfaces as events
- WeakReference support
- No 3rd party libs

# 🚀 Usage

### Installation

Get it from [releases page](https://github.com/mopsicus/shardy-signals/releases) or add the line to `Packages/manifest.json` and module will be installed directly from Git url:

```
"com.mopsicus.shardy.signals": "https://github.com/mopsicus/shardy-signals.git",
```

### Environment setup

For a better experience, you can set up an environment for local development. Since Shardy and all modules (like this) are developed with VS Code, all settings are provided for it.

1. Use `Monokai Pro` or `eppz!` themes
2. Use `FiraCode` font
3. Install extensions:
    - C#
    - C# Dev Kit
    - Unity
4. Enable `Inlay Hints` in C# extension
5. Install `Visual Studio Editor` package in Unity
6. Put `.editorconfig` in root project directory
7. Be cool

### Quick start

1. Install package
2. Add `Shardy.Signals` to uses section
3. Create your events/signals as interfaces
4. Subscribe to them and implement the interface
5. Send signals
6. Profit

```csharp
using Shardy.Signals;

/// <summary>
/// Demo interface
/// </summary>
public interface IDemo : ISubscriber {
    void OnSignalAction(int data);
}

/// <summary>
/// Demo class with sender/receiver
/// </summary>
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
    /// Send int data -> current subscriber count
    /// </summary>
    void Send() {
        Signals.Send<IDemo>(a => a.OnSignalAction(Signals.GetSubscriberCount<IDemo>()));
    }

    /// <summary>
    /// Called when a signal is received
    /// </summary>
    public void OnSignalAction(int data) {
        Debug.Log($"[{TAG}] action: {data}");
    }
}
```

> [!NOTE]
> You can get the number of subscribers to an event by calling the `GetSubscriberCount<T>()` method.

### WeakReference

This simple event manager uses `WeakReference` and cleans up all dead subscribers after check. You don't need to worry about unsubscribing for destroyable objects and memory leaks.

### Demo

See the sample section to get a [demo app](./Samples~/Demo). This demo contains a simple example of signals with destroyable objects and multiple subscribers.

_Tested in Unity 2020.3.x._

# 🏗️ Contributing

We invite you to contribute and help improve Signals for Shardy. Please see [contributing document](./CONTRIBUTING.md). 🤗

You also can contribute to the Shardy project by:

- Helping other users 
- Monitoring the issue queue
- Sharing it to your socials
- Referring it in your projects

# 🤝 Support

You can support Shardy by using any of the ways below:

* Bitcoin (BTC): bc1qaejavqm0r2fnx3d6mhvt8r6uqzwzun5vwkryn2
* USDT (TRC20): TLDPntuWNZRZTiWmtaKvZH7cVFXuf5TQvS
* TON: UQBrv16QN-6y2Jba0IgoKJEGB4u7_0ilPw8BXDH7M9NnExhw
* Visa, Mastercard via [Boosty](https://boosty.to/mopsicus/donate)
* MIR via [CloudTips](https://pay.cloudtips.ru/p/9f507669)

# ✉️ Contact

Before you ask a question, it is best to search for existing [issues](https://github.com/mopsicus/shardy-signals/issues) that might help you. Anyway, you can ask any questions and send suggestions by [email](mailto:mail@mopsicus.ru) or [Telegram](https://t.me/mopsicus).

# 🔑 License

Signals for Shardy is licensed under the [MIT License](./LICENSE.md). Use it for free and be happy. 🎉
