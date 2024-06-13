using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportWpf.Model
{
    public partial class Passenger
    {
        public string FIO => FirstName + " " + LastName + " " + Patronymic;
    }
}
