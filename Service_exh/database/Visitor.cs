namespace Service_exh.database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [DataContract(Namespace = "http://iterator")]
    public partial class Visitor
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
