using Healt.Entidades;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healt.Datos
{
    public class EstudiosDatos
    {
        private readonly IMongoCollection<EstudiosModelo> _Estudios;

        public EstudiosDatos()
        {
            // Conectar a MongoDB en localhost
            var cliente = new MongoClient("mongodb+srv://ooviedo12:admin.123@hospital.xlwki.mongodb.net/?retryWrites=true&w=majority&appName=hospital");
            var baseDatos = cliente.GetDatabase("HealtDB"); // Cambia el nombre de la base de datos según sea necesario
            _Estudios = baseDatos.GetCollection<EstudiosModelo>("Estudios"); // Cambia el nombre de la colección según sea necesario
        }

        public async Task<List<EstudiosModelo>> LeerEstudios()
        {
            try
            {
                var filter = Builders<EstudiosModelo>.Filter.Empty;
                var estudios = await _Estudios.Find(filter).ToListAsync();
                return estudios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer estudios de la base de datos: {ex.Message}");
            }
        }
        public async Task<List<EstudiosModelo>> LeerEstudiosPorEspecialidad(string especialidad)
        {
            try
            {
                var filter = Builders<EstudiosModelo>.Filter.Eq(e => e.EspecialidadMedica, especialidad);
                var estudios = await _Estudios.Find(filter).ToListAsync();
                return estudios;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer estudios de la especialidad '{especialidad}': {ex.Message}");
            }
        }


    }






}

