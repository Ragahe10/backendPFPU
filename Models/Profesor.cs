namespace backendPFPU.Models;

public class Profesor : Usuario {

    public Profesor()
    {
    }

    public Profesor(Usuario usuario, string direccion, int matricula, DateTime fecha_nac)
    {
        this.id_usuario = usuario.Id_usuario;
        this.dni = usuario.Dni;
        this.contrasenia = usuario.Contrasenia;
        this.correo = usuario.Correo;
        this.nombre = usuario.Nombre;
        this.apellido = usuario.Apellido;
        this.tipo = usuario.Tipo;
    }
}