using UnityEngine;

namespace Events
{   // events we want to track go in here
    // I made some examples, but whichever events 
    // we want to track can be added as needed
    public struct EnemyKilledEvent
    {
        public string EnemyType;

        public EnemyKilledEvent(string type)
        {
            EnemyType = type;
        }
    }

    public struct DamageDealtEvent
    {
        public int Amount;

        public DamageDealtEvent(int amount)
        {
            Amount = amount;
        }
    }

    public struct ItemCollectedEvent
    {
        public string ItemID;

        public ItemCollectedEvent(string id)
        {
            ItemID = id;
        }
    }

    public struct LevelCompletedEvent
    {
        public int LevelNumber;

        public LevelCompletedEvent(int level)
        {
            LevelNumber = level;
        }
    }

    public struct GameStateChangedEvent
    {
        public GameManager.GameState NewState;

        public GameStateChangedEvent(GameManager.GameState newState)
        {
            NewState = newState;
        }
    }

    public struct GameCompletedEvent
    {
        public GameCompletedEvent(int foo /*stops compiler from complaining*/) { }
    }
}
