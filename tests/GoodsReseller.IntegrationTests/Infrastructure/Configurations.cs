using System;

namespace GoodsReseller.IntegrationTests.Infrastructure
{
    internal static class Configurations
    {
        public static readonly string BaseUrl = $"http://{TestableServiceHost}:{TestableServicePort}";
        
        private static string TestableServiceHost =>
            Environment.GetEnvironmentVariable("TESTABLE_SERVICE_HOST") ?? "localhost";

        private static int TestableServicePort =>
            int.Parse(Environment.GetEnvironmentVariable("TESTABLE_SERVICE_PORT") ?? "5000");
    }
}