using Godot;
using System;

namespace Dulagun.Enemies {
	/// <summary>
    /// An overall weak slime creature capable of melee attacks, overwhelming in numbers
    /// </summary>
	public class slime : BaseEnemy
	{
		public slime() {
			health = 40;
			speed = 70;
		}

		public override void _Process(float delta) {
			base._Process(delta);

			// Extremely basic placeholder AI to follow the player
			if (playerSpotted && playerRef != null) {
				Vector2 positionToPlayerDiff = playerRef.GlobalPosition - GlobalPosition;
				vel.x = positionToPlayerDiff.x * delta;
				GetNode<AnimatedSprite>("AnimatedSprite").Play("walk");
			}

			vel.x *= speed;
			MoveAndSlide(vel);
		}

		// <summary>
        /// Store the player when spotted
        /// </summary>
		public void _onSearchBodyEntered(Node body) {
			if (body is player) {
				playerRef = body as player;
				playerSpotted = true;
			}

		}	
	}
}

