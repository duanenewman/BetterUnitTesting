using System;
using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace BetterUnitTestingNUnitTests
{
	public abstract class BaseTest
	{
		protected UnityContainer Container;
		protected LifetimeResetter Resetter { get; set; }

		protected BaseTest()
		{
			Resetter = new LifetimeResetter();
			Container = new UnityContainer();
		}

		[SetUp]
		public void OnTestSetup()
		{
			Resetter.Reset();
		}

		protected void RegisterResettableType<T>(params InjectionMember[] injectionMembers)
		{
			Container.RegisterType<T>(new ResettableLifetimeManager(Resetter), injectionMembers);
		}

		protected void RegisterResettableType<T>(Func<Action<Mock<T>>> onCreatedCallbackFactory) where T : class
		{
			RegisterResettableType<T>(new InjectionFactory(c => CreateMockInstance(onCreatedCallbackFactory)));
		}

		protected T CreateMockInstance<T>(Func<Action<Mock<T>>> onCreatedCallbackFactory) where T : class
		{
			var mock = new Mock<T>();
			var onCreatedCallback = onCreatedCallbackFactory();
			onCreatedCallback?.Invoke(mock);
			return mock.Object;
		}

		protected class LifetimeResetter
		{
			public event EventHandler<EventArgs> OnReset;

			public void Reset()
			{
				OnReset?.Invoke(this, EventArgs.Empty);
			}
		}

		protected class ResettableLifetimeManager : LifetimeManager
		{
			public ResettableLifetimeManager(LifetimeResetter lifetimeResetter)
			{
				lifetimeResetter.OnReset += (o, args) => instance = null;
			}

			private object instance;

			public override object GetValue()
			{
				return instance;
			}

			public override void SetValue(object newValue)
			{
				instance = newValue;
			}

			public override void RemoveValue()
			{
				instance = null;
			}
		}
	}
}