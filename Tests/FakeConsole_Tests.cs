using System;
using System.Linq;
using NUnit.Framework;
using TTTCore;

namespace TTTGame.IntegrationTests
{
  [TestFixture]
  public class Fake_Console_Tests
  {
    private FakeConsole subject;

    [SetUp]
    public void Init()
    {
      subject = new FakeConsole();
    }

    [Test]
    public void WriteShouldAddNewItemToConsoleOutputList()
    {
      var input = "test item";

      subject.Write(input);
      var result = subject.ConsoleOutputList;

      Assert.That(result, Has.Member(input));
    }
  }
}