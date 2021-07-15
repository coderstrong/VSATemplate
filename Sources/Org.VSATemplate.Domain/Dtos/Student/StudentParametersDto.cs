using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Dtos.Student
{
    public class StudentParametersDto : PaginationQuery
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}