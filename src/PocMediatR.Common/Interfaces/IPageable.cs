namespace PocMediatR.Common.Interfaces
{
    public interface IPageable
    {
        int _page { get; set; }
        int _size { get; set; }
    }
}
