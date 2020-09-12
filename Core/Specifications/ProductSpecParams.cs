namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? _pageSize : value;
        }

        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private string _search;

        public string Search
        {
            get => _search;
            set => _search = value.ToString();
        }
    }
}