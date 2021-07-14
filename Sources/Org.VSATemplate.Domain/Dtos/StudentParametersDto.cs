using MakeSimple.SharedKernel.Contract;

namespace Org.VSATemplate.Domain.Students.Dtos
{
    public class StudentParametersDto : PaginationQuery
    {
        public string Filters { get; set; }
        public string SortOrder { get; set; }
    }
}