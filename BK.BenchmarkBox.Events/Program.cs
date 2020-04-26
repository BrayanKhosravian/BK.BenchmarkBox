using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using static System.Int32;

namespace BK.BenchmarkBox.Events
{
	public static class Program
	{
		public static void Main()
		{
			var summary = BenchmarkRunner.Run<Events>();

		}

	}
}
