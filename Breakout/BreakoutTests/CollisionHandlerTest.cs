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
        /// Integration test: Test that collision between ball and block can happen.
        /// </summary>
        [Test]
        public void TestCollisionBallAndBlock() {
            DynamicShape shape = new DynamicShape(0.4f, 0.1f, 0.1f, 0.1f);
            Ball ball = new Ball(shape, new NoImage()); // pos(0.4,0.1)
            Block block = new Block(new StationaryShape(0.4f, 0.4f, 0.1f, 0.1f), new NoImage()); // pos(0.4,0.5)

            // Store old direction and health for comparing
            Vec2F oldDir = shape.Direction.Copy();
            int oldHealth = block.Health;

            // Put ball and block in collision table
            collision.Subsribe(ball);
            collision.Subsribe(block);

            // Move ball towards block for colliding
            shape.ChangeDirection(new Vec2F(0.0f, 1.0f));

            // Update collisions for block loosing health, which can be checked for Assert()
            collision.Update();

            // Check that block has lost health and ball has changed direction after colliding
            Assert.True(oldHealth > block.Health);
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        /// <summary>
        /// Integration test: Test that collision between ball and player can happen.
        /// </summary>
        [Test]
        public void TestCollisionBallAndPlayer() {
            DynamicShape shape = new DynamicShape(0.4f, 0.4f, 0.1f, 0.1f);
            Ball ball = new Ball(shape, new NoImage()); // pos(0.4,0.4)
            Player player = new Player(new DynamicShape(0.4f, 0.1f, 0.1f, 0.1f), new NoImage()); // pos(0.4,0.1)

            // Store old direction
            Vec2F oldDir = shape.Direction.Copy();

            // Put ball and block in collision table
            collision.Subsribe(ball);
            collision.Subsribe(player);

            shape.ChangeDirection(new Vec2F(0.0f, -1.0f)); // move ball towards player for colliding

            // Update collisions for block loosing health, which can be checked for Assert()
            collision.Update();

            // Check that ball has changed direction after colliding
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        /// <summary>
        /// Whitebox test: 100% Statement and Branch coverage:
        /// Test both that collision can happen and not depending on given direction.
        /// </summary>
        [TestCase(0.0f, false)] // Only B1 executed
        [TestCase(1.0f, true)] // Both B1 & B1 executed (100% statement coverage)
        // Both test cases ensures 100% branch coverage
        public void TestUpdate(float dir, bool expected) {
            DynamicShape shape = new DynamicShape(0.0f, 0.0f, 0.1f, 0.1f);
            GameObjectSpy obj1 = new GameObjectSpy(shape, new NoImage());
            GameObjectSpy obj2 = new GameObjectSpy(new StationaryShape(0.0f, 1.0f, 0.1f, 0.1f), new NoImage());

            // Store old hasCollided (should here be false)
            bool objOldHasCollided = obj1.hasCollided || obj2.hasCollided;

            // Put shapes in collision table
            collision.Subsribe(obj1);
            collision.Subsribe(obj2);

            // Move shape towards other shape for collision
            shape.ChangeDirection(new Vec2F(0.0f, dir));

            // Update collisions for shapes for checking that Accept method has been called
            collision.Update();

            // Check that oldHasCollided is false and
            Assert.True(objOldHasCollided == false &&
                obj1.hasCollided == expected && obj2.hasCollided == expected);
        }

    }

}
