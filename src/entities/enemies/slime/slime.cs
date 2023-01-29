using Godot;
using System;
using Dulagun.Enemies;

namespace Dulagun.Enemies {
	/// <summary>
    /// A slow moving and overall weak slime creature capable of melee attacks and slow projectiles 
    /// </summary>
	public class slime : BaseEnemy
	{
		public slime() {
			health = 40;
		}
	}
}

