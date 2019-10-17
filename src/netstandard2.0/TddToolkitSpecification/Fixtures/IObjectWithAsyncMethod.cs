using System.Threading.Tasks;
using Functional.Maybe;

namespace TddToolkitSpecification.Fixtures
{
  public interface IObjectWithAsyncMethod
  {
    Task<Maybe<string>> GetSthAsync(int i);
  }
}