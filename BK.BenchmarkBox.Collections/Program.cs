using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace BK.BenchmarkBox.Collections
{
	public class Program
	{

		public static void Main()
		{
			var summary = BenchmarkRunner.Run<FindFirst>();
		}
	}

	public class Item
	{
		public int Id { get; }

		public Item(int id)
		{
			Id = id;
		}
	}

	public class FindFirst
	{
		private const int _count = 1_000_000;
		private static readonly List<Item> _items = Enumerable.Range(1, _count).Select(i => new Item(i)).ToList();

		[Benchmark]
		public Item FirstOrDefault() => _items.FirstOrDefault(item => item.Id == _count);

		[Benchmark]
		public Item Find() => _items.Find(item => item.Id == _count);
	}
}
