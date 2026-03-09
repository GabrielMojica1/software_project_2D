using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using Events;

// Alex
// Tests for StatsTracker and related event classes.
// I chose to also write these as EditMode tests to avoid needing a full PlayMode setup, since we're mostly testing logic and event handling rather than actual gameplay.
// We mainly test basic functionality, as there aren't many edge cases. Primarily, we need to ensure that the StatsTracker correctly updates its counters when events are published, and that it properly subscribes/unsubscribes from the EventBus.
// To ensure that works as intended, we also test the EventBus itself and the event classes to confirm that they store data correctly and subscriptions work as intended.
// Each event class has a simple test to confirm it stores the necessary values, alhtough this isn't used in actual gameplay.
// We test EventBus for pasic funcitonality: Subscribe() correctly subscribes, Unsubscribe() correctly unsubscribes, and Publish() I. Publishes an event, and II. correctly invokes all subscribers for a given event type.
// The StatsTracker tests confirm that when events are published, the appropriate counters are updated, and that when the StatsTracker is disabled, it no longer receives events.
// Since StatsTracker is a singleton, we delete and recreate the instance for each test to avoid interference. We also clear EventBus subscribers fo rthe same reason.
public class StatsTests
{
	private GameObject _trackerObject;

	[SetUp]
	public void SetUp()
	{
		ClearEventBusSubscribers();
		ResetStatsTrackerInstance();
	}

	[TearDown]
	public void TearDown()
	{
		if (_trackerObject != null)
		{
			UnityEngine.Object.DestroyImmediate(_trackerObject);
		}

		ResetStatsTrackerInstance();
		ClearEventBusSubscribers();
	}

	[Test]
	public void EnemyKilledEvent_Constructor_SetsEnemyType()
	{
		var e = new EnemyKilledEvent("Asteroid");

		Assert.AreEqual("Asteroid", e.EnemyType);
	}

	[Test]
	public void DamageDealtEvent_Constructor_SetsAmount()
	{
		var e = new DamageDealtEvent(42);

		Assert.AreEqual(42, e.Amount);
	}

	[Test]
	public void ItemCollectedEvent_Constructor_SetsItemId()
	{
		var e = new ItemCollectedEvent("medkit_01");

		Assert.AreEqual("medkit_01", e.ItemID);
	}

	[Test]
	public void LevelCompletedEvent_Constructor_SetsLevelNumber()
	{
		var e = new LevelCompletedEvent(3);

		Assert.AreEqual(3, e.LevelNumber);
	}

	[Test]
	public void EventBus_Publish_CallsSubscriber()
	{
		var callCount = 0;

		Action<DamageDealtEvent> callback = _ => { callCount++; };
		EventBus.Subscribe(callback);

		EventBus.Publish(new DamageDealtEvent(5));

		Assert.AreEqual(1, callCount);
	}

	[Test]
	public void EventBus_Unsubscribe_StopsCallingSubscriber()
	{
		var callCount = 0;

		Action<EnemyKilledEvent> callback = _ => { callCount++; };
		EventBus.Subscribe(callback);
		EventBus.Unsubscribe(callback);

		EventBus.Publish(new EnemyKilledEvent("Drone"));

		Assert.AreEqual(0, callCount);
	}

	[Test]
	public void EventBus_Publish_InvokesAllSubscribersForType()
	{
		var a = 0;
		var b = 0;

		Action<ItemCollectedEvent> callbackA = _ => { a++; };
		Action<ItemCollectedEvent> callbackB = _ => { b++; };

		EventBus.Subscribe(callbackA);
		EventBus.Subscribe(callbackB);

		EventBus.Publish(new ItemCollectedEvent("fuel"));

		Assert.AreEqual(1, a);
		Assert.AreEqual(1, b);
	}


	[Test]
	public void StatsTracker_WhenEventsPublished_UpdatesAllCounters()
	{
		_trackerObject = new GameObject("StatsTracker");
		var tracker = _trackerObject.AddComponent<StatsTracker>();
		
		// Manually invoke OnEnable to subscribe to events (EditMode doesn't auto-call lifecycle methods)
		InvokeOnEnable(tracker);

		EventBus.Publish(new EnemyKilledEvent("Drone"));
		EventBus.Publish(new DamageDealtEvent(10));
		EventBus.Publish(new ItemCollectedEvent("ammo"));
		EventBus.Publish(new LevelCompletedEvent(1));

		Assert.AreEqual(1, tracker.enemiesKilled);
		Assert.AreEqual(10, tracker.damageDealt);
		Assert.AreEqual(1, tracker.itemsCollected);
		Assert.AreEqual(1, tracker.levelsCompleted);
	}

	[Test]
	public void StatsTracker_WhenDisabled_DoesNotReceivePublishedEvents()
	{
		_trackerObject = new GameObject("StatsTracker");
		var tracker = _trackerObject.AddComponent<StatsTracker>();
		InvokeOnEnable(tracker);

		tracker.enabled = false;
		InvokeOnDisable(tracker);
		
		EventBus.Publish(new EnemyKilledEvent(""));
		EventBus.Publish(new DamageDealtEvent(7));

		Assert.AreEqual(0, tracker.enemiesKilled);
		Assert.AreEqual(0, tracker.damageDealt);
	}

	private static void ClearEventBusSubscribers()
	{
		var field = typeof(EventBus).GetField("_subscribers", BindingFlags.NonPublic | BindingFlags.Static);
		var dictionary = field?.GetValue(null) as IDictionary;
		dictionary?.Clear();
	}

	private static void ResetStatsTrackerInstance()
	{
		var field = typeof(StatsTracker).GetField("<Instance>k__BackingField", BindingFlags.NonPublic | BindingFlags.Static);
		field?.SetValue(null, null);
	}

	private static void InvokeOnEnable(StatsTracker tracker)
	{
		var method = typeof(StatsTracker).GetMethod("OnEnable", BindingFlags.NonPublic | BindingFlags.Instance);
		method?.Invoke(tracker, null);
	}

	private static void InvokeOnDisable(StatsTracker tracker)
	{
		var method = typeof(StatsTracker).GetMethod("OnDisable", BindingFlags.NonPublic | BindingFlags.Instance);
		method?.Invoke(tracker, null);
	}
}
