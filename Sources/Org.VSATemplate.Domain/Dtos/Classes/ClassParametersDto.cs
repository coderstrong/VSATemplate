using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public class ClassParametersDto : PaginationQuery
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}