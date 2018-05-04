using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SculptorWebApi.DataAccess.Entities
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }
        //[Timestamp]
        //public byte[] RowVersion { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTimeOffset CreationDate { get; set; }

        [Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public DateTimeOffset LastUpdated { get; set; }
    }
}