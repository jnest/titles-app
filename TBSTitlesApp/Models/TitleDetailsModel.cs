using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TBSTitlesApp.Models
{
    public class TitleDetailsModel
    {
        public string ReleaseYear { get; set; }
        public string TitleName { get; set; }
        public List<string> Genres { get; set; }
        public string Description { get; set; }
        public List<string> Participants { get; set; }
        public List<AwardModel> Nominations { get; set; }
        public List<AwardModel> Awards { get; set; }

    }
}