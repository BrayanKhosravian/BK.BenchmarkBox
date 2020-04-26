using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BK.BenchmarkBox.Events
{
	public class ClassEventArg : EventArgs
	{
		public int Number { get; set; }
	}

	public struct StructEventArgs
	{
		public int Number { get; set; }
	}

	public class Events
	{
		public EventHandler<ClassEventArg> ClassEventHandler;
		public EventHandler<StructEventArgs> StructEventHandler;
		private readonly int[] _collection;
		public Events()
		{
			_collection = Enumerable.Range(Int32.MaxValue - 1001, 1000).ToArray();
			ClassEventHandler += OnClassEventHandler;
			StructEventHandler += OnStructEventHandler;
		}

		[Benchmark]
		public void ClassEvent() => Array.ForEach<int>(_collection, item => ClassEventHandler.Invoke(null, new ClassEventArg(){Number = item}));

		[Benchmark]
		public void StructEvent() =>  Array.ForEach<int>(_collection, item => StructEventHandler.Invoke(null, new StructEventArgs(){Number = item}));

		private static void OnClassEventHandler(object? sender, ClassEventArg e) { }

		private static void OnStructEventHandler(object? sender, StructEventArgs e) { }

	}
}
