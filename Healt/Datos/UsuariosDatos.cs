using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Healt.Datos
{
    public class UsuarioInfo
    {
        [BsonId]
        public ObjectId Id { get; set; } // Campo que representa el ID de MongoDB

        [BsonElement("ID")]
        public int ID { get; set; } // Identificador numérico

        [BsonElement("Usuario")]
        public string Usuarios { get; set; }

        [BsonElement("Contrasena")]
        public string Contrasena { get; set; }  // Asegúrate de que esta propiedad exista

        [BsonElement("Nombre")]
        public string Nombre { get; set; }  // Campo adicional

        [BsonElement("EspecialidadMedica")]
        public string EspecialidadMedica { get; set; }  // Campo adicional

        [BsonElement("Rol")]
        public string Rol { get; set; }
    }

    public class UsuarioDatos
    {
        private readonly IMongoCollection<UsuarioInfo> _usuarios;

        public UsuarioDatos()
        {
            try
            {
                var cliente = new MongoClient("mongodb+srv://ooviedo12:admin.123@hospital.xlwki.mongodb.net/?retryWrites=true&w=majority&appName=hospital");
                var baseDatos = cliente.GetDatabase("HealtDB");
                _usuarios = baseDatos.GetCollection<UsuarioInfo>("Usuarios");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
                throw;
            }
        }

        public async Task<(bool esValido, UsuarioInfo usuario, string mensaje)> VerificarUsuario(string usuario, string contrasena)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                    return (false, null, "Usuario o contraseña no pueden estar vacíos");

                // Buscar el usuario en la base de datos
                var filtro = Builders<UsuarioInfo>.Filter.Eq(u => u.Usuarios, usuario);
                var usuarioEncontrado = await _usuarios.Find(filtro).FirstOrDefaultAsync();

                // Si el usuario no fue encontrado, retorna false
                if (usuarioEncontrado == null)
                {
                    return (false, null, "Usuario no encontrado");
                }

                // Comparar la contraseña almacenada con la suministrada
                if (usuarioEncontrado.Contrasena == contrasena)
                {
                    return (true, usuarioEncontrado, "Inicio de sesión exitoso");
                }
                else
                {
                    return (false, null, "Contraseña incorrecta");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar el usuario: {ex.Message}");
                return (false, null, $"Error al verificar el usuario: {ex.Message}");
            }
        }
    }
}





