namespace backendPFPU.Models;

public class Administrador : Usuario {
    private string rango;

    public Administrador()
    {
    }

    public Administrador(Usuario usuario, string rango)
    {
        this.id_usuario = usuario.Id_usuario;
        this.dni = usuario.Dni;
        this.contrasenia = usuario.Contrasenia;
        this.correo = usuario.Correo;
        this.nombre = usuario.Nombre;
        this.apellido = usuario.Apellido;
        this.tipo = usuario.Tipo;
        this.rango = rango;
    }
}