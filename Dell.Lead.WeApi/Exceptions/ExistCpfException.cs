using System;

namespace Dell.Lead.WeApi.Exceptions
{
    public class ExistCpfException : Exception
    {
        public ExistCpfException(string messeger) : base(messeger) { }
    }
}
