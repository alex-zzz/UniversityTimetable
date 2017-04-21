using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;

namespace UniversityTimetable.BLL.Interfaces
{
    public interface INewsService : IDisposable
    {
        IEnumerable<NewsDTO> GetNews();
        NewsDTO GetNewsDTOById(Guid Id);

        void AddNews(NewsDTO newsDto);
        void UpdateNews(NewsDTO newsDto);
        void DeleteNews(Guid Id);

        void Dispose();
    }
}
