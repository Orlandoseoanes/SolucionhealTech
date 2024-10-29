using Healt.Logica;
using Microsoft.VisualBasic;

namespace Healt.Presentacion
{
    public class UsuariosUI
    {
        private readonly UsuarioLogica _usuarioLogica;

        public UsuariosUI(UsuarioLogica usuarioLogica)
        {
            _usuarioLogica = usuarioLogica;
        }

        public async Task VerificarUsuarioAsync()
        {
            Console.Write("Ingrese el usuario: ");
            string usuario = Console.ReadLine();
            Console.Write("Ingrese la contraseña: ");
            string contrasena = Console.ReadLine();

            var resultado = await _usuarioLogica.VerificarUsuario(usuario, contrasena);

            if (resultado.esValido)
            {
                Console.WriteLine("Inicio de sesión exitoso...");
               
                Console.Clear();

                switch (resultado.usuario.Rol)
                {
                    case "Recepcionista":
                        await new RecepcionistaUI(_usuarioLogica).EjecutarAsync(resultado.usuario.ToString());
                        break;
                    case "PersonalMedico":
                        await new PersonalMedicoUI(_usuarioLogica).EjecutarAsync(resultado.usuario.ToString());
                        break;
                    default:
                        Console.WriteLine("Rol no reconocido.");
                        break;
                }
            }
            else
            {
                Console.WriteLine(resultado.mensaje);
            }
        }
    }






}