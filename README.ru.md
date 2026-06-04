<a href="./README.md">![Static Badge](https://img.shields.io/badge/english-118027)</a>
<a href="./README.ru.md">![Static Badge](https://img.shields.io/badge/russian-0390fc)</a>
<p align="center">
    <picture>
        <source media="(prefers-color-scheme: dark)" srcset="Media/logo-signals-dark.png">
        <source media="(prefers-color-scheme: light)" srcset="Media/logo-signals.png">
        <img alt="Signals для Shardy" height="256" width="256" src="Media/logo-signals.png">
    </picture>
</p>
<h3 align="center">Signals для Shardy</h3>
<h4 align="center">Фреймворк для онлайн игр и приложений</h4>
<p align="center">
    <a href="#быстрый-старт">Быстрый старт</a> · <a href="https://github.com/mopsicus/shardy-unity">Unity клиент</a> · <a href="https://github.com/mopsicus/shardy">Shardy</a> · <a href="https://github.com/mopsicus/shardy-signals/issues">Отчёт об ошибке</a>
</p>

# 💬 Описание

Этот пакет представляет собой простой менеджер событий aka шина данных (eventbus) aka паттерн публикация-подписка (pub-sub). Это узконаправленная, небольшая, автономная библиотека, предназначенная для уменьшения связности между системами, объектами и сценами в вашем проекте. Создана для Shardy и не только.

> [!NOTE] 
> Shardy – это фреймворк для онлайн игр и приложений на Node.js. Он предоставляет базовую функциональность для построения микросервисных решений: мобильных, социальных, веб, многопользовательских игр, приложений реального времени, чатов, middleware сервисов и т.п.
>
> [Узнать про Shardy](https://github.com/mopsicus/shardy) 💪

# ✨ Возможности

- Простейший API: подписка, отправка, обработка
- События в виде интерфейсов
- Поддержка WeakReference
- Не используются сторонние библиотеки

# 🚀 Использование

### Установка

Скачайте пакет со страницы [релизов](https://github.com/mopsicus/shardy-signals/releases) или добавьте строчку ниже в ваш файл `Packages/manifest.json` и пакет будет установлен по адресу Git репозитория:

```
"com.mopsicus.shardy.signals": "https://github.com/mopsicus/shardy-signals.git",
```

### Настройка окружения

Настройте своё окружение для локальной разработки для удобства и "синхронизации" с текущим проектом. Так как Shardy и все остальные модули разрабатываются с использованием редактора VS Code, то все настройки и рекомендации предложены для него.

1. Используйте `Monokai Pro` или `eppz!` тему
2. Используйте `FiraCode` шрифт
3. Установите расширения:
   - C#
   - C# Dev Kit
   - Unity
4. Включите `Inlay Hints` в настройках C# расширения
5. Установить пакет `Visual Studio Editor` в редакторе Unity
6. Поместите файл `.editorconfig` в корневую папку проекта
7. Ура!

### Быстрый старт

1. Установите пакет
2. Добавьте `Shardy.Signals` в раздел uses
3. Создайте свои события в виде интерфейсов
4. Подпишитесь на событие и реализуйте интерфейс
5. Отправьте сигнал
6. Профит

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
> Вы можете получить количество подписчиков на событие вызвав метод `GetSubscriberCount<T>()`.

### WeakReference

Этот простой менеджер событий использует `WeakReference` и удаляет всех "отвалившихся" подписчиков после проверки. Вам не нужно беспокоиться об отписке для разрушаемых объектов и утечках памяти.

### Демо

Посмотрите раздел с примерами и скачайте [демо](./Samples~/Demo). Это демо содержит простой пример менеджера событий с несколькими подписчиками и разрушаемыми объектами.

_Протестировано в Unity 2020.3.x_

# 🏗️ Развитие

Мы приглашаем вас внести свой вклад и помочь улучшить Signals для Shardy. Пожалуйста, ознакомьтесь с [документом](./CONTRIBUTING.md). 🤗

Вы также можете внести свой вклад в проект Shardy:

- Помогая другим пользователям
- Мониторя список существующих проблем
- Рассказав о проекте в своих соцсетях
- Используя его в своих проектах

# 🤝 Поддержка

Вы можете поддержать проект любым из способов ниже:

* Bitcoin (BTC): bc1qaejavqm0r2fnx3d6mhvt8r6uqzwzun5vwkryn2
* USDT (TRC20): TLDPntuWNZRZTiWmtaKvZH7cVFXuf5TQvS
* TON: UQBrv16QN-6y2Jba0IgoKJEGB4u7_0ilPw8BXDH7M9NnExhw
* Карты Visa, Mastercard через [Boosty](https://boosty.to/mopsicus/donate)
* Карты МИР через [CloudTips](https://pay.cloudtips.ru/p/9f507669)

# ✉️ Контактная информация

Перед тем как задать вопрос, лучшим решением будет посмотреть уже существующие [проблемы](https://github.com/mopsicus/shardy-signals/issues), это может помочь. В любом случае, вы можете задать любой вопрос или отправить предложение по [email](mailto:mail@mopsicus.ru) или [Telegram](https://t.me/mopsicus).

# 🔑 Лицензия

Signals для Shardy выпущен под лицензией [MIT](./LICENSE.md). Используйте бесплатно и радуйтесь. 🎉
