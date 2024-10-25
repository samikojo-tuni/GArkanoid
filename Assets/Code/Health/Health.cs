using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GA.GArkanoid
{
	public class Health : MonoBehaviour, IHealth
	{
		// A member variable storing current health points.
		// The underscore is a common naming convention for private fields.
		private int _hp;

		[SerializeField] private int _minHP;
		[SerializeField] private int _maxHP;
		[SerializeField] private int _initialHP;

		public int HP
		{
			get { return _hp; }
			private set
			{
				// This set accessor is private, so it can only be called from within this class.
				// The value keyword is a special keyword in C# that represents the value being 
				// assigned to the property.
				// The Mathf.Clamp function is used to ensure that the health points are 
				// within the valid range of MinHP and MaxHP.
				_hp = Mathf.Clamp(value, MinHP, MaxHP);
				if (_hp <= MinHP)
				{
					Die();
				}

				// TODO: Raise an event about the health change and dying if necessary.
			}
		}

		public int MaxHP { get { return _maxHP; } }

		public int MinHP { get { return _minHP; } }

		public int InitialHP { get { return _initialHP; } }

		private void Start()
		{
			// Initializes the health points to the initial value.
			HP = InitialHP;
		}

		protected virtual void Die()
		{
			// Disables the GameObject, when the health points reach MinHP.
			gameObject.SetActive(false);
		}

		public void Heal(int heal)
		{
			// Increases the health points by the heal amount.
			if (heal > 0)
			{
				HP += heal;
			}
		}

		public void TakeDamage(int damage)
		{
			// Reduces the health points by the damage amount.
			if (damage > 0)
			{
				HP -= damage;
			}
		}
	}
}
