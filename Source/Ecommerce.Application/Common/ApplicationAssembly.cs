using System.Reflection;

namespace Ecommerce.Application.Common;

// Helper class for Accessing the Application Assembly
public static class ApplicationAssembly
{
    public static Assembly Assembly => typeof(ApplicationAssembly).Assembly;
}
