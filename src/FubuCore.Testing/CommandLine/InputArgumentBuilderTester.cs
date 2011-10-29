using FubuCore.CommandLine;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuCore.Testing.CommandLine
{
	public class InputArgumentBuilderTester
	{
		[TestFixture]
		public class When_building_up_the_input_arguments : InputArgumentBuilderTester
		{
			private ICommandFactory factory;
			private InputArgumentBuilder inputArgumentBuilder;

			[SetUp]
			public void SetUp()
			{
				factory = MockRepository.GenerateMock<ICommandFactory>();

				inputArgumentBuilder = new InputArgumentBuilder(factory);
			}
	
			[Test]
			public void and_the_input_args_are_not_empty_the_return_args_should_equal_the_input_args()
			{
				// Arrange
				var args = new[] {"non empty"};

				// Act
				var newArgs = inputArgumentBuilder.Build(args);

				// Assert
				Assert.AreEqual(args, newArgs);
			}

			// TODO: Add more tests
		}
	}
}