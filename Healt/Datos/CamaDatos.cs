using Healt.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Healt.Datos
{

    public class CamaDatos
    {
        private readonly IMongoCollection<CamasModelos> _camas;

        public CamaDatos()
        {
            // Conectar a MongoDB en localhost
            var cliente = new MongoClient("mongodb+srv://ooviedo12:admin.123@hospital.xlwki.mongodb.net/?retryWrites=true&w=majority&appName=hospital");
            var baseDatos = cliente.GetDatabase("HealtDB"); // Cambia el nombre de la base de datos según sea necesario
            _camas = baseDatos.GetCollection<CamasModelos>("Camas"); // Cambia el nombre de la colección según sea necesario
        }

        public async Task<List<CamasModelos>> LeerCamas()
        {
            return await _camas.Find(new BsonDocument()).ToListAsync();
        }

       
        // Nuevo método para obtener camas disponibles
        public async Task<List<CamasModelos>> ObtenerCamasDisponibles()
        {
            try
            {
                var filtro = Builders<CamasModelos>.Filter.Eq(c => c.Ocupada, false);
                var camas = await _camas.Find(filtro)
                                         .Sort(Builders<CamasModelos>.Sort.Ascending(c => c.NumeroCama))
                                         .ToListAsync();
                return camas;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener camas disponibles de la base de datos: {ex.Message}");
                // Aquí podrías también registrar el stack trace si es necesario
                return new List<CamasModelos>();
            }
        }

        public async Task<CamasModelos> ActualizarCamaAocupada(int numeroCama)  
        {
            try
            {
                var filtro = Builders<CamasModelos>.Filter.Eq(c => c.NumeroCama, numeroCama);

                var actualizacion = Builders<CamasModelos>.Update
                    .Set(c => c.Ocupada, true);

                var resultado = await _camas.FindOneAndUpdateAsync(
                    filtro,
                    actualizacion,
                    new FindOneAndUpdateOptions<CamasModelos>
                    {
                        ReturnDocument = ReturnDocument.After
                    }
                );

                return resultado;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar la cama {numeroCama}: {ex.Message}");
                return null;
            }
        }




    }

}
