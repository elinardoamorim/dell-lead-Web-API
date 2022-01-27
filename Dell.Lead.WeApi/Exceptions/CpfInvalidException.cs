using System;

namespace Dell.Lead.WeApi.Exceptions
{
    public class CpfInvalidException : Exception
    {
        public CpfInvalidException(string messeger) : base(messeger) { }
    }
}
