using System;
using UniRx;
using UnityEngine;

namespace Core.GamePlay.Input
{
    public interface IInputService
    {
        public IObservable<Unit> PointerDown { get; }
        public IObservable<Unit> PointerUp { get; }
        public Vector2 GetPointerPosition();
    }
}