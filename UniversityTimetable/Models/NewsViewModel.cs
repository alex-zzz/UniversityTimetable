using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;
using UniversityTimetable.Common.Validators;

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

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public string Img { get; set; }

        //[Required]
        [ValidateFileType("JPG,JPEG,PNG")]
        //[ValidateFileSize(1 * 2048 * 2048, ErrorMessage = "Maximum allowed file size is {0} bytes")]
        [ValidateFileSize(1 * 2048 * 2048)]
        [Display(Name = "Upload Image")]
        public HttpPostedFileBase ImgFile { get; set; }

        [Required]
        [Display(Name = "Content")]
        public string Content { get; set; }
    }
}