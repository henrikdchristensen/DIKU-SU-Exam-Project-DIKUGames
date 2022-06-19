using NUnit.Framework;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using Breakout;
using Breakout.Collision;
using Breakout.Entities;
using DIKUArcade.GUI;

namespace BreakoutTests {

    [TestFixture]
    public class CollisionHandlerTest {

        private CollisionHandler collisionHandler;

        [SetUp]
        public void InitializeTest() {
            collisionHandler = new CollisionHandler();
        }

        /// <summary>
        /// Integration test: Test that collision between ball and block can happen.
        /// </summary>
        [Test]
        public void CollisionBallAndBlockTest() {
            Ball ball = new Ball(new Vec2F(0.4f, 0.1f), new Vec2F(0.1f, 0.1f), new NoImage()); // pos(0.4,0.1)
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(0.0f, 1.0f));

            Block block = new Block(new StationaryShape(0.4f, 0.4f, 0.1f, 0.1f), new NoImage()); // pos(0.4,0.5)

            // Store old direction and health for comparing
            Vec2F oldDir = shape.Direction.Copy();
            int oldHealth = block.Health;

            // Put ball and block in collision table
            collisionHandler.Subsribe(ball);
            collisionHandler.Subsribe(block);

            // Update collisions for block loosing health, which can be checked for Assert()
            collisionHandler.Update();

            // Check that block has lost health and ball has changed direction after colliding
            Assert.True(oldHealth > block.Health);
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        /// <summary>
        /// Integration test: Test that collision between ball and player can happen.
        /// </summary>
        [Test]
        public void CollisionBallAndPlayerTest() {
            Window.CreateOpenGLContext();
            Ball ball = new Ball(new Vec2F(0.4f, 0.1f), new Vec2F(0.1f, 0.1f), new NoImage()); // pos(0.4,0.4)
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(0.0f, 1.0f));

            Player player = new Player(new DynamicShape(0.4f, 0.4f, 0.1f, 0.1f), new NoImage()); // pos(0.4,0.1)

            // Store old direction
            Vec2F oldDir = shape.Direction.Copy();

            // Put ball and block in collision table
            collisionHandler.Subsribe(ball);
            collisionHandler.Subsribe(player);

            // Update collisions for block loosing health, which can be checked for Assert()
            collisionHandler.Update();

            // Check that ball has changed direction after colliding
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        [Test]
        public void CollisionWallAndPlayerTest() {
            Window.CreateOpenGLContext();
            Ball ball = new Ball(new Vec2F(0.4f, 0.1f), new Vec2F(0.1f, 0.1f), new NoImage()); // pos(0.4,0.4)
            var shape = ball.Shape.AsDynamicShape();
            shape.ChangeDirection(new Vec2F(0.0f, 1.0f));

            Wall wall = new Wall(new StationaryShape(0.4f, 0.4f, 0.1f, 0.1f)); // pos(0.4,0.1)

            // Store old direction
            Vec2F oldDir = shape.Direction.Copy();

            // Put ball and block in collision table
            collisionHandler.Subsribe(ball);
            collisionHandler.Subsribe(wall);

            // Update collisions for block loosing health, which can be checked for Assert()
            collisionHandler.Update();

            // Check that ball has changed direction after colliding
            Assert.True(oldDir.X != shape.Direction.X || oldDir.Y != shape.Direction.Y);
        }

        /// <summary>
        /// Whitebox test: 100% statement- and branch coverage.
        /// Test both that collision can happen and not depending on given direction.
        /// </summary>
        [TestCase(0.0f, true, false)]  // B1b:       Same objects
        [TestCase(1.0f, false, true)]  // B1a & B2a: Different and collided objects
        [TestCase(0.0f, false, false)] // B1a & B2b: Different and not collided objects
        public void CollisionUpdateTest(float yDirection, bool sameObject, bool expectedOutput) {
            DynamicShape shape = new DynamicShape(0.0f, 0.0f, 0.1f, 0.1f);
            GameObjectSpy obj1 = new GameObjectSpy(shape, new NoImage()); // pos: (0,0)
            GameObjectSpy obj2 = sameObject ? obj1 : new GameObjectSpy(new StationaryShape(0.0f, 1.0f, 0.1f, 0.1f), new NoImage()); // pos: (0,1)

            // Put objects in CollidableList
            collisionHandler.Subsribe(obj1);
            collisionHandler.Subsribe(obj2);

            // Precondition: Assert that no object is null CollidableList
            foreach (var item in collisionHandler.CollidableList) {
                if (item == null) {
                    Assert.Fail("Precondition: A null object in CollidableList is detected");
                }
            }

            // Set direction of obj1 to collide with obj2
            shape.ChangeDirection(new Vec2F(0.0f, yDirection));

            // Update CollidableList
            collisionHandler.Update();

            // Postcondition: Assert that objects has collided according to expected
            Assert.AreEqual(expectedOutput, obj1.hasCollided, "Postcondition: obj1.hasCollided");
            Assert.AreEqual(expectedOutput, obj2.hasCollided, "Postcondition: obj2.hasCollided");
        }
    }

}
