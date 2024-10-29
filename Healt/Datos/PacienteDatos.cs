using Healt.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Healt.Datos
{
    public class PacienteDatos
    {
        private readonly IMongoCollection<PacienteModelos> _pacientes;

        public PacienteDatos()
        {
            // Conectar a MongoDB en localhost
            var cliente = new MongoClient("mongodb+srv://ooviedo12:admin.123@hospital.xlwki.mongodb.net/?retryWrites=true&w=majority&appName=hospital");
            var baseDatos = cliente.GetDatabase("HealtDB"); // Cambia el nombre de la base de datos según sea necesario
            _pacientes = baseDatos.GetCollection<PacienteModelos>("Pacientes"); // Cambia el nombre de la colección según sea necesario
        }

        public async Task<List<PacienteModelos>> CargarPacientes()
        {
            return await _pacientes.Find(new BsonDocument()).ToListAsync();
        }

        public async Task GuardarPacientes(List<PacienteModelos> pacientes)
        {
            await _pacientes.InsertManyAsync(pacientes);
        }
        public async Task<PacienteModelos> BuscarPorId(string id)
        {
            var filter = Builders<PacienteModelos>.Filter.Eq(p => p.Id, id);
            return await _pacientes.Find(filter).FirstOrDefaultAsync();
        }
        public async Task ActualizarEspecialidadAsignada(string id, string nuevaEspecialidad)
        {
            var filter = Builders<PacienteModelos>.Filter.Eq(p => p.Id, id);
            var update = Builders<PacienteModelos>.Update.Set(p => p.EspecialidadAsignada, nuevaEspecialidad);
            await _pacientes.UpdateOneAsync(filter, update);
        }

        public async Task<PacienteModelos> BuscarPorCedula(string cedula)
        {
            var filter = Builders<PacienteModelos>.Filter.Eq(p => p.Cedula, cedula);
            return await _pacientes.Find(filter).FirstOrDefaultAsync();
        }

       //falta un metodo  que por cedula permita añadir informacion a la historia clinica



    }


}
