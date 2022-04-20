using System.Threading.Tasks;
using Functional.Maybe;

namespace AnySpecification.Fixtures;

public interface IObjectWithAsyncMethod
{
  Task<Maybe<string>> GetSthAsync(int i);
}