using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.BLL.DTO;
using UniversityTimetable.BLL.Infrastructure;
using UniversityTimetable.BLL.Interfaces;
using UniversityTimetable.DAL.Entities;
using UniversityTimetable.DAL.Interfaces;

namespace UniversityTimetable.BLL.Services
{
    public class NewsService : INewsService
    {
        IMapper _mapper;
        IUnitOfWork Database { get; set; }

        public NewsService(IUnitOfWork uow, IMapper mapper)
        {
            _mapper = mapper;
            Database = uow;
        }

        public void AddNews(NewsDTO newsDto)
        {
            News news = _mapper.Map<NewsDTO, News>(newsDto);

            Database.News.Create(news);
            Database.Save();
        }

        public void DeleteNews(Guid id)
        {
            Database.News.Delete(id);
            Database.Save();
        }

        public IEnumerable<NewsDTO> GetNews()
        {
            return _mapper.Map<IEnumerable<News>, List<NewsDTO>>(Database.News.GetAll());
        }

        public NewsDTO GetNewsDTOById(Guid id)
        {
            News news = Database.News.Get(id);
            return _mapper.Map<News, NewsDTO>(news);
        }

        public void UpdateNews(NewsDTO newsDto)
        {
            News news = _mapper.Map<NewsDTO, News>(newsDto);

            Database.News.Update(news);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
