using Healt.Entidades;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healt.Datos
{
    public class EstudiosRealizadosDatos
    {
        private readonly IMongoCollection<EstudiosRealizadosModelos> _EstudiosRealizados;

        public EstudiosRealizadosDatos()
        {
            // Conectar a MongoDB en localhost
            var cliente = new MongoClient("mongodb+srv://ooviedo12:admin.123@hospital.xlwki.mongodb.net/?retryWrites=true&w=majority&appName=hospital");
            var baseDatos = cliente.GetDatabase("HealtDB"); // Cambia el nombre de la base de datos según sea necesario
            _EstudiosRealizados = baseDatos.GetCollection<EstudiosRealizadosModelos>("EstudiosRealizados"); // Cambia el nombre de la colección según sea necesario
        }

        public async Task<List<EstudiosRealizadosModelos>> LeerEstudiosRealizados()
        {
            try
            {
                var filter = Builders<EstudiosRealizadosModelos>.Filter.Empty;
                var estudiosrealizados = await _EstudiosRealizados.Find(filter).ToListAsync();
                return estudiosrealizados;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer estudios de la base de datos: {ex.Message}");
            }
        }
        public async Task InsertarEstudioRealizado(IEnumerable<EstudiosRealizadosModelos> estudios)
        {
            try
            {
                await _EstudiosRealizados.InsertManyAsync(estudios);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar estudios en la base de datos: {ex.Message}");
            }
        }

        public async Task<List<EstudiosRealizadosModelos>> LeerEstudiosPorCedula(string cedula)
        {
            try
            {
                var filter = Builders<EstudiosRealizadosModelos>.Filter.Eq(e => e.Cedula, cedula);
                var estudios = await _EstudiosRealizados.Find(filter).ToListAsync();
                return estudios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer estudios por cédula: {ex.Message}");
            }
        }




    }
}
