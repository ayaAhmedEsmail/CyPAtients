using CyPatients.Models;
using Microsoft.EntityFrameworkCore;

namespace CyPatients.Service
{
    public class PagePagination<T>
    {
        public IEnumerable<T> Patients { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagePagination(IEnumerable<T> patients, int count, int currentPage, int pageSize) {
        
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Patients = patients;
        }
        public static async Task<PagePagination<T>> GetPagintaionPatient( IQueryable<T> patients, int current , int size)
        {
            var count = await patients.CountAsync();
            var items = await patients.Skip((current - 1) / size)
                                .Take(size).ToListAsync();

            return new PagePagination<T>(items, count, current, size);
        }
    }
}
