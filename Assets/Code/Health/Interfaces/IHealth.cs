using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
    public interface IHealth
    {
        int HP { get; }
        int MaxHP { get; }
        int MinHP { get; }
        int InitialHP { get; }

        void TakeDamage(int damage);
        void Heal(int heal);
    }
}
