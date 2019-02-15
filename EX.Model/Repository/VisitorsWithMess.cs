using EX.Model.DbLayer;
//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EX.Model.Repository
{
    public class VisitorsWithMess
    {
        public IEnumerable<Visitor> visitors { get; set; }
        public string message { get; set; }
    }
}