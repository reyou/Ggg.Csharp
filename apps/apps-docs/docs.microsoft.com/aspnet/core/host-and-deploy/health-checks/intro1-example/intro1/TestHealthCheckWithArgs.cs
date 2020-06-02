using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace intro1
{
    public class TestHealthCheckWithArgs : IHealthCheck
    {
        public int I { get; set; }
        public string S { get; set; }

        public TestHealthCheckWithArgs(int i, string s)
        {
            I = i;
            S = s;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }
}
