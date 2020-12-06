using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSU.ThreeLayer.WebPublicLogic.Models
{
    public class RatingVM
    {
        public RatingVM()
        {

        }

        public int ID { get; set; }

        public IEnumerable<SelectListItem> Options
        {
            get
            {
                return Enumerable.Range(1, 5).Select(i => new SelectListItem { Text = i.ToString(), Value = i.ToString() } );
            }
        }

        public string SelectedRating { get; set; }
    }
}