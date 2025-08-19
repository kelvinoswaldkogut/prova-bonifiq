namespace ProvaPub.Models
{
    public class PagLists<T> 
    {
        public List<T> Itens { get; set; } = new List<T>();
        public bool HasNext { get; set; }
        public int TotalCount { get; set; }
        
    }
}
