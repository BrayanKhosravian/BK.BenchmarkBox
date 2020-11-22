using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BK.BenchmarkBox.Events
{
	public class ClassEventArg : EventArgs
	{
		public ClassEventArg(int number1, int number2, int number3)
		{
			Number1 = number1;
			Number2 = number2;
			Number3 = number3;
		}

		public int Number1 { get; }
		public int Number2 { get; }
		public int Number3 { get; }
	}

	public readonly struct StructEventArgs
	{
		public StructEventArgs(int number1, int number2, int number3)
		{
			Number1 = number1;
			Number2 = number2;
			Number3 = number3;
		}

		public readonly int Number1;
		public readonly int Number2;
		public readonly int Number3;
	}

	[Serializable]
	public delegate void ValueEventHandler<TEventArgs>(object sender, in TEventArgs e)
		where TEventArgs : struct;

	public class Events
	{
		public EventHandler<ClassEventArg> ClassEventHandler;
		public EventHandler<StructEventArgs> StructEventHandler;
		public ValueEventHandler<StructEventArgs> ValueEventHandler;

		private readonly int[] _collection;
		public Events()
		{
			_collection = Enumerable.Range(Int32.MaxValue - 1001, 1000).ToArray();
			ClassEventHandler += OnClassEventHandler;
			StructEventHandler += OnStructEventHandler;
			ValueEventHandler += OnValueEventHandler;
		}

		[Benchmark]
		public void ClassEvent() => Array.ForEach<int>(_collection, item =>
			ClassEventHandler.Invoke(null, new ClassEventArg(item, item, item)));

		[Benchmark]
		public void StructEvent() => Array.ForEach<int>(_collection, item =>
		   StructEventHandler.Invoke(null, new StructEventArgs(item, item, item)));

		[Benchmark]
		public void ValueEvent() => Array.ForEach<int>(_collection, item =>
		{
			var val = new StructEventArgs(item, item, item);
			ValueEventHandler.Invoke(null, in val);
		});


		private static void OnClassEventHandler(object sender, ClassEventArg e) { }
		private static void OnStructEventHandler(object sender, StructEventArgs e) { }
		private void OnValueEventHandler(object sender, in StructEventArgs e) { }
	}
}
