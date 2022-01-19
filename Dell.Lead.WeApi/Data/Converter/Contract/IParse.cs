using System.Collections.Generic;

namespace Dell.Lead.WeApi.Data.Converter.Contract
{
    public interface IParse<D, O>
    {
        D Parse(O origin);
        List<D> Parse(List<O> origins);
    }
}
