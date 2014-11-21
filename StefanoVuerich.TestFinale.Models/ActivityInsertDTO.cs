using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.Contracts
{
    public class ActivityInsertDTO
    {
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public int Duration { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
        public ActivityInsertDTO() { }
    }
}
