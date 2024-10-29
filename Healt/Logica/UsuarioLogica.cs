using Healt.Datos;
namespace Healt.Logica
{
    public class UsuarioLogica
    {
        private readonly UsuarioDatos _usuarioDatos;

        public UsuarioLogica(UsuarioDatos usuarioDatos = null)
        {
            // Si no se pasa una instancia de UsuarioDatos, se crea una nueva
            _usuarioDatos = usuarioDatos ?? new UsuarioDatos();
        }

        // Método para verificar el usuario y su contraseña
        public async Task<(bool esValido, UsuarioInfo usuario, string mensaje)> VerificarUsuario(string usuario, string contrasena)
        {
            try
            {
                // Verifica el usuario en la capa de datos
                var resultado = await _usuarioDatos.VerificarUsuario(usuario, contrasena);

                return resultado;
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                return (false, null, $"Error al verificar el usuario: {ex.Message}");
            }
        }
    }



}






