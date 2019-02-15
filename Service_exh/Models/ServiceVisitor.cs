using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Service_exh.Models
{
    [DataContract(Namespace ="http://iterator")]
    class ServiceVisitor
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Collumn1 { get; set; }
        [DataMember]
        public string Collumn2 { get; set; }
        [DataMember]
        public string Collumn3 { get; set; }
        [DataMember]
        public string Collumn4 { get; set; }
        [DataMember]
        public string Collumn5 { get; set; }
        [DataMember]
        public string Collumn6 { get; set; }
        [DataMember]
        public string Collumn7 { get; set; }
        [DataMember]
        public string Collumn8 { get; set; }
        [DataMember]
        public string Collumn9 { get; set; }
        [DataMember]
        public string Collumn10 { get; set; }
        [DataMember]
        public string Collumn11 { get; set; }
        [DataMember]
        public string Collumn12 { get; set; }
        [DataMember]
        public string Collumn13 { get; set; }
        [DataMember]
        public string Collumn14 { get; set; }
        [DataMember]
        public string Collumn15 { get; set; }
    }
}
