using System;
using System.Collections.Generic;
using System.Linq;

namespace FubuCore.CommandLine
{
	public class InputArgumentBuilder
	{
		private readonly ICommandFactory factory;

		public InputArgumentBuilder(ICommandFactory factory)
		{
			if (factory == null) throw new ArgumentNullException("factory");
			this.factory = factory;
		}

		// TODO: Refactor. To much stuff going on in here.
		public string[] Build(string[] args)
		{
			if (args == null) throw new ArgumentNullException("args");
			if (args.Any()) return args;
			var appName = factory.GetAppName();
			Console.WriteLine("Available Commands:");
			var allCommandTypes = factory.AllCommandTypes().OrderBy(x => x.Name);
			for (var i = 0; i < allCommandTypes.Count(); i++)
			{
				var commandOption = allCommandTypes.ElementAt(i);
				Console.WriteLine("{0}. {1} - {2}", i + 1, commandOption.Name, new UsageGraph(appName, commandOption).Description);
			}
			Console.Write("Please select a command to run: ");
			var commandToRunNumber = Console.ReadLine();
			var command = allCommandTypes.ElementAt(int.Parse(commandToRunNumber) - 1);
			var usageGraph = new UsageGraph(appName, command);
			var commandName = usageGraph.CommandName;
			var argsAsStrings = new List<string> { commandName };
			if (commandName.Contains("help")) return argsAsStrings.ToArray();

			foreach (var handler in usageGraph.Handlers)
			{
				Console.Write(handler.PromptForArg());
				var arg = Console.ReadLine();
				Console.WriteLine();

				if (string.IsNullOrEmpty(arg)) continue;

				var flag = handler as Flag;
				var booleanFlag = handler as BooleanFlag;
				if (flag != null)
				{
					argsAsStrings.Add(flag.ConvertPromptArg());
				}
				if (booleanFlag != null)
				{
					arg = booleanFlag.ConvertPromptArg(arg);
				}
				argsAsStrings.Add(arg);
			}

			return argsAsStrings.ToArray();
		}
	}
}