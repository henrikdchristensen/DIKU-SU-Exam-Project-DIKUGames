using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Breakout;
class Block : Entity, IItem {
    private int health {
        get; set;
    }
    private int value {
        get; set;
    }

    public Block(DynamicShape shape, IBaseImage image, int health, int value) : base(shape, image) {
        this.health = health;
        this.value = value;
    }

    /// <summary> Should be called when the block is hit, and decrements health </summary>
    /// <returns> Returns true if it is dead, and false otherwise </returns>
    public bool Hit() {
        health--;
        if (health < 0)
            return true;
        return false;
    }
}