using System;
using System.Collections.Generic;
using UnityEngine;

namespace JHI.Dict
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();
        
        public static void RegisterService<T>(T service)
        {
            if (_services.ContainsKey(typeof(T)))
            {
                Debug.LogError($"Service type {nameof(T)} already registered");
                return;
            }
            
            _services.Add(typeof(T), service);
        }

        public static T GetService<T>()
        {
            if (!_services.TryGetValue(typeof(T), out var service))
            {
                Debug.LogError($"Service type {typeof(T).Name} not registered");
                return default;
            }

            return (T)service;
        }
    }
}