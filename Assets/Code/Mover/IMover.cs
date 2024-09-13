using UnityEngine;

namespace GA.GArkanoid
{
    public interface IMover
    {
        float Speed { get; }

        void Move(Vector2 direction);
    }
}
