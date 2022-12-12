using System;
using Xunit;

namespace Valkyrie.Tests.BaseClasses
{
    [Collection("DirectoryCollection")]
    public class TestingDirectoryFixture : IDisposable
    {
        public TestingDirectoryFixture()
        {
        }

        public void Dispose()
        {
        }
    }
}