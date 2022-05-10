using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using DIKUArcade.Events;
using Breakout.Game;


namespace Breakout.Levels;
public class LevelContainer {
    // Singleton pattern
    private static LevelContainer instance = null;
    private GameEventBus eventBus;

    private List<Level> levelList;

    private LevelLoader levelLoader;

    // A dictionary of the levels within the level container
    // public Dictionary<string, Level> Levels {
    //     get; private set;
    // }
    // Property to keep track of which level is currently being played
    public Level ActiveLevel {
        get; private set;
    }

    private int levelCounter { get; set; } = 0;

    /// <summary>
    /// Set up eventbus and subsribe to TimedEvents once, to make sure that events do not
    /// get duplicated when LevelContainer is reset in the ResetLevelContainer method.
    /// </summary>
    public LevelContainer() {
        //eventBus = GameBus.GetBus();
        //eventBus.Subscribe(GameEventType.TimedEvent, this);
        InitializeLevels();
    }

    /// <summary>
    /// Singleton pattern used to make sure only one LevelContainer exists.
    /// </summary>
    /// <returns>Existing LevelContainer or new LevelContainer</returns>
    public static LevelContainer GetLevelContainer() {
        return LevelContainer.instance ?? (LevelContainer.instance = new LevelContainer());
    }

    /// <summary>
    /// Initializes levels. The levels are hard-coded and added to the dictionary
    /// of levels. Levels are chained together in a structure, using the NextLevel property,
    /// making easy to change to next level.
    /// </summary>
    private void InitializeLevels() {
        levelLoader = new LevelLoader();
        // Initialize structure of levels
        levelList = new List<Level> {
            levelLoader.CreateLevel(Path.Combine("Assets", "Levels", "level1.txt")),
            // levelLoader.CreateLevel(Path.Combine(Directory.GetCurrentDirectory(), "../Assets/Levels", "level2.txt")),
            // levelLoader.CreateLevel(Path.Combine(Directory.GetCurrentDirectory(), "../Assets/Levels", "level3.txt")),
            levelLoader.CreateLevel(Path.Combine("Assets", "Levels", "level4.txt"))

        };
        // Set initial active level
        ActiveLevel = levelList[levelCounter];
    }

    /// <summary>
    /// Creating customers based on strings in the Level objects. Customers are created here
    /// to make the references between a customer and platforms in all levels possible.
    /// A customer is added to the queue on each level, such that they are rendered when
    /// their spawn time has expired.
    /// </summary>

    /// <summary>
    /// Follow the chain of levels and set the ActiveLevel to the next level.
    /// Invoke method to create timed events for customers in the new level, such that they
    /// spawn according to their specified spawn time.
    /// </summary>
    public void NextLevel() {
        if (++levelCounter <= levelList.Count - 1) {
            ActiveLevel = levelList[levelCounter];
        }
    }

    /// <summary>
    /// Used by the game state ChooseLevel to set the picked level.
    /// Invoke method to create timed events for customers in the new level, such that they
    /// spawn according to their specified spawn time.
    /// </summary>
    /// <param name="activeLevel">String with the name of level</param>
    public void SetActiveLevel(string activeLevel) {
        ActiveLevel = levelLoader.CreateLevel(Path.Combine(Directory.GetCurrentDirectory(), "../Assets/Levels", activeLevel));
    }

    /// <summary>
    /// Reset the LevelContainer. The method is invoked the game has ended.
    /// </summary>
    public void ResetLevelContainer() {
        InitializeLevels();
    }

    /// <summary>
    /// The LevelContainer is a IGameEventProcessor such that it can handle TimedEvents
    /// and spawn customers.
    /// </summary>
    /// <param name="eventType"></param>
    /// <param name="gameEvent"></param>
    // public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
    //     if (eventType == GameEventType.TimedEvent) {
    //         switch (gameEvent.Message) {
    //             case "ADD_CUSTOMER":
    //                 AddCustomerToLevel(
    //                     ActiveLevel.QueuedCustomers.Find(
    //                         c => c.Name == gameEvent.Parameter1));
    //                 break;
    //             case "DERENDER_CUSTOMER":
    //                 ActiveLevel.RemoveCustomerFromLevel(
    //                     ActiveLevel.Customers.Find(c => c.Name == gameEvent.Parameter1));
    //                 break;
    //         }
    //     }

    // }



}
