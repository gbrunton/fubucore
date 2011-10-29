using System.Collections.Generic;
using System.Reflection;

namespace FubuCore.CommandLine
{
    public class BooleanFlag : TokenHandlerBase
    {
        private readonly PropertyInfo _property;

        public BooleanFlag(PropertyInfo property) : base(property)
        {
            _property = property;
        }

        public override bool Handle(object input, Queue<string> tokens)
        {
            if (tokens.NextIsFlag(_property))
            {
                tokens.Dequeue();
                _property.SetValue(input, true, null);

                return true;
            }

            return false;
        }

        public override string ToUsageDescription()
        {
            return "[{0}]".ToFormat(InputParser.ToFlagName(_property));
        }

		public override string PromptForArg()
		{
			var propertyName = PropertyName;
			var description = Description;
			return string.Format("Optional {0}{1} (y for true / empty for false): ", propertyName, propertyName == description ? string.Empty : " - " + description);
		}

		public string ConvertPromptArg(string argFromPrompt)
		{
			return string.IsNullOrEmpty(argFromPrompt) ? string.Empty : ConvertPromptArg();
		}
    }
}