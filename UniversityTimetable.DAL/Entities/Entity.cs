using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityTimetable.DAL.Interfaces;

namespace UniversityTimetable.DAL.Entities
{
    public class Entity : IEntity
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (this._id == Guid.Empty)
                    this._id = Guid.NewGuid();
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime CreatedDate { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
