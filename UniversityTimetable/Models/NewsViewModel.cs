using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace UniversityTimetable.Models
{
    public class NewsViewModel
    {
        public Guid _id;

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


        public string Title { get; set; }

        public Image Img { get; set; }

        public string Content { get; set; }

        
        
    }
}