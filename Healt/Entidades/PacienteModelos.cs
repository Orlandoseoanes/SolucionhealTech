using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Healt.Entidades
{
    public class PacienteModelos
    {
        public PacienteModelos(string nombre, string edad, string cedula, string diagnostico, string especialidadAsignada,
            string camaAsignada, string fechaIngreso, string historialMedico)
        {
            Nombre = nombre;
            Edad = edad;
            Cedula = cedula;

            Diagnostico = diagnostico;
            EspecialidadAsignada = especialidadAsignada;
            CamaAsignada = camaAsignada;
            FechaIngreso = fechaIngreso;
            HistorialMedico = historialMedico;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  // Added Id property for MongoDB

        public string Nombre { get; set; }

        public string Edad { get; set; }
        public string Cedula { get; set; }

        public string Diagnostico { get; set; }

        public string EspecialidadAsignada { get; set; }

        public string CamaAsignada { get; set; }

        public string FechaIngreso { get; set; }

        public string HistorialMedico { get; set; }
    }
}
