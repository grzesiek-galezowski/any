using System;

namespace TddXt.CommonTypes
{
    public class ThirdPartyGeneratorFailed : Exception
    {
        public ThirdPartyGeneratorFailed(Exception creationException) 
            : base("Third-party generation library failed", creationException)
        {
            
        }
    }
}