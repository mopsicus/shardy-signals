using System;
using System.Collections.Generic;

namespace Shardy.Signals {

    /// <summary>
    /// Controls list of subscribers using weak references
    /// </summary>
    class SignalsList<TSubscriber> where TSubscriber : class {

        /// <summary>
        /// Flag to check is process run
        /// </summary>
        public bool IsProcessing = false;

        /// <summary>
        /// Flag to cleanup
        /// </summary>
        bool _isNeedsCleanup = false;

        /// <summary>
        /// List of weak references to subscribers
        /// </summary>
        readonly List<WeakReference> _weak = new List<WeakReference>();

        /// <summary>
        /// Cached list of alive subscribers for iteration
        /// </summary>
        readonly List<TSubscriber> _list = new List<TSubscriber>();

        /// <summary>
        /// Get count of alive subscribers
        /// </summary>
        public int Count {
            get {
                var count = 0;
                for (var i = 0; i < _weak.Count; i++) {
                    if (IsAlive(_weak[i].Target)) {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Add subscriber
        /// </summary>
        public void Add(TSubscriber subscriber) {
            if (subscriber == null) {
                return;
            }
            if (ContainsKey(subscriber)) {
                return;
            }
            _weak.Add(new WeakReference(subscriber));
        }

        /// <summary>
        /// Check subscriber for this type
        /// </summary>
        public bool ContainsKey(TSubscriber subscriber) {
            if (subscriber == null) {
                return false;
            }
            for (var i = 0; i < _weak.Count; i++) {
                if (_weak[i].Target is TSubscriber target && ReferenceEquals(target, subscriber)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Remove subscriber
        /// </summary>
        public void Remove(TSubscriber subscriber) {
            if (subscriber == null) {
                return;
            }
            if (IsProcessing) {
                for (var i = 0; i < _weak.Count; i++) {
                    if (_weak[i].Target is TSubscriber target && ReferenceEquals(target, subscriber)) {
                        _weak[i].Target = null;
                        _isNeedsCleanup = true;
                        break;
                    }
                }
            } else {
                for (var i = _weak.Count - 1; i >= 0; i--) {
                    var target = _weak[i].Target as TSubscriber;
                    if (!IsAlive(target) || ReferenceEquals(target, subscriber)) {
                        _weak.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Get list of alive subscribers for iteration
        /// </summary>
        public List<TSubscriber> GetList() {
            _list.Clear();
            for (var i = 0; i < _weak.Count; i++) {
                if (_weak[i].Target is TSubscriber target && IsAlive(target)) {
                    _list.Add(target);
                }
            }
            return _list;
        }

        /// <summary>
        /// Clean up dead references and marked for removal
        /// </summary>
        public void Cleanup() {
            if (!_isNeedsCleanup && !HasDeadReferences()) {
                return;
            }
            for (var i = _weak.Count - 1; i >= 0; i--) {
                if (!IsAlive(_weak[i].Target)) {
                    _weak.RemoveAt(i);
                }
            }
            _isNeedsCleanup = false;
        }

        /// <summary>
        /// Check if there are any dead references
        /// </summary>
        bool HasDeadReferences() {
            for (var i = 0; i < _weak.Count; i++) {
                if (!IsAlive(_weak[i].Target)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Check if object is alive, check custom null behavior
        /// </summary>
        bool IsAlive(object target) {
            if (target == null) {
                return false;
            }
            if (target is UnityEngine.Object obj && obj == null) {
                return false;
            }
            return true;
        }
    }
}
