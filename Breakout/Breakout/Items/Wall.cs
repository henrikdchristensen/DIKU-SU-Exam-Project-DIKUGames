﻿using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using Breakout.Collision;

namespace Breakout.Entities {

    public class Wall : GameObject {

        /// <summary>Constructor of Wall</summary>
        /// <param name="shape">StationaryShape of the wall</param>
        public Wall(StationaryShape shape) : base(shape, new NoImage()) {

        }


        /// <summary>
        /// Accepts another GameObject if a collision has occured with another GameObject,
        /// and put the Wall's instance itself into the other one.
        /// (Visitor Pattern)
        /// </summary>
        /// <param name="other">The other GameObject</param>
        /// <param name="data">Collision data</param>
        public override void Accept(IGameObjectVisitor other, CollisionHandlerData data) {
            other.WallCollision(data);
        }

    }

}
