using System.Reflection;

namespace Ecommerce.Infrastructure.Common;

public static class InfrastructureAssembly
{
    public static Assembly Assembly => typeof(InfrastructureAssembly).Assembly;
}
