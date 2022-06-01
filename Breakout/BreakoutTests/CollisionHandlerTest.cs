using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Collision;
using Breakout.Items;

namespace BreakoutTests {

    [TestFixture]
    public class CollisionHandlerTest {

        private CollisionHandler collision = CollisionHandler.GetInstance();

        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void InitiateTest() {
            
        }

        /// <summary>
        /// Test that collision between ball and block can happen.
        /// </summary>
        [Test]
        public void TestCollisionBallAndBlock() {
            DynamicShape shape = new DynamicShape(0.4f, 0.1f, 0.1f, 0.1f);
            Ball ball = new Ball(shape, new NoImage()); // pos(0.4,0.1), width(0.1,0.1)
            Block block = new Block(new StationaryShape(0.4f, 0.5f, 0.1f, 0.1f), new NoImage()); // pos(0.4,0.5), width(0.1,0.1)

            shape.ChangeDirection(new Vec2F(0.0f, 1.0f)); // move ball towards block for colliding
            
            int oldHealth = block.Health;
            Vec2F oldDir = shape.Direction.Copy();

            // Put ball and block in collision table
            collision.Subsribe(ball);
            collision.Subsribe(block);

            // Update collisions for block loosing health, which can be checked for Assert()
            collision.Update();

            // Check that block has lost health (collision has happened) and ball has changed direction
            Assert.True(oldHealth > block.Health);
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCollisionBallAndPlayer() {
            DynamicShape shape = new DynamicShape(0.4f, 0.11f, 0.1f, 0.1f);
            Ball ball = new Ball(shape, new NoImage());
            shape.ChangeDirection(new Vec2F(0.0f, -0.3f));
            Player player = new Player(new DynamicShape(0.4f, 0.01f, 0.1f, 0.1f), new NoImage());

            Vec2F oldDir = shape.Direction.Copy();
            collision.Subsribe(ball);
            collision.Subsribe(player);
            collision.Update();
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);

        }

    }

}
