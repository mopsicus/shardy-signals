using System;
using System.Collections.Generic;
using System.Linq;
#if SHARDY_DEBUG
using UnityEngine;
#endif

namespace Shardy.Signals {

    /// <summary>
    /// Signal class with optional WeakReference support
    /// </summary>
    public static class Signals {

        /// <summary>
        /// Log tag
        /// </summary>
        public const string TAG = "SIGNALS";

        /// <summary>
        /// List of weak subscribers
        /// </summary>
        static readonly Dictionary<Type, SignalsList<ISubscriber>> _subscribers = new Dictionary<Type, SignalsList<ISubscriber>>();

        /// <summary>
        /// Cached types
        /// </summary>
        static readonly Dictionary<Type, List<Type>> _cachedTypes = new Dictionary<Type, List<Type>>();

        /// <summary>
        /// Subscribe on signals
        /// </summary>
        /// <param name="subscriber">Listener</param>
        public static void Subscribe(ISubscriber subscriber) {
            if (subscriber == null) {
                return;
            }
            var types = GetTypes(subscriber);
            foreach (var type in types) {
                if (!_subscribers.ContainsKey(type)) {
                    _subscribers[type] = new SignalsList<ISubscriber>();
                }
                _subscribers[type].Add(subscriber);
            }
        }

        /// <summary>
        /// Unsubscribe from signals
        /// </summary>
        /// <param name="subscriber">Listener</param>
        /// <param name="isUseWeak">Use weak references</param>
        public static void Unsubscribe(ISubscriber subscriber) {
            if (subscriber == null) {
                return;
            }
            var types = GetTypes(subscriber);
            foreach (var type in types) {
                if (_subscribers.ContainsKey(type)) {
                    _subscribers[type].Remove(subscriber);
                }
            }
        }

        /// <summary>
        /// Send signal
        /// </summary>
        /// <typeparam name="TSubscriber">Type of signal</typeparam>
        /// <param name="action">Action to execute</param>
        public static void Send<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, ISubscriber {
            if (action == null) {
                return;
            }
            if (!_subscribers.ContainsKey(typeof(TSubscriber))) {
#if SHARDY_DEBUG
                Debug.LogWarning($"[{TAG}] subscriber not found for: {typeof(TSubscriber)}");
#endif
                return;
            }
            var subscribers = _subscribers[typeof(TSubscriber)];
            subscribers.IsProcessing = true;
            foreach (var subscriber in subscribers.GetList()) {
                try {
                    if (subscriber != null) {
                        action.Invoke(subscriber as TSubscriber);
                    }
                } catch (Exception e) {
#if SHARDY_DEBUG
                    Debug.LogError($"[{TAG}] exception: {e}");
#endif
                }
            }
            subscribers.IsProcessing = false;
            subscribers.Cleanup();

        }

        /// <summary>
        /// Get type
        /// </summary>
        /// <param name="subscriber">Interface</param>
        public static List<Type> GetTypes(ISubscriber subscriber) {
            var type = subscriber.GetType();
            if (_cachedTypes.ContainsKey(type)) {
                return _cachedTypes[type];
            }
            var list = new List<Type>();
            var types = type.GetInterfaces();
            foreach (var item in types) {
                if (item.GetInterfaces().Contains(typeof(ISubscriber))) {
                    list.Add(item);
                }
            }
            _cachedTypes[type] = list;
            return list;
        }

        /// <summary>
        /// Get subscriber count for specific type
        /// </summary>
        /// <typeparam name="TSubscriber">Type of subscriber</typeparam>
        /// <returns>Number of subscribers</returns>
        public static int GetSubscriberCount<TSubscriber>() where TSubscriber : class, ISubscriber {
            if (!_subscribers.ContainsKey(typeof(TSubscriber))) {
                return 0;
            }
            return _subscribers[typeof(TSubscriber)].Count;
        }

        /// <summary>
        /// Force cleanup of dead references
        /// </summary>
        public static void Cleanup() {
            foreach (var subscriber in _subscribers) {
                subscriber.Value.Cleanup();
            }
        }

        /// <summary>
        /// Clear all subscribers
        /// </summary>
        public static void Clear() {
            _subscribers.Clear();
            _cachedTypes.Clear();
        }
    }
}
