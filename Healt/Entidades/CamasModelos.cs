using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Healt.Entidades
{
    public class CamasModelos
    {
        [BsonId] // Indica que este es el campo de identificación
        public ObjectId Id { get; set; } // Campo para el ID de MongoDB
        public int NumeroCama { get; set; }
        public string TipoCama { get; set; }
        public bool Ocupada { get; set; }
        public decimal Precio { get; set; }
    }

}
