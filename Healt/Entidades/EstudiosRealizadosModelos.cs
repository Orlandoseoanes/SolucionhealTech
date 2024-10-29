using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healt.Entidades
{
    public class EstudiosRealizadosModelos
    {
        public EstudiosRealizadosModelos(string cedula, string examen, string resultado, string fecha)
        {
            Cedula = cedula;
            Examen = examen;
            Resultado = resultado;
            Fecha = fecha;
        }

       

        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // Added Id property for MongoDB

        public string Examen { get; set; }
        public string Cedula { get; set; }

        public string Resultado { get; set; }

        public string Fecha { get; set; }

        




    }
}
