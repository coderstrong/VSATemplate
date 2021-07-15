namespace Org.VSATemplate.WebApi
{
    public abstract class PaginationQueryt
    {
        protected virtual int MaxPageSize { get; } = 100;

        protected virtual int DefaultPageSize { get; set; } = 10;

        public int PageNumber { get; set; }

        public int PageSize
        {
            get
            {
                return DefaultPageSize;
            }
            set
            {
                DefaultPageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

        public int Skip()
        {
            if (PageNumber == 0)
                return 0;
            return (PageNumber - 1) * PageSize;
        }
    }
}
