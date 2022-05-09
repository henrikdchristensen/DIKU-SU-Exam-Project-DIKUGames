﻿using DIKUArcade.Entities;
using Breakout.Collision;
using DIKUArcade.Graphics;
using DIKUArcade.Physics;

namespace Breakout.Items {

    class Wall : Entity, ICollidable {

        public Wall(StationaryShape shape) : base(shape, new NoImage()) {

        }

        public DynamicShape GetShape() {
            return Shape.AsDynamicShape();
        }

        public void IsCollided(DynamicShape other, CollisionData data) {
            System.Console.WriteLine("Wall collision");
        }

        public bool IsDestroyed() {
            return false;
        }
    }
}
