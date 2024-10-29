using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healt.Entidades
{
    public class EstudiosModelo
    {
        public EstudiosModelo(string id, string especialidadMedica, string nombre, decimal precio)
        {
            Id = id;
            EspecialidadMedica = especialidadMedica;
            Nombre = nombre;
            Precio = precio;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // Added Id property for MongoDB

        public string EspecialidadMedica { get; set; }

        public string Nombre { get; set; }
        public decimal Precio { get; set; }




    }
}
